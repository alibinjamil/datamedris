using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using RIS.RISLibrary.Utilities;
using RIS.RISLibrary.Objects.RIS;
/// <summary>
/// Summary description for AuthenticatedPage
/// </summary>
public abstract class AuthenticatedPage : System.Web.UI.Page
{
	public AuthenticatedPage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    protected abstract void Page_Load_Extended(object sender, EventArgs e);
    protected abstract bool IsPopUp();
    protected UserObject loggedInUser = null;
    protected int loggedInUserId = 0;
    protected int loggedInUserRoleId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Refresh the expiration on the user's authentication ticket
        //try
        {
            PagesFactory.InstantiatePageMap();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (Session[ParameterNames.Session.LoggedInUser] == null)
            {
                if (IsPopUp())
                {
                    //Session[ParameterNames.Session.ExceptionString] = Messages.Error.YouMustBeLoggedIn;
                    PagesFactory.Transfer(PagesFactory.Pages.ErrorPage);
                }
                else
                {
                    //Session[ParameterNames.Session.ErrorMessage] = Messages.Error.YouMustBeLoggedIn;
                    PagesFactory.Transfer(PagesFactory.Pages.LoginPage);
                }
            }
            else
            {
                loggedInUser = (UserObject)Session[ParameterNames.Session.LoggedInUser];
                loggedInUserId = (int)loggedInUser.UserId.Value;
                if (Session[ParameterNames.Session.LoggedInUserRoleId] != null)
                {
                    loggedInUserRoleId = (int)Session[ParameterNames.Session.LoggedInUserRoleId];
                }
                Page_Load_Extended(sender, e);
            }
        }
        /*catch (Exception ex)
        {
            Session[ParameterNames.Session.ExceptionString] = ex.ToString();
            PagesFactory.Transfer(Constants.Pages.ErrorPage);
        }*/
    }
    
}
