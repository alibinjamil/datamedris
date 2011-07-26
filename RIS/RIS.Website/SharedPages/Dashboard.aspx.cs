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

public partial class SharedPages_Dashboard : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        int debug = 0;
        if(loggedInUserRoleId == Constants.Roles.Admin
            || loggedInUserRoleId == Constants.Roles.ClientAdmin)
        {
            hlAddHospital.Visible = true;
            hlAddUser.Visible = true;
            hlViewHospital.Visible = true;
            hlViewUser.Visible = true;
        }
        else if (loggedInUserRoleId == Constants.Roles.HospitalAdmin
            || loggedInUserRoleId == Constants.Roles.ClientTechnologist)
        {
            hlAddUser.Visible = true;
            hlViewUser.Visible = true;
        }
        else if (loggedInUserRoleId == Constants.Roles.Radiologist)
        {
            hlAddTemplate.Visible = true;
            hlTemplateList.Visible = true;
        }
    }
    protected override bool IsPopUp()
    {
        return false;
    }
}
