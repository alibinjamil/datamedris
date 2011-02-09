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
public partial class Admin_UsersList : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (loggedInUserRoleId != Constants.Roles.ClientAdmin
            && loggedInUserRoleId != Constants.Roles.Admin
            && loggedInUserRoleId != Constants.Roles.HospitalAdmin)
        {
            PagesFactory.Transfer(PagesFactory.Pages.LoginPage);
        }
        if (IsPostBack == false)
        {
            if (loggedInUserRoleId == Constants.Roles.ClientAdmin)
            {
                ddlRoles.Items.Add(new ListItem("My Technologist", Constants.Roles.ClientTechnologist.ToString()));
                ddlRoles.Items.Add(new ListItem("Hospital Administrator", Constants.Roles.HospitalAdmin.ToString()));
                ddlRoles.Items.Add(new ListItem("Referring Physician", Constants.Roles.ReferringPhysician.ToString()));
            }
            else if (loggedInUserRoleId == Constants.Roles.HospitalAdmin)
            {
                ddlRoles.Items.Add(new ListItem("Referring Physician", Constants.Roles.ReferringPhysician.ToString()));
                ddlRoles.Items.Add(new ListItem("Hospital Staff", Constants.Roles.HospitalStaff.ToString()));
            }
            else if (loggedInUserRoleId == Constants.Roles.Admin)
            {
                ddlRoles.Items.Insert(0, new ListItem("[--Select--]", "0"));     
                ddlRoles.Items.Add(new ListItem("Client Administrator", Constants.Roles.ClientAdmin.ToString()));
                ddlRoles.Items.Add(new ListItem("My Technologist", Constants.Roles.ClientTechnologist.ToString()));
                ddlRoles.Items.Add(new ListItem("Hospital Administrator", Constants.Roles.HospitalAdmin.ToString()));
                ddlRoles.Items.Add(new ListItem("Radiologist", Constants.Roles.Radiologist.ToString()));
                ddlRoles.Items.Add(new ListItem("Referring Physician", Constants.Roles.ReferringPhysician.ToString()));
            }
            ddlClients.DataSource = (from u in DatabaseContext.UserClients where u.UserId == loggedInUserId select u.Client);
            ddlClients.DataTextField = "Name";
            ddlClients.DataValueField = "ClientId";
            ddlClients.DataBind();

            ddlHospitals.DataSource = (from u in DatabaseContext.UserHospitals where u.UserId == loggedInUserId select u.Hospital);
            ddlHospitals.DataTextField = "Name";
            ddlHospitals.DataValueField = "HospitalId";
            ddlHospitals.DataBind();

            BindGrid();
        }
    }
    protected override bool IsPopUp()
    {
        return false;
    }

    private void BindGrid()
    {
        int clientId = int.Parse(ddlClients.SelectedValue);
        int hospitalId = int.Parse(ddlHospitals.SelectedValue);
        int roleId = int.Parse(ddlRoles.SelectedValue);
        gvUsers.DataSource = (from u in DatabaseContext.Users
                              join uh in DatabaseContext.UserHospitals on u equals uh.User
                              join uc in DatabaseContext.UserClients on u equals uc.User
                              join ur in DatabaseContext.UserRoles on u equals ur.User
                              where uh.HospitalId == hospitalId
                                 && uc.ClientId == clientId
                                 && ur.RoleId == roleId
                              select u);
        gvUsers.DataBind();
    }
    protected void ddlClients_DataBound(object sender, EventArgs e)
    {
        //ddlClients.Items.Insert(0,new ListItem("[--Select--]","0"));
        /*if (loggedInUserRoleId == Constants.Roles.ClientAdmin ||
            loggedInUserRoleId == Constants.Roles.HospitalAdmin)
        {
            ddlClients.SelectedValue = loggedInUserClientId.ToString();
            ddlClients.Enabled = false;
        }*/
    }
    protected void ddlHospitals_DataBound(object sender, EventArgs e)
    {
        /*ddlHospitals.Items.Insert(0, new ListItem("[--Select--]", "0"));
        if (loggedInUserRoleId == Constants.Roles.HospitalAdmin)
        {
            ddlHospitals.SelectedValue = loggedInUserHospitalId.ToString();
            ddlHospitals.Enabled = false;
        }*/
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}
