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

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;
using RIS.RISLibrary.Utilities;

public partial class SharedPages_ResetPassword : GenericPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("~/SharedPages/Login.aspx");
        }
    }
    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        if (tbPassword.Text == tbConfirmPassword.Text)
        {
            UserObject user = new UserObject();
            user.UserId.Value = (int)Session["userId"];
            user.Load();
            if (user.IsLoaded)
            {
                user.SecretQuestionId.Value = SecretQuestions1.SecretQuestionId;
                user.Answer.Value = tbAnswer.Text;
                user.Password.Value = tbPassword.Text;


                Session.Add(ParameterNames.Session.LoggedInUser, user);
                Session.Add(ParameterNames.Session.LoggedInUserId, user.UserId.Value);
                //Session.Add(ParameterNames.Session.LoggedInUserClientId, user.ClientId.Value);
                if (user.LastLoginDate.Value != null)
                    Session.Add(ParameterNames.Session.LastLoginTime, user.LastLoginDate.Value.ToString());
                else
                    Session.Add(ParameterNames.Session.LastLoginTime, DateTime.Now.ToString());
                user.LastLoginDate.Value = DateTime.Now;
                user.ResetPassword.Value = false;
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
            SetErrorMessage("Passwords do not match");
        }
    }
}
