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
            int userId = (int)Session["userId"];
            User user = (from u in DatabaseContext.Users where u.UserId == userId select u).FirstOrDefault();
            if (user != null)
            {
                user.SecretQuestionId = SecretQuestions1.SecretQuestionId;
                user.Answer = tbAnswer.Text;
                user.Password = tbPassword.Text;


                Session.Add(ParameterNames.Session.LoggedInUser, user);
                Session.Add(ParameterNames.Session.LoggedInUserId, user.UserId);
                //Session.Add(ParameterNames.Session.LoggedInUserClientId, user.ClientId.Value);
                if (user.LastLoginDate.Value != null)
                    Session.Add(ParameterNames.Session.LastLoginTime, user.LastLoginDate.Value.ToString());
                else
                    Session.Add(ParameterNames.Session.LastLoginTime, DateTime.Now.ToString());
                user.LastLoginDate = DateTime.Now;
                user.ResetPassword = false;                                
                DatabaseContext.SaveChanges();

                int numOfRoles = user.UserRoles.Count;
                if (numOfRoles == 0)
                {
                    Session[ParameterNames.Session.ExceptionString] = Messages.Error.NoRolesDefined;
                    Session[ParameterNames.Session.LoggedInUser] = null;
                    PagesFactory.Transfer(PagesFactory.Pages.ErrorPage);
                }
                else if (numOfRoles == 1)
                {
                    int roleId = user.UserRoles.First().RoleId;
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
