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
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

using RIS.RISLibrary.Utilities;

using RIS.Common;
using System.Text;

public partial class Technologist_AddStudy : StudyPage
{
    int WizardFormStep;
    bool IsWizardStepValid;
    Study study = null;
    override protected void Page_Load_Extended(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            study = GetManualStudy();
            if (study != null)
            {
                tbPatId.Text = study.OriginalPatientId;
                tbPatientName.Text = study.PatientName;
                tbPatWeight.Text = study.PatientWeight;
                rblGender.SelectedValue = study.PatientGender;
                dcExamDate.Date = study.StudyDate.Value;
                dcDOB.Date = study.PatientDOB.Value;
                tbTechComments.Text = study.TechComments;
                if (study.ClientId.HasValue)
                {
                    ddlClient.SelectedValue = study.ClientId.Value.ToString();
                }
                if (study.HospitalId.HasValue)
                {
                    ddlHospital.SelectedValue = study.HospitalId.Value.ToString();
                }
                if (study.ModalityId.HasValue)
                {
                    ddlModality.SelectedValue = study.ModalityId.Value.ToString();
                }
                if (study.ProcedureId.HasValue)
                {
                    ddlProcedures.SelectedValue = study.ProcedureId.Value.ToString();
                }
                if (study.ReferringPhysicianId.HasValue)
                {
                    ddlRef.SelectedValue = study.ReferringPhysicianId.Value.ToString();
                }
            }
            BindClientList();
            BindModalitiesList();
        }

    }
    
    

    protected override bool IsPopUp()
    {
        return false;
    }
    
    private void BindClientList()
    {
        if (loggedInUserRoleId != Constants.Roles.Admin)
        {
            ddlClient.DataSource = (from uc in DatabaseContext.UserClients where uc.UserId == loggedInUserId orderby uc.Client.Name select uc.Client);
        }
        else
        {
            ddlClient.DataSource = (from c in DatabaseContext.Clients orderby c.Name select c);
        }
        ddlClient.DataTextField = "Name";
        ddlClient.DataValueField = "ClientId";
        ddlClient.DataBind();     
    }
    private void BindHospitalList()
    {
        if (loggedInUserRoleId != Constants.Roles.Admin)
        {
            ddlHospital.DataSource = (from uh in DatabaseContext.UserHospitals where uh.UserId == loggedInUserId orderby uh.Hospital.Name select uh.Hospital);
        }
        else
        {
            int clientId = int.Parse(ddlClient.SelectedValue);
            ddlHospital.DataSource = (from h in DatabaseContext.Hospitals where h.ClientId == clientId orderby h.Name select h);
        }
        ddlHospital.DataTextField = "Name";
        ddlHospital.DataValueField = "HospitalId";
        ddlHospital.DataBind();
    }

    private void BindModalitiesList()
    {
        ddlModality.DataSource = (from m in DatabaseContext.Modalities select m);
        ddlModality.DataTextField = "Name";
        ddlModality.DataValueField = "ModalityId";
        ddlModality.DataBind();
    }
    protected void OnPatientIdChange(object sender, EventArgs e)
    {
        LoadPatientData();
    }
    private void LoadPatientData()
    {
        Study study = (from s in DatabaseContext.Studies where s.ExternalPatientId == tbPatId.Text select s).FirstOrDefault();
        if (study != null)
        {
            tbPatientName.Text = study.PatientName;
            if (study.PatientDOB.HasValue)
            {
                dcDOB.Date = study.PatientDOB.Value;
            }
            if (study.PatientGender != null)
            {
                rblGender.SelectedValue = study.PatientGender;
            }
            else
            {
                rblGender.ClearSelection();
            }
        }
        else
        {
            tbPatientName.Text = "";
            dcDOB.ClearSelection();
            rblGender.ClearSelection();
        }
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        /*string status = hfStatus.Value;
        
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
        args.Append(PagesFactory.Pages.AddStudyPage);*/
        //PagesFactory.Transfer(PagesFactory.Pages.DataSavedPage,args.ToString());
    }

    protected void StepNextButton_Click(object sender, EventArgs e)
    {
        IsWizardStepValid = Page.IsValid;
        if (!IsWizardStepValid)
        {
            //Wizard1.ActiveStepIndex = WizardFormStep;
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
        ddlModality.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
        if (IsPostBack == false)
        {
            
            if (study != null && study.ModalityId.HasValue)
            {
                ddlModality.SelectedValue = study.ModalityId.Value.ToString();
            }
            BindProceduresList();
        }
    }
    protected void ddlProcedures_DataBound(object sender, EventArgs e)
    {
        ddlProcedures.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
        if (IsPostBack == false)
        {            
            if (study != null && study.ProcedureId.HasValue)
            {
                ddlProcedures.SelectedValue = study.ProcedureId.Value.ToString();
            }
        }
    }

    protected void ddlRef_DataBound(object sender, EventArgs e)
    {
        ddlRef.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
        if (IsPostBack == false)
        {
            if (study != null && study.ReferringPhysicianId.HasValue)
            {
                ddlRef.SelectedValue = study.ReferringPhysicianId.Value.ToString();
            }
        }
    }
    protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindHospitalList();
    }
    protected void ddlClient_DataBound(object sender, EventArgs e)
    {
        ddlClient.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
        if (IsPostBack == false)
        {
            if (study != null && study.ClientId.HasValue)
            {
                ddlClient.SelectedValue = study.ClientId.Value.ToString();
            }
            else if (loggedInUserRoleId != Constants.Roles.Admin)
            {
                ddlClient.SelectedValue = loggedInUser.UserClients.FirstOrDefault().ClientId.ToString();
                ddlClient.Enabled = false;
            }
            BindHospitalList();
        }            
    }

    protected void ddlHospital_DataBound(object sender, EventArgs e)
    {
        ddlHospital.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));

        if (IsPostBack == false)
        {
            if (study != null && study.HospitalId.HasValue)
            {
                ddlHospital.SelectedValue = study.HospitalId.Value.ToString();
            }
            BindRefPhyList();
        }
    }

    private void BindRefPhyList()
    {
        int hospitalId = int.Parse(ddlHospital.SelectedValue);
        ddlRef.DataSource = (from uh in DatabaseContext.UserHospitals
                                where uh.HospitalId == hospitalId
                                && uh.User.UserRoles.FirstOrDefault().RoleId == Constants.Roles.ReferringPhysician
                                orderby uh.User.Name
                                select uh.User);
        ddlRef.DataTextField = "Name";
        ddlRef.DataValueField = "UserId";
        ddlRef.DataBind();
    }
    protected void ddlHospital_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRefPhyList();
    }
    protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddlModality_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProceduresList();
    }
    private void BindProceduresList()
    {
        int modalityId = int.Parse(ddlModality.SelectedValue);
        ddlProcedures.DataSource = (from p in DatabaseContext.Procedures
                             where p.ModalityId == modalityId
                             select p);
        ddlProcedures.DataTextField = "Name";
        ddlProcedures.DataValueField = "ProcedureId";
        ddlProcedures.DataBind();
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Study study = GetStudy();
        if (study == null)
        {
            study = new Study();
            study.CreatedBy = loggedInUserId;
            study.CreationDate = DateTime.Now;
            DatabaseContext.Studies.AddObject(study);
            study.StudyInstance = System.Guid.NewGuid().ToString();
        }
        study.PatientDOB = dcDOB.Date;
        study.PatientName = tbPatientName.Text;
        study.PatientGender = rblGender.SelectedValue;
        study.ExternalPatientId = tbPatId.Text;
        study.OriginalPatientId = tbPatId.Text;
        study.StudyStatusId = Constants.StudyStatusTypes.PreRelease;
        study.ClientId = int.Parse(ddlClient.SelectedValue);
        study.HospitalId = int.Parse(ddlHospital.SelectedValue);
        study.IsLatest = true;
        study.IsManual = true;
        study.LastUpdateDate = DateTime.Now;
        study.LastUpdatedBy = loggedInUserId;
        study.ModalityId = int.Parse(ddlModality.SelectedValue);
        study.PatientWeight = tbPatWeight.Text;
        study.ProcedureId = int.Parse(ddlProcedures.SelectedValue);
        study.ReferringPhysicianId = int.Parse(ddlRef.SelectedValue);
        study.StudyDate = dcExamDate.Date;
        study.TechComments = tbTechComments.Text;
        DatabaseContext.SaveChanges();
        Response.Redirect("~/Technologist/AddRadiologists.aspx?studyId=" + study.StudyId);
    }
    
}
