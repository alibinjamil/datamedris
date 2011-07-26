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
            if (loggedInUserRoleId == Constants.Roles.Radiologist)
            {
                //tbRadiologist.Text = (string)loggedInUser.Name.Value;
                //tbRadiologist.Enabled = false;

            }
            //BindDDL();
        }
    }
    protected string Radiologists
    {
        get
        {
            StringBuilder radiologists = new StringBuilder();
            /*UsersTableAdapters.tUsersTableAdapter rads = new UsersTableAdapters.tUsersTableAdapter();
            IEnumerator iEnum = rads.GetActiveUsersByRole(Constants.Roles.Radiologist).GetEnumerator();
            while (iEnum.MoveNext())
            {
                Users.tUsersRow rad = (Users.tUsersRow)iEnum.Current;
                radiologists.Append("\"").Append(rad.Name).Append("\"").Append(",");
            }
            //radiologists.Append("\"Alabama\"","\"Alaska\"");*/
            return radiologists.ToString();
        }
    }

    protected override bool IsPopUp()
    {
        return false;
    }
    private void BindDDL()
    {
        /*ClientTableAdapters.tClientsTableAdapter ta = new ClientTableAdapters.tClientsTableAdapter();
        ddlClients.DataSource = ta.GetClientsForUser(loggedInUserId);
        ddlClients.DataTextField = "Name";
        ddlClients.DataValueField = "ClientId";
        ddlClients.DataBind();*/
        //DatabaseUtility.BindUserDDL(Constants.Roles.ReferringPhysician, Labels.DDLTexts.PleaseSelect, ddlRef);
        //DatabaseUtility.BindUserDDL(Constants.Roles.Radiologist, Labels.DDLTexts.PleaseSelect, ddlRadiologist);
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
        string status = hfStatus.Value;
        
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
        DateTime dateTime = new DateTime(dcExamDate.Year, dcExamDate.Month, dcExamDate.Day);
        study.StudyDate.Value = dateTime;
        study.ReferringPhysicianId.Value = ddlRef.SelectedValue;
        study.IsManual.Value = "Y";
        study.StudyStatusId.Value = hfStatus.Value;
        study.ModalityId.Value = ddlModality.SelectedValue;
        study.ProcedureId.Value = ddlProcedures.SelectedValue;
        if (ddlClient.SelectedIndex > 0)
        {
            study.ClientId.Value = ddlClient.SelectedValue;
        }
        if (ddlHospital.SelectedIndex > 0)
        {
            study.HospitalId.Value = ddlHospital.SelectedValue;
        }
        study.Save(loggedInUserId);
        
        study.TechComments.Value = tbTechComments.Text;
        study.Save(loggedInUserId);
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
    protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindHospital();
    }
    protected void ddlClient_DataBound(object sender, EventArgs e)
    {
        ddlClient.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
        /*if (IsPostBack == false)
        {
            BindHospital();
        }*/
    }
    protected void ddlHospital_DataBound(object sender, EventArgs e)
    {
        ddlHospital.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
        if (IsPostBack == false)
        {
            BindRefPhy();
        }
    }
    private void BindHospital()
    {
        /*HospitalsTableAdapters.tHospitalsTableAdapter ta = new HospitalsTableAdapters.tHospitalsTableAdapter();
        ddlHospital.DataSource = ta.GetHospitalsForUser(loggedInUserId);
        ddlHospital.DataTextField = "Name";
        ddlHospital.DataValueField = "HospitalId";
        ddlHospital.DataBind();*/
    }
    private void BindRefPhy()
    {
        /*UsersTableAdapters.tUsersTableAdapter ta = new UsersTableAdapters.tUsersTableAdapter();
        ddlRef.DataSource = ta.GetUsersForHospital(int.Parse(ddlClient.SelectedValue), int.Parse(ddlHospital.SelectedValue), Constants.Roles.ReferringPhysician);
        ddlRef.DataTextField = "Name";
        ddlRef.DataValueField = "UserId";
        ddlRef.DataBind();*/
    }
    protected void ddlHospital_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRefPhy();
    }
    protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    {
        if (Wizard1.ActiveStepIndex == 2)
        {
            lblClient.Text = ddlClient.SelectedItem.Text;
            lblDOB.Text = dcDOB.Date.ToShortDateString();
            lblExamDate.Text = dcExamDate.Date.ToShortDateString();
            lblFirstName.Text = tbPatFName.Text;
            lblGender.Text = rblGender.SelectedItem.Text;
            lblHospital.Text = ddlHospital.SelectedItem.Text;
            lblLastName.Text = tbPatLName.Text;
            lblModality.Text = ddlModality.SelectedItem.Text;
            lblPatientID.Text = tbPatId.Text;
            lblProcedure.Text = ddlProcedures.SelectedItem.Text;
            lblRefPhy.Text = ddlRef.SelectedItem.Text;
            lblTechComments.Text = tbTechComments.Text;
        }
    }
}
