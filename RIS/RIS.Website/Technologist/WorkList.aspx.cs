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
using System.IO;
using System.Text;
using System.Security;

using System.Data.OleDb;

using RIS.RISLibrary.Objects.DICOM;
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;

public partial class Technologist_WorkList : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            if (Request[ParameterNames.Request.ExternalPatientId] != null)
            {
                tbPatId.Text = Request[ParameterNames.Request.ExternalPatientId];
                LoadPatientData();
            }
            DatabaseUtility.BindUserDDL(Constants.Roles.ReferringPhysician, Labels.DDLTexts.PleaseSelect, ddlRefPhysician);
            DatabaseUtility.BindModalitiesDDL(Labels.DDLTexts.PleaseSelect, ddlModality);
            DatabaseUtility.BindProceduresDDL(Labels.DDLTexts.PleaseSelect, int.Parse(ddlModality.SelectedValue), ddlProcedure);
            DatabaseUtility.BindStationsDDL(Labels.DDLTexts.PleaseSelect, int.Parse(ddlModality.SelectedValue), ddlStationName);
        }
    }
    protected override bool IsPopUp()
    {
        return false;
    }
    /*protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        DICOMWorklistObject workList = new DICOMWorklistObject();
        workList.PatientId.Value = tbPatId.Text;
        workList.PatientName.Value = RISUtility.GetFullName(tbPatFName.Text, tbPatLName.Text);
        workList.PatientBir.Value = dcDOB.Date;
        workList.PatientSex.Value = (rblGender.SelectedIndex == 0) ? "M" : "F";
        workList.MedicalAle.Value = tbMedAllergy.Text;
        workList.ReqPhysici.Value = ddlReqPhysician.SelectedValue;
        workList.ReqProcDes.Value = tbProcDesc.Text;
        workList.ReqProcID.Value = tbProcId.Text;
        workList.ReqContras.Value = tbContrast.Text;
        workList.ReqProcPri.Value = ddlPriority.SelectedValue;
        //workList.
    }*/
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblDOB.Text = RISUtility.GetDICOMDate(dcDOB.Date);
        lblFirstName.Text = tbPatFName.Text;
        lblLastName.Text = tbPatLName.Text;
        lblGender.Text = rblGender.SelectedItem.Text;
        lblModality.Text = ddlModality.SelectedItem.Text;
        lblPatientId.Text = tbPatId.Text;
        lblProcedure.Text = ddlProcedure.SelectedItem.Text;
        lblRefPhy.Text = ddlRefPhysician.SelectedItem.Text;
        lblStation.Text = ddlStationName.SelectedItem.Text;
        SetLabelVisibilty(true);
        SetFieldVisibility(false);
        btnCancel.Visible = true;
        btnSave.Visible = false;
        btnConfirmSave.Visible = true;
        EnableValidator(false);
    }

    private void EnableValidator(bool validation)
    {
        RequiredFieldValidator1.Enabled = validation;
        RequiredFieldValidator2.Enabled = validation;
        RequiredFieldValidator3.Enabled = validation;
        RequiredFieldValidator4.Enabled = validation;
        CustomValidator1.Enabled = validation;
        CustomValidator3.Enabled = validation;
        CustomValidator4.Enabled = validation;
        CustomValidator5.Enabled = validation;
        CustomValidator6.Enabled = validation;
    }

    private void SetLabelVisibilty(bool visibilty)
    {
        lblConfirmSave.Visible = visibilty;
        lblDOB.Visible = visibilty;
        lblFirstName.Visible = visibilty;
        lblGender.Visible = visibilty;
        lblLastName.Visible = visibilty;
        lblModality.Visible = visibilty;
        lblPatientId.Visible = visibilty;
        lblProcedure.Visible = visibilty;
        lblRefPhy.Visible = visibilty;
        lblStation.Visible = visibilty;
        lblYear.Visible = visibilty;
        lblMonth.Visible = visibilty;
        lblDay.Visible = visibilty;
        lblBE.Visible = visibilty;
        lblBO.Visible = visibilty;
    }
    private void SetFieldVisibility(bool visibility)
    {
        dcDOB.Visible = visibility;
        tbPatFName.Visible = visibility;
        tbPatLName.Visible = visibility;
        rblGender.Visible = visibility;
        ddlModality.Visible = visibility;
        tbPatId.Visible = visibility;
        ddlProcedure.Visible = visibility;
        ddlRefPhysician.Visible = visibility;
        ddlStationName.Visible = visibility;
    }

    private void WriteFile(StringBuilder fileContents, string fileName)
    {
        string mwlDirectory = ConfigurationManager.AppSettings["MWLDirectory"];
        string mwlFilePath = mwlDirectory + "\\" + fileName + ".xml";
        StreamWriter mwlFile = new FileInfo(mwlFilePath).CreateText();
        mwlFile.Write(fileContents.ToString());
        mwlFile.Close();
    }

    private StringBuilder GetFile()
    {
        StringBuilder workListData = new StringBuilder();
        StreamReader workListFile = File.OpenText(ConfigurationManager.AppSettings["MWLFile"]);
        workListData.Append(workListFile.ReadToEnd());
        workListFile.Close();
        return workListData;
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
    protected void OnModalityChange(object sender, EventArgs e)
    {
        DatabaseUtility.BindProceduresDDL(Labels.DDLTexts.PleaseSelect, int.Parse(ddlModality.SelectedValue), ddlProcedure);
        DatabaseUtility.BindStationsDDL(Labels.DDLTexts.PleaseSelect, int.Parse(ddlModality.SelectedValue), ddlStationName);
    }
    protected void btnConfirmSave_Click(object sender, EventArgs e)
    {
        PatientObject patient = new PatientObject();
        patient.ExternalPatientId.Value = tbPatId.Text;
        patient.Load(Constants.Database.NullUserId);
        patient.Name.Value = RISUtility.GetFullName(tbPatFName.Text, tbPatLName.Text);
        patient.DateOfBirth.Value = dcDOB.Date;
        patient.Gender.Value = rblGender.SelectedItem.Text;
        patient.Save(loggedInUserId);
        WorkListObject worklist = new WorkListObject();
        worklist.PatientId.Value = patient.PatientId.Value;
        worklist.RequestingPhysicianId.Value = int.Parse(ddlRefPhysician.SelectedValue);
        worklist.ModalityId.Value = int.Parse(ddlModality.SelectedValue);
        worklist.ProcedureId.Value = int.Parse(ddlProcedure.SelectedValue);
        worklist.StationId.Value = int.Parse(ddlStationName.SelectedValue);
        worklist.Save(loggedInUserId);
        //try
        {
            StringBuilder workListFile = GetFile();
            workListFile.Replace("${AccessionNumber}", SecurityElement.Escape(worklist.WorkListId.Value.ToString()));
            workListFile.Replace("${ReferringPhysician}", SecurityElement.Escape(ddlRefPhysician.SelectedItem.Text));
            workListFile.Replace("${PatientName}", SecurityElement.Escape(RISUtility.GetFullName(tbPatFName.Text, tbPatLName.Text)));
            workListFile.Replace("${PatientId}", SecurityElement.Escape(tbPatId.Text));
            workListFile.Replace("${PatientDOB}", SecurityElement.Escape(RISUtility.GetDICOMDate(dcDOB.Date)));
            workListFile.Replace("${PatientSex}", SecurityElement.Escape(rblGender.SelectedItem.Text));
            int age = DateTime.Now.Subtract(dcDOB.Date).Days/365;
            workListFile.Replace("${PatientAge}", SecurityElement.Escape(age.ToString()));
            workListFile.Replace("${Procedure}", SecurityElement.Escape(ddlProcedure.SelectedItem.Text));
            workListFile.Replace("${Modality}", SecurityElement.Escape(ddlModality.SelectedItem.Text));
            workListFile.Replace("${StationName}", SecurityElement.Escape(ddlStationName.SelectedItem.Text));
            WriteFile(workListFile, worklist.WorkListId.Value.ToString());
            Session[ParameterNames.Session.InformationMessage] = Messages.Information.DataSaved;
            string url = PagesFactory.GetUrl(PagesFactory.Pages.DataSavedPage);
            url += "?" + ParameterNames.Request.ExternalPatientId + "=" + patient.ExternalPatientId.Value;
            url += "&" + ParameterNames.Request.PatientName + "=" + patient.Name.Value;
            Response.Redirect(url);
        }
        //catch (OleDbException ex)
        {
            //((Main)this.Master).ErrorMessage = Messages.Error.ErrorSavingDataToDICOM;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        SetFieldVisibility(true);
        SetLabelVisibilty(false);
        btnCancel.Visible = false;
        btnSave.Visible = true;
        btnConfirmSave.Visible = false;
        EnableValidator(true);
    }
}
