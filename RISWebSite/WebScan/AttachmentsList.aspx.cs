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

public partial class WebScan_AttachmentsList : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (Request[ParameterNames.Request.StudyId] == null)
        {
            PagesFactory.Transfer(PagesFactory.Pages.ErrorPage);
        }
        if (IsPostBack == false)
        {
            hlAddAttachment1.NavigateUrl = GetAddURL();
            hlAddAttachment2.NavigateUrl = GetAddURL();
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
}
