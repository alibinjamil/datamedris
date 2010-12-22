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
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;
using System.IO;

public partial class Radiologist_FindingReport : AuthenticatedPage
{
    ReportObject report = null;
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        int studyId = int.Parse(Request[ParameterNames.Request.StudyId]);
        report = new ReportObject(studyId,true);
        if (report.Load())
        {
            if (IsPostBack == false)
            {
                faxBtn.OnClientClick = ClientClickForFaxBtn();
            }

            lblClientName.Text = report.ClientName;
            lblClientAddress.Text = report.ClientAddress;
            lblModality.Text = report.Modality;
            lblManualStatus.Text = report.ManualStatus;
            lblPatientName.Text = report.PatientName;
            lblDOB.Text = report.DateOfBirth;
            lblRefPhy.Text = report.ReferringPhysician;
            lblStudyDate.Text = report.StudyDate;
            
            lblTranscription.Text = report.Transcription;
            lblReportDateTime.Text = report.ReportDateTime;
            lblReportDate.Text = report.ReportDate;
                
            lblRadiologist.Text = report.Radiologist;
            lblStatus.Text = report.Status;
            lblHospitalName.Text = report.HospitalName;

            LogObject log = new LogObject();
            log.UserId.Value = loggedInUserId;
            log.StudyId.Value = studyId;
            log.PatientId.Value = report.PatientId;
            log.Action.Value = Constants.LogActions.ViewedStudy;
            log.ActionTime.Value = DateTime.Now;
            log.Save();
        }
    }
    protected override bool IsPopUp()
    {
        return true;
    }



    protected void pdfBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Radiologist/DownloadReport.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId]);
    }
    protected void faxBtn_Click(object sender, ImageClickEventArgs e)
    {
        string fax = FaxSender.Instance.SendFax(int.Parse(Request[ParameterNames.Request.StudyId]));
        if (fax != null)
        {
            RegisterStartupScript("FaxSent", "<script>alert('Selected report has been routed for fax to " + fax + "')</script>");
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
}
