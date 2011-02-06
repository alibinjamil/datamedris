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

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;
using RIS.RISLibrary.Utilities;

public partial class AdminPages_ManageUsers : AuthenticatedPage
{
    public static class Params
    {
        public static string UserId = "UserId";
    }
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
       
    }
    protected override bool IsPopUp()
    {
        return false;
    }

}
