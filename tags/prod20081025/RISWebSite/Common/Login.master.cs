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

public partial class Common_Login : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PagesFactory.InstantiatePageMap();
        lblMessage.Text = "";
        if (Session[ParameterNames.Session.ErrorMessage] != null)
        {
            SetErrorMessage((string)Session[ParameterNames.Session.ErrorMessage]);
            Session[ParameterNames.Session.ErrorMessage] = null;
        }
        else if (Session[ParameterNames.Session.InformationMessage] != null)
        {
            SetInformationMessage((string)Session[ParameterNames.Session.ErrorMessage]);
            Session[ParameterNames.Session.InformationMessage] = null;
        } 
    }

    public void SetErrorMessage(string message)
    {
        lblMessage.Text = message;
        lblMessage.CssClass = "errorText";
    }
    public void SetInformationMessage(string message)
    {
        lblMessage.Text = message;
        lblMessage.CssClass = "informationText";
    }

}
