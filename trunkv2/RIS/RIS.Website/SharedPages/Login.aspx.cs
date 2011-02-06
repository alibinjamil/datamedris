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

using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

using RIS.RISLibrary.Database;
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;

public partial class SharedPages_Login : GenericPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        var user = (from u in DatabaseContext.Users
                    where u.LoginName == tbLoginName.Text
                    && u.Password == tbPassword.Text
                    select u).FirstOrDefault();
        if (user != null)
        {
            RIS.Common.Log log = new RIS.Common.Log();
            log.UserId = user.UserId; 
            log.Action = Constants.LogActions.Login;
            log.ActionTime = DateTime.Now;
            DatabaseContext.AddToLogs(log);
            DatabaseContext.SaveChanges();
            if (user.SecretQuestionId == null || user.Answer == null
                || user.ResetPassword == null || user.ResetPassword == true)
            {
                Session.Add("userId", user.UserId);
                Response.Redirect("~/SharedPages/ResetPassword.aspx");
            }
            else
            {
                Session.Add(ParameterNames.Session.LoggedInUser, user);
                Session.Add(ParameterNames.Session.LoggedInUserId, user.UserId);
                //Session.Add(ParameterNames.Session.LoggedInUserClientId, user.ClientId.Value);
                if (user.LastLoginDate.Value != null)
                    Session.Add(ParameterNames.Session.LastLoginTime, user.LastLoginDate.Value.ToString());
                else
                    Session.Add(ParameterNames.Session.LastLoginTime, DateTime.Now.ToString());
                user.LastLoginDate = DateTime.Now;
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
            ((Common_Login)this.Master).SetErrorMessage(Messages.Error.InValidUserNamePassword);            
        }
    }

}