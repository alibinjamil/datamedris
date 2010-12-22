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

using RIS.RISLibrary.Utilities;

public partial class Common_Popup : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public bool IsValidSession
    {
        get
        {
            if (Session[ParameterNames.Session.LoggedInUser] == null)
            {
                Session[ParameterNames.Session.ExceptionString] = Messages.Error.YouMustBeLoggedIn;
                PagesFactory.Transfer(PagesFactory.Pages.ErrorPage);
            }
            return true;
        }
    }
}
