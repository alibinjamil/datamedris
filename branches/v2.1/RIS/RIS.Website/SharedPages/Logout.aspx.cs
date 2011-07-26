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
public partial class SharedPages_Logout : AuthenticatedPage
{
    protected override bool IsPopUp()
    {
        return false;
    }
    
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session[ParameterNames.Session.LoggedInUser] = null;
        Session[ParameterNames.Session.LoggedInUserRoleId] = null;
        PagesFactory.Transfer(PagesFactory.Pages.LoginPage);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Session[ParameterNames.Session.LoggedInUser] == null)
        {
            PagesFactory.Transfer(PagesFactory.Pages.LoginPage);
        }
        else if (Session[ParameterNames.Session.LoggedInUserRoleId] == null)
        {
            PagesFactory.Transfer(PagesFactory.Pages.SelectRolePage);
        }
        else
        {
            int roleId = (int)Session[ParameterNames.Session.LoggedInUserRoleId];
            PagesFactory.TransferAfterLogin(roleId);
        }
    }
}
