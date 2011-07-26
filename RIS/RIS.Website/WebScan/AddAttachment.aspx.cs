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
public partial class WebScan_AddAttachment : StudyPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        Study study = GetStudy();
        
        if (study == null || study.StudyStatusId.Value == Constants.StudyStatusTypes.Verified)
        {
            Response.Redirect("~/WebScan/AttachmentsList.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId]);
        }            
        
        if (IsPostBack == false)
        {
            hlAddAttachment1.NavigateUrl = GetListURL();
            hlAddAttachment2.NavigateUrl = GetListURL();
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
    protected string GetListURL()
    {
        return "~/WebScan/AttachmentsList.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId];
    }
    protected string GetServerAddress()
    {
        return "";
    }
}
