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
using System.Text;
using System.Xml;

using RIS.RISLibrary.Utilities;
using System.IO;
using RIS.Common;

public partial class Radiologist_FindingReport : StudyPage
{
    ReportObject report = null;
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            if (loggedInUserRoleId == Constants.Roles.Radiologist)
            {
                createTemplate.Visible = true;
            }
            else
            {
                createTemplate.Visible = false;
            }
            Study study = GetStudy();
            if (study != null)
            {
                report = new ReportObject(study, true);
                if (report.Load())
                {
                    lblFooter.Text = report.FooterText;
                    if (IsPostBack == false)
                    {
                        faxBtn.OnClientClick = ClientClickForFaxBtn();
                    }
                    lblClientAddress.Text = report.ClientAddress;
                    lblClientTelephone.Text = study.Client.Phone;
                    //lblClientName.Text = report.ClientName;
                    //lblClientAddress.Text = report.ClientAddress;
                    lblReportDate.Text = report.ReportDate;
                    lblRadiologist.Text = report.Radiologist;
                    lblReportDateTime.Text = report.ReportDateTime;
                    lblModality.Text = report.Modality;
                    lblDateAddendum.Text = report.DateAmmendtment;
                    
                    if (study.StudyStatusId == Constants.StudyStatusTypes.Verified || report.Ammendments.Count > 0)
                    {
                        lblVerified.Text = "Electronically Approved and Signed by:";                        
                    }
                    else if (study.StudyStatusId == Constants.StudyStatusTypes.PendingVerification)
                    {
                        lblVerified.Text = "Unverified draft report:";
                    }
                    
                    lblHospitalName.Text = report.HospitalName;
                    //lblModality.Text = report.Modality;
                    //lblManualStatus.Text = report.ManualStatus;
                    lblPatientName.Text = report.PatientName;
                    lblDOB.Text = report.DateOfBirth;
                    lblRefPhy.Text = report.ReferringPhysician;
                    lblExamDate.Text = report.StudyDate;
                    //lblExamDateTime.Text = study.StudyDate.Value.ToString();
                    imgClientLogo.ImageUrl = report.HeaderUrl;
                    lblPatientID.Text = report.PatientId;
                    lblGender.Text = study.PatientGender;
                    lblAccession.Text = study.AccessionNumber;
                    //lblBodyPart.Text = study.BodyPart.Name;
                    //lblVerificationDate.Text = report.ReportDateTime;

                    lblTranscription.Text = report.Transcription;
                    lblReportDateTime.Text = report.ReportDateTime;
                    //lblReportDate.Text = report.ReportDate;

                    
                    //lblStatus.Text = report.Status;
                    lblHospitalName.Text = report.HospitalName;
                    ammentmentPnl.Controls.Add(new LiteralControl(report.GetAmmendmentHTML()));

                    Log log = new Log();
                    log.UserId = loggedInUserId;
                    log.StudyId = study.StudyId;
                    log.Action = Constants.LogActions.ViewedStudy;
                    log.ActionTime = DateTime.Now;
                    DatabaseContext.AddToLogs(log);
                    DatabaseContext.SaveChanges();
                }
            } 
        }
    }
    protected override bool IsPopUp()
    {
        return true;
    }



    protected void pdfBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Exams/DownloadReport.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId]);
    }
    protected void faxBtn_Click(object sender, ImageClickEventArgs e)
    {
        Study study = GetStudy();
        if (study != null)
        {
            string fax = FaxSender.Instance.SendFax(study);
            if (fax != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "FaxSent", "alert('Selected report has been routed for fax to " + fax + "');", true);
            }
        }
    }
    protected string ClientClickForFaxBtn()
    {
        if (report != null && report.Fax != null)
        {
            return "return confirm('You have selected to fax this report to " + report.Fax + ".  Click OK to fax or Cancel to return.');";
        }
        return "alert('No fax number is available for this facility.  Please ask your system administrator to enter a fax number for this facility from the Hospital Administration tab.');return false;";
    }

    protected void viewBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/WebScan/AttachmentsList.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId]);
    }
    protected void createTemplate_Click(object sender, ImageClickEventArgs e)
    {
        Study study = GetStudy();
        if (study != null)
        {
            Template template = new Template();
            template.BodyPart = new BodyPart();
            template.BodyPart.ModalityId = study.ModalityId.Value;
            if (study.BodyPartId.HasValue)
            {
                template.BodyPartId = study.BodyPartId.Value;
            }
            
            template.Description = study.Description;
            template.Heading = study.Heading;
            template.Impression = study.Impression;
            template.Name = study.Heading;
            TemplateUser templateUser = new TemplateUser();
            templateUser.UserId = loggedInUserId;
            template.TemplateUsers.Add(templateUser);
            Session[ParameterNames.Session.Template] = template;
            Response.Redirect("~/AdminPages/AddTemplate.aspx");
        }
    }
    protected void printBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Exams/DownloadReport.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId]);
    }
}
