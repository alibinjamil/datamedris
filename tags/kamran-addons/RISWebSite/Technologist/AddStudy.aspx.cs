using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using RIS.RISLibrary.Utilities;
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;
using System.Text;

public partial class Technologist_AddStudy : AuthenticatedPage
{
    int WizardFormStep;
    bool IsWizardStepValid;

    override protected void Page_Load_Extended(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            IsWizardStepValid = true;
            WizardFormStep = Wizard1.ActiveStepIndex;
            if (Request[ParameterNames.Request.ExternalPatientId] != null)
            {
                tbPatId.Text = Request[ParameterNames.Request.ExternalPatientId];
                LoadPatientData();
            }
            //BindDDL();
        }
    }
    protected override bool IsPopUp()
    {
        return false;
    }
    private void BindDDL()
    {
        DatabaseUtility.BindUserDDL(Constants.Roles.ReferringPhysician, Labels.DDLTexts.PleaseSelect, ddlRef);
        DatabaseUtility.BindUserDDL(Constants.Roles.Radiologist, Labels.DDLTexts.PleaseSelect, ddlRadiologist);
    }
    protected void OnPatientIdChange(object sender, EventArgs e)
    {
        LoadPatientData();
    }
    private void LoadPatientData()
    {
        PatientObject patient = new PatientObject();
        patient.ExternalPatientId.Value = tbPatId.Text;
        patient.Load();
        if (patient.IsLoaded)
        {
            if (patient.Name.Value != null)
            {
                string[] names = ((string)patient.Name.Value).Split(',');
                if (names.Length > 1)
                {
                    tbPatLName.Text = names[0].Trim();
                    tbPatFName.Text = names[1].Trim();
                }
                else
                {
                    tbPatFName.Text = names[0].Trim();
                }
            }
            if (patient.DateOfBirth.Value != null)
                dcDOB.Date = (DateTime)patient.DateOfBirth.Value;
            if (patient.Gender.Value != null)
                rblGender.SelectedValue = (string)patient.Gender.Value;
            else
                rblGender.ClearSelection();
        }
        else
        {
            tbPatFName.Text = "";
            dcDOB.ClearSelection();
            rblGender.ClearSelection();
        }
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        PatientObject patient = new PatientObject();
        patient.ExternalPatientId.Value = tbPatId.Text;
        patient.Load(loggedInUserId);
        patient.DateOfBirth.Value = dcDOB.Date;
        patient.Name.Value = RISUtility.GetFullName(tbPatFName.Text, tbPatLName.Text);
        patient.Gender.Value = rblGender.SelectedValue;
        patient.Save(loggedInUserId);
        StudyObject study = new StudyObject();
        study.PatientId.Value = patient.PatientId.Value;
        Random rand = new Random();
        study.StudyInstance.Value = rand.Next().ToString();
        DateTime dateTime = new DateTime(dcExamDate.Year, dcExamDate.Month, dcExamDate.Day, tcExamTime.Hour, tcExamTime.Minute, 0);
        study.StudyDate.Value = dateTime;
        study.ReferringPhysicianId.Value = ddlRef.SelectedValue;
        study.IsManual.Value = "Y";
        study.StudyStatusId.Value = Constants.StudyStatusTypes.PendingVerification;
        study.ModalityId.Value = ddlModality.SelectedValue;
        study.ProcedureId.Value = ddlProcedures.SelectedValue;
        study.Save(loggedInUserId);
        FindingObject finding = new FindingObject();
        finding.AudioUserId.Value = ddlRadiologist.SelectedValue;
        finding.StudyId.Value = study.StudyId.Value;
        finding.TextualTranscript.Value = tbFinding.Text;
        finding.TranscriptUserId.Value = loggedInUserId;
        finding.TranscriptionDate.Value = DateTime.Now;
        finding.Save(loggedInUserId);
        study.LatestFindingId.Value = finding.FindingId.Value;
        study.Save(loggedInUserId);
        GroupsTableAdapters.tGroupsTableAdapter groupsTA = new GroupsTableAdapters.tGroupsTableAdapter();
        groupsTA.InsertStudyDefaultGroup(int.Parse(study.StudyId.Value.ToString()), loggedInUserId);
        //code for logging
        LogObject log = new LogObject();
        log.UserId.Value = loggedInUserId;
        log.Action.Value = Constants.LogActions.MarkedStudyForVerification;
        log.ActionTime.Value = DateTime.Now;
        log.Save();
        /////
        /// Get User Groups
        //DataTable userGroups = new DataTable();
        //userGroups = RISProcedureCaller.GetUserGroupsWithDefaults(loggedInUserId);
        //if (userGroups.Rows.Count > 0)
        //{
        //    for (int RowCounter = 0; RowCounter <= userGroups.Rows.Count - 1; RowCounter++)
        //    {
        //        StudyGroupObject studygroup = new StudyGroupObject();
        //        studygroup.GroupId.Value  = userGroups.Rows[RowCounter][0];
        //        studygroup.StudyId.Value  = study.StudyId.Value;
        //        studygroup.Save(loggedInUserId);
        //    }
        //}
        StringBuilder args = new StringBuilder();
        args.Append(ParameterNames.Request.ExternalPatientId);
        args.Append("=");
        args.Append(patient.ExternalPatientId.Value);
        args.Append("&");
        args.Append(ParameterNames.Request.PatientName);
        args.Append("=");
        args.Append(patient.Name.Value);
        args.Append("&");
        args.Append(ParameterNames.Request.ReturnPage);
        args.Append("=");
        args.Append(PagesFactory.Pages.AddStudyPage);
        PagesFactory.Transfer(PagesFactory.Pages.DataSavedPage,args.ToString());
    }

    protected void StepNextButton_Click(object sender, EventArgs e)
    {
        IsWizardStepValid = Page.IsValid;
        if (!IsWizardStepValid)
        {
            Wizard1.ActiveStepIndex = WizardFormStep;
            //CVRow.Style.Add("visibility", "visible");
        }

    }
    protected void StartNextButton_Click(object sender, EventArgs e)
    {
        if (dcExamDate.Day == 0 && dcExamDate.Month == 0 &&  dcExamDate.Year == 0)
        {
            dcExamDate.Day = 01;
            dcExamDate.Month = 01;
            dcExamDate.Year = 2000;
        }
    }
    protected void ddlProcedures_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlModality_DataBound(object sender, EventArgs e)
    {
        ddlModality.Items.Insert(0,new ListItem(Labels.DDLTexts.PleaseSelect,"0"));
    }
    protected void ddlProcedures_DataBound(object sender, EventArgs e)
    {
        ddlProcedures.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
    }

    protected void ddlRef_DataBound(object sender, EventArgs e)
    {
        ddlRef.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
    }
    protected void ddlRadiologist_DataBound(object sender, EventArgs e)
    {
        ddlRadiologist.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
    }
}
