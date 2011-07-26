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
        if (IsPostBack == false)
        {
            if (loggedInUserRoleId == Constants.Roles.Admin)
            {
                ddlClients.DataSource = (from c in DatabaseContext.Clients select c);
            }
            else
            {
                ddlClients.DataSource = (from u in DatabaseContext.UserClients where u.UserId == loggedInUserId select u.Client);
            }
            ddlClients.DataTextField = "Name";
            ddlClients.DataValueField = "ClientId";
            ddlClients.DataBind();

            BindGrid();
        }
    }
    private void BindGrid()
    {
        //int clientId = int.Parse(ddlClients.SelectedValue);
        //gvHospitals.DataSource = (from h in DatabaseContext.Hospitals where h.ClientId == clientId select h);
        gvHospitals.DataBind();
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
    protected void ddlClients_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    /*protected void gvHospitals_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvHospitals_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHospitals.PageIndex = e.NewPageIndex;
        gvHospitals.DataBind();
    }
    protected void gvHospitals_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = gvHospitals.DataSource as DataTable;

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

            gvHospitals.DataSource = dataView;
            gvHospitals.DataBind();
        }
    }
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }*/
}
