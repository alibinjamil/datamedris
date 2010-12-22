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

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;

public partial class Main : System.Web.UI.MasterPage
{
    //private int loggedInUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsValidSession)
        {
            lblMessage.Text = "";
            UserObject user = (UserObject)Session[ParameterNames.Session.LoggedInUser];
            lblLoggedInUser.Text = (string)user.Name.Value;
            lblLastLoginTime.Text = (string)Session[ParameterNames.Session.LastLoginTime];
            
            if (Session[ParameterNames.Session.ErrorMessage] != null)
            {
                this.ErrorMessage = (string)Session[ParameterNames.Session.ErrorMessage];
                Session[ParameterNames.Session.ErrorMessage] = null;
            }
            else if (Session[ParameterNames.Session.InformationMessage] != null)
            {
                this.InformationMessage = (string)Session[ParameterNames.Session.InformationMessage];
                Session[ParameterNames.Session.InformationMessage] = null;
            }
            if (Session[ParameterNames.Session.LoggedInUserRoleId] != null)
            {
                int loggedInUserRoleId = (int)Session[ParameterNames.Session.LoggedInUserRoleId];
                if (loggedInUserRoleId == Constants.Roles.Admin)
                {
                    hlScreens.Visible = true;
                }
            }
            if (Session[ParameterNames.Session.LoggedInUserRoleId] != null)
            {
                int loggedInUserRoleId = (int)Session[ParameterNames.Session.LoggedInUserRoleId];
                if (loggedInUserRoleId == Constants.Roles.Technologist)
                {
                    MenuTechnologist.Visible = true;
                }
            }
        }
    }
    public bool IsValidSession
    {
        get
        {
            if (Session[ParameterNames.Session.LoggedInUser] == null)
                return false;
            return true;
        }
    }
    public string ErrorMessage
    {
        set
        {
            lblMessage.Text = value;
            lblMessage.CssClass = "errorText";
        }
    }
    public string InformationMessage
    {
        set
        {
            lblMessage.Text = value;
            lblMessage.CssClass = "informationText";
        }
    }
    protected void logoutLink_Click(object sender, EventArgs e)
    {
        if(Session[ParameterNames.Session.LoggedInUser] != null)
        {
            int loggedInUserId = (int)((UserObject)Session[ParameterNames.Session.LoggedInUser]).UserId.Value;
            LogObject log = new LogObject();
            log.UserId.Value = loggedInUserId;
            log.Action.Value = Constants.LogActions.Logout;
            log.ActionTime.Value = DateTime.Now;
            log.Save();
        }
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        PagesFactory.Transfer(PagesFactory.Pages.LoginPage);
    }
}
