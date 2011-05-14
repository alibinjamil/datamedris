using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Data.SqlClient;
using System.Collections.Generic;

using RIS.RISLibrary.Utilities;
using RIS.Common;

public partial class Radiologist_EditFinding : StudyPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            tbDescription.Text = "";
            tbHeading.Text = "";
            tbImpression.Text = "";

            if (Request[ParameterNames.Request.StudyId] != null)
            {
                
                hlAttach.NavigateUrl = "~/WebScan/AttachmentsList.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId];
                //int studyId = int.Parse(Request[ParameterNames.Request.StudyId]);
                Study study = GetStudy();
                if(study != null)
                {
                    
                    if(study.TechComments != null)
                    {
                        techComments.Text = study.TechComments;
                    }
                    if (loggedInUserRoleId == Constants.Roles.Radiologist 
                        && study.StudyStatusId != Constants.StudyStatusTypes.Verified)
                    {
                        btnSave.Visible = true;
                        btnVerify.Visible = true;
                        btnReject.Visible = true;
                        ddlBodyParts.Visible = true;
                        ddlTemplates.Visible = true;
                        btnApplyTemplate.Visible = true;
                        if (study.ModalityId != null)
                        {
                            BindBodyPart(study.ModalityId.Value);
                            if (study.BodyPartId.HasValue)
                            {
                                ddlBodyParts.SelectedValue = study.BodyPartId.Value.ToString();
                                BindTemplate(study.BodyPartId.Value);
                                if (study.TemplateId != null)
                                {
                                    ddlTemplates.SelectedValue = study.TemplateId.Value.ToString();
                                }
                            }
                        }
                    }

                    lblExamDate.Text = study.StudyDate.Value.ToShortDateString();
                    if (study.Modality != null)
                    {
                        lblModality.Text = study.Modality.Name;
                    }

                    
                    
                    lblPatientId.Text = study.ExternalPatientId;
                    lblPatientName.Text = study.PatientName;
                    

                    if (study.StudyStatusType != null)
                    {                        
                        lblStatus.Text = study.StudyStatusType.Status;
                    }

                    if (study.ReferringPhysician != null)
                    {
                        lblPhysician.Text = study.ReferringPhysician.Name;
                    }

                    if (study.Procedure != null)
                    {                     
                        lblProcedure.Text = study.Procedure.Name;
                    }

                    if (study.Radiologist != null)
                    {
                        lblRadiologist.Text = study.Radiologist.Name;
                    }
                    
                    tbHeading.Text = study.Heading;
                    tbDescription.Text = study.Description;
                    tbImpression.Text = study.Impression;

                    if (study.ParentStudy != null)
                    {
                        pnlAmendment.Visible = true;
                        tbDescription.Enabled = false;
                        tbHeading.Enabled = false;
                        tbImpression.Enabled = false;
                        ddlBodyParts.Enabled = false;
                        ddlTemplates.Enabled = false;
                        btnApplyTemplate.Enabled = false;

                        tbAmendment.Text = study.Amendment;
                        btnAmendments.Visible = true;                        
                    }
                }                
            }
        }
    }
    private void BindBodyPart(int modalityId)
    {
        ddlBodyParts.DataSource = (from bp in DatabaseContext.BodyParts where bp.ModalityId == modalityId select bp);
        ddlBodyParts.DataTextField = "Name";
        ddlBodyParts.DataValueField = "BodyPartId";
        ddlBodyParts.DataBind();
    }

    private void BindTemplate(int bodyPartId)
    {
        ddlTemplates.DataSource = (from t in DatabaseContext.TemplateUsers
                                   where t.UserId == loggedInUserId
                                   && t.Template.BodyPartId == bodyPartId
                                   select t.Template);
        ddlTemplates.DataTextField = "Name";
        ddlTemplates.DataValueField = "TemplateId";
        ddlTemplates.DataBind();        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateStudy(Constants.StudyStatusTypes.PendingVerification);
        ClientScript.RegisterStartupScript(this.GetType(), "CloseFinding", "parent.document.aspnetForm.submit();",true);
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        Study study = UpdateStudy(Constants.StudyStatusTypes.Verified);
        if(study != null)
        {
            if (ConfigurationManager.AppSettings["SendFax"].ToUpper().Equals("TRUE"))
            {
                string filePath = ReportGenerator.Instance.Generate(study);
                if (filePath != null && study.Hospital != null && study.Hospital.Fax != null)
                {
                    string name = (study.Hospital.Name == null) ? "" : (string)study.Hospital.Name;
                    FaxSender.Instance.SendFax(name, name, study.Hospital.Fax, filePath, study.HospitalId.Value);
                }
            }
        
            //send sms to ref phy
            if (ConfigurationManager.AppSettings["SendSMS"].ToUpper().Equals("TRUE"))
            {
                if (study.ReferringPhysician != null && study.ReferringPhysician.SendSMS != null
                    && study.ReferringPhysician.SendSMS == true)
                {

                    string clientURL = "www.datamedusa.com";
                    string hospitalName = "DataMed";
                    if (study.Client != null && study.Client.Website != null)
                    {
                        clientURL = study.Client.Website;
                    }
                    if (study.Hospital != null)
                    {
                        hospitalName = study.Hospital.Name;
                    }
                    EmailSender.Instance.SendSMS(study.ReferringPhysician.CarrierId.Value, study.ReferringPhysician.Mobile, hospitalName, clientURL);
                }
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "CloseFinding", "parent.document.aspnetForm.submit();", true);
    }

    private Study UpdateStudy(Nullable<int> studyStatusId)
    {
        Study study = GetStudy();                 
        if(study != null)
        {                 
            study.LastUpdateDate = DateTime.Now;
            study.LastUpdatedBy = loggedInUserId;
            //adding this code to put in radiologist is and name. 
            study.ReportDate = DateTime.Now;
            study.RadiologistId = loggedInUserId;
            study.Heading = tbHeading.Text;
            study.Description = tbDescription.Text;
            study.Impression = tbImpression.Text;
            study.Amendment = tbAmendment.Text;   
            
            if (studyStatusId != null)
            {
                study.StudyStatusId = studyStatusId;
            }
            DatabaseContext.SaveChanges();
        }        
        return study;
    }
    
    

    protected override bool IsPopUp()
    {
        return true;
    }
    protected void ddlBodyParts_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBodyParts.SelectedIndex > 0)
        {            
            BindTemplate(int.Parse(ddlBodyParts.SelectedValue));                        
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            int templateId = int.Parse(ddlTemplates.SelectedValue);

            Study study = GetStudy();
            if (study != null)
            {
                study.BodyPartId = int.Parse(ddlBodyParts.SelectedValue);
                study.TemplateId = int.Parse(ddlTemplates.SelectedValue);
                DatabaseContext.SaveChanges();
            }

            study = GetStudy();
            if (study.Template != null)
            {
                if (study.Template.Heading != null)
                {
                    tbHeading.Text = study.Template.Heading;
                }
                if (study.Template.Description != null)
                {
                    tbDescription.Text = study.Template.Description;
                }
                if (study.Template.Impression != null)
                {
                    tbImpression.Text = study.Template.Impression;
                }
            }
        }
        catch
        {
        }
    }
    protected void btnAmendments_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Exams/AmendmentsList.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId]);
    }
}
