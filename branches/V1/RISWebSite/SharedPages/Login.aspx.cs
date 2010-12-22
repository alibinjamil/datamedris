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
using System.Data.SqlClient;

using RIS.RISLibrary.Database;
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;

public partial class SharedPages_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        UserObject user = new UserObject();
        user.LoginName.Value = tbLoginName.Text;
        user.Password.Value = tbPassword.Text;
        user.IsActive.Value = true;
        user.Load(Constants.Database.NullUserId);
        if (user.IsLoaded)
        {
            LogObject log = new LogObject();
            log.UserId.Value = user.UserId.Value;
            log.Action.Value = Constants.LogActions.Login;
            log.ActionTime.Value = DateTime.Now;
            log.Save();
            if (user.SecretQuestionId.Value == null || user.Answer.Value == null
                || user.ResetPassword.Value == null || (bool)user.ResetPassword.Value)
            {
                Session.Add("userId", user.UserId.Value);
                Response.Redirect("~/SharedPages/ResetPassword.aspx");
            }
            else
            {
                Session.Add(ParameterNames.Session.LoggedInUser, user);
                Session.Add(ParameterNames.Session.LoggedInUserId, user.UserId.Value);
                //Session.Add(ParameterNames.Session.LoggedInUserClientId, user.ClientId.Value);
                if (user.LastLoginDate.Value != null)
                    Session.Add(ParameterNames.Session.LastLoginTime, user.LastLoginDate.Value.ToString());
                else
                    Session.Add(ParameterNames.Session.LastLoginTime, DateTime.Now.ToString());
                user.LastLoginDate.Value = DateTime.Now;
                user.Save();
                int numOfRoles = RISProcedureCaller.GetRoleCount((int)user.GetPrimaryKey().Value);
                if (numOfRoles == 0)
                {
                    Session[ParameterNames.Session.ExceptionString] = Messages.Error.NoRolesDefined;
                    Session[ParameterNames.Session.LoggedInUser] = null;
                    PagesFactory.Transfer(PagesFactory.Pages.ErrorPage);
                }
                else if (numOfRoles == 1)
                {
                    UserRoleObject userRole = new UserRoleObject();
                    userRole.UserId.Value = user.GetPrimaryKey().Value;
                    userRole.Load(Constants.Database.NullUserId);
                    int roleId = (int)userRole.RoleId.Value;
                    Session[ParameterNames.Session.LoggedInUserRoleId] = roleId;
                    PagesFactory.TransferAfterLogin(roleId);
                }
                else
                {
                    PagesFactory.Transfer(PagesFactory.Pages.SelectRolePage);
                }
            }
        }
        else
        {
            ((Common_Login)this.Master).SetErrorMessage(Messages.Error.InValidUserNamePassword);            
        }
    }

}