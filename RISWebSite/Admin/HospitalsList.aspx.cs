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
using RIS.RISLibrary.Utilities;
using RIS.RISLibrary.Objects.RIS;
public partial class Admin_HospitalsList : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        
    }
    protected override bool IsPopUp()
    {
        return false;
    }
    protected void ddlClients_DataBound(object sender, EventArgs e)
    {
        /*ddlClients.Items.Insert(0, new ListItem("[--Select--]", "0"));
        if (loggedInUserRoleId == Constants.Roles.ClientAdmin ||
            loggedInUserRoleId == Constants.Roles.HospitalAdmin)
        {
            ddlClients.SelectedValue = loggedInUserClientId.ToString();
            ddlClients.Enabled = false;
        }//it should be the case always so sticking with that
        else if (loggedInUserClientId != null)
        {
            ddlClients.SelectedValue = loggedInUserClientId.ToString();
        }*/
    }
}
