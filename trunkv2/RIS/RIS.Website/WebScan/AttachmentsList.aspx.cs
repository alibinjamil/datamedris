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

using RIS.Common;

using RIS.RISLibrary.Utilities;
public partial class WebScan_AttachmentsList : StudyPage
{
    private Study study = null;
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (Request[ParameterNames.Request.StudyId] == null)
        {
            PagesFactory.Transfer(PagesFactory.Pages.ErrorPage);
        }
        else
        {
            study = GetStudy();
            hlAddAttachment1.Visible = CanUpdate();
            hlAddAttachment2.Visible = CanUpdate();
            BindGrid();
        }
        if (IsPostBack == false)
        {
            hlAddAttachment1.NavigateUrl = GetAddURL();
            hlAddAttachment2.NavigateUrl = GetAddURL();
        }
    }
    private void BindGrid()
    {
        if (study != null)
        {
            GridView1.DataSource = (from a in DatabaseContext.Attachments
                                    where a.StudyId == study.StudyId
                                    && a.AttachmentType != "REPORT"
                                    select a);
            GridView1.DataBind();
        }
    }
    protected override bool IsPopUp()
    {
        return false;
    }
    protected string GetHeaderURL()
    {
        HttpCookie cookie = Request.Cookies[ParameterNames.Cookie.ClientId];
        if (cookie != null)
        {
            return "../Images/" + cookie.Value + "_Header.jpg";
        }
        else
        {
            return "../Images/Datamed_Header.jpg";
        }
    }
    protected string GetAddURL()
    {
        return "~/WebScan/AddAttachment.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId];
    }
    protected bool CanUpdate()
    {
        return (study != null && study.StudyStatusId.Value != Constants.StudyStatusTypes.Verified);
    }
    protected string Test(Attachment att)
    {
        return "";
    }
}
