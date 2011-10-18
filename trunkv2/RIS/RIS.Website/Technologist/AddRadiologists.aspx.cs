using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using RIS.Common;
using RIS.RISLibrary.Utilities;

public partial class Technologist_AddRadiologists : StudyPage
{
    protected override bool IsPopUp()
    {
         return false;
    }
    Study study;
    protected override void  Page_Load_Extended(object sender, EventArgs e)
    {        
        study = GetManualStudy();
        if (IsPostBack == false)
        {
            
            if (study != null && (study.IsManual.HasValue == false || study.IsManual.Value))
            {
                BindStatusList();
                ddlExamStatus.SelectedValue = study.StudyStatusId.Value.ToString();
                hlScan.NavigateUrl = "~/WebScan/AddAttachment.aspx?studyId=" + study.StudyId + "&isReport=true";
                BindRadiologistPanel(study);
                if (study.ReportType.HasValue)
                {
                    rblReportType.SelectedValue = study.ReportType.Value.ToString();
                    ShowPanels();
                    if (study.ReportType.Value == Constants.ReportTypes.Manual)
                    {
                        tbHeading.Text = study.Heading;
                        tbDescription.Text = study.Description;
                        tbImpression.Text = study.Impression;
                    }
                    else if (study.ReportType.Value == Constants.ReportTypes.Upload 
                        || study.ReportType.Value == Constants.ReportTypes.Scan)
                    {
                        if (study.AttachmentId.HasValue)
                        {
                            pnlAlreadyUploaded.Visible = true;
                            pnlUpload.Visible = false;
                            pnlScan.Visible = false;
                            hlDownloadReport.NavigateUrl = hlDownloadReport.NavigateUrl += "?attachmentId=" + study.AttachmentId;
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("~/Technologist/AddStudy.aspx");
            }
        }
    }
    private void BindStatusList()
    {
        ddlExamStatus.DataSource = (from s in DatabaseContext.StudyStatusTypes orderby s.ColumnOrder select s);
        ddlExamStatus.DataTextField = "Status";
        ddlExamStatus.DataValueField = "StudyStatusTypeId";
        ddlExamStatus.DataBind();
    }
    private void BindRadiologistPanel(Study study)
    {
        BindRadiologistLists(study);        
    }
    private void BindRadiologistLists(Study study)
    {
        if (study.ClientId.HasValue)
        {
            int clientId = study.ClientId.Value;
            int studyId = study.StudyId;
            var radiologists = (from u in DatabaseContext.StudyUsers
                                where u.StudyId == studyId
                                select u.User);
            lbRadiologists.DataSource = radiologists;
            lbRadiologists.DataBind();

            ddlRadiologist.DataSource = radiologists;
            ddlRadiologist.DataTextField = "Name";
            ddlRadiologist.DataValueField = "UserId";
            ddlRadiologist.DataBind();

            lbNotRadiologists.DataSource = (from u in DatabaseContext.Users
                                            join ur in DatabaseContext.UserRoles on u equals ur.User
                                            join uc in DatabaseContext.UserClients on u equals uc.User
                                            where ur.RoleId == Constants.Roles.Radiologist
                                            && uc.ClientId == clientId
                                            select u).Distinct().Except(radiologists);
            lbNotRadiologists.DataBind();
        }
    }

    protected void btnAddRadiologist_Click(object sender, EventArgs e)
    {
        //Study study = GetStudy();
        if (study != null)
        {
            bool needToSave = false;
            foreach (ListItem li in lbNotRadiologists.Items)
            {
                if (li.Selected)
                {
                    needToSave = true;
                    StudyUser studyUser = new StudyUser();
                    studyUser.StudyId = study.StudyId;
                    studyUser.UserId = int.Parse(li.Value);
                    study.StudyUsers.Add(studyUser);
                }
            }
            if (needToSave)
            {
                DatabaseContext.SaveChanges();
                BindRadiologistLists(study);
            }
        }
    }
    protected void btnRemoveRadiologist_Click(object sender, EventArgs e)
    {
        //Study study = GetStudy();
        if (study != null)
        {
            bool needToSave = false;
            foreach (ListItem li in lbRadiologists.Items)
            {
                if (li.Selected)
                {
                    needToSave = true;
                    int userId = int.Parse(li.Value);
                    if (study.RadiologistId.HasValue && userId == study.RadiologistId.Value)
                    {
                        study.RadiologistId = null;
                    }
                    StudyUser studyUser = (from su in DatabaseContext.StudyUsers
                                           where su.UserId == userId
                                              && su.StudyId == study.StudyId
                                           select su).FirstOrDefault();
                    if (studyUser != null)
                    {
                        DatabaseContext.DeleteObject(studyUser);
                    }
                }
            }
            if (needToSave)
            {
                DatabaseContext.SaveChanges();
                BindRadiologistLists(study);
            }
        }
    }
    protected void ddlRadiologist_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddlRadiologist_DataBound(object sender, EventArgs e)
    {
        ddlRadiologist.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
        if (IsPostBack == false)
        {            
            if (study != null && study.RadiologistId.HasValue)
            {
                ddlRadiologist.SelectedValue = study.RadiologistId.Value.ToString();
            }
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (study != null)
        {
            if (ddlRadiologist.SelectedValue == "0")
            {
                study.RadiologistId = null;
            }
            else
            {
                study.RadiologistId = int.Parse(ddlRadiologist.SelectedValue);
            }
            study.StudyStatusId = int.Parse(ddlExamStatus.SelectedValue);
            study.ReportType = byte.Parse(rblReportType.SelectedValue);
            if (rblReportType.SelectedValue == Constants.ReportTypes.Manual.ToString())
            {
                study.Heading = tbHeading.Text;
                study.Description = tbDescription.Text;
                study.Impression = tbImpression.Text;
                //study.StudyStatusId = Constants.StudyStatusTypes.PendingVerification;
                study.ReportDate = DateTime.Now;
            }
            else if (rblReportType.SelectedValue == Constants.ReportTypes.Upload.ToString())
            {
                if (study.Attachment != null)
                {
                    SetErrorMessage("Report already attached with this Exam");
                }
                else
                {
                    Attachment attachment = new Attachment();
                    attachment.StudyId = study.StudyId;
                    attachment.Name = "REPORT";
                    attachment.Description = "REPORT";
                    attachment.ScannedBy = loggedInUserId;
                    attachment.ScannedTime = DateTime.Now;
                    attachment.AttachmentData = fileAttach.FileBytes;
                    attachment.AttachmentType = "REPORT";
                    study.Attachment = attachment;
                    //study.StudyStatusId = Constants.StudyStatusTypes.PendingVerification;
                    study.ReportDate = DateTime.Now;
                }
            }
            DatabaseContext.SaveChanges();
            Response.Redirect("~/Exams/StudyList.aspx");
        }
    }
    protected void rblReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowPanels();
    }

    private void ShowPanels()
    {
        if (rblReportType.SelectedValue == Constants.ReportTypes.Manual.ToString())
        {
            pnlManual.Visible = true;
            pnlScan.Visible = false;
            pnlUpload.Visible = false;
            pnlAlreadyUploaded.Visible = false;
        }
        else if (study.AttachmentId.HasValue)
        {
            pnlAlreadyUploaded.Visible = true;
            pnlManual.Visible = false;
            pnlScan.Visible = false;
            pnlUpload.Visible = false;
        }
        else if (rblReportType.SelectedValue == Constants.ReportTypes.Upload.ToString())
        {
            pnlManual.Visible = false;
            pnlScan.Visible = false;
            pnlUpload.Visible = true;
            pnlAlreadyUploaded.Visible = false;
        }
        else if (rblReportType.SelectedValue == Constants.ReportTypes.Scan.ToString())
        {
            pnlManual.Visible = false;
            pnlScan.Visible = true;
            pnlUpload.Visible = false;
            pnlAlreadyUploaded.Visible = false;
        }
    }
    protected void btnRemoveReport_Click(object sender, EventArgs e)
    {
        Attachment attachment = study.Attachment;
        study.Attachment = null;
        DatabaseContext.DeleteObject(attachment);
        DatabaseContext.SaveChanges();
        ShowPanels();
    }
    protected void ddlExamStatus_DataBound(object sender, EventArgs e)
    {
        ddlExamStatus.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
    }
}