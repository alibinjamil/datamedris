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
using RIS.RISLibrary.Utilities;
using RIS.RISLibrary.Objects.RIS;

public partial class SharedPages_SelectRole : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            BindList();
        }
    }
    protected override bool IsPopUp()
    {
        return false;
    }

    private void BindList()
    {
        RISDatabaseAccessLayer dataAccess = new RISDatabaseAccessLayer();
        SqlConnection connection = (SqlConnection)dataAccess.GetConnection();
        connection.Open();
        SqlCommand command = new SqlCommand("sp_get_roles_for_user", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@userId", loggedInUserId);
        SqlDataReader reader = command.ExecuteReader();
        ddlRoles.DataSource = reader;
        ddlRoles.DataMember = "Name";
        ddlRoles.DataTextField = "Name";
        ddlRoles.DataValueField = "RoleId";
        ddlRoles.DataBind();
        connection.Close();
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        int roleId = Int32.Parse(ddlRoles.SelectedValue);
        Session[ParameterNames.Session.LoggedInUserRoleId] = roleId;
        PagesFactory.TransferAfterLogin(roleId);
    }
}
