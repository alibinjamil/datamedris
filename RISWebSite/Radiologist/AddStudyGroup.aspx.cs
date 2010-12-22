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
using RIS.RISLibrary.Utilities;
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;

public partial class Radiologist_AddStudyGroup : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        lblStudyId.Text = Request[ParameterNames.Request.StudyId];
        if (IsPostBack == false)
        {
            if (Request[ParameterNames.Request.StudyGroupId] != null)
            {
                int studyGroupId = int.Parse(Request[ParameterNames.Request.StudyGroupId]);
                StudyGroupObject studyGroup = new StudyGroupObject();
                studyGroup.StudyGroupId.Value = studyGroupId;
                studyGroup.Remove(loggedInUserId);
            }
            BindData();
        }
    }
    protected override bool IsPopUp()
    {
        return true;
    }
    private void BindData()
    {
        int studyId = int.Parse(Request[ParameterNames.Request.StudyId]);
        RISDatabaseAccessLayer dataAccess = new RISDatabaseAccessLayer();
        SqlConnection connection = (SqlConnection)dataAccess.GetConnection();
        connection.Open();
        SqlCommand command = new SqlCommand("sp_get_groups_for_study", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@studyId", studyId);
        command.Parameters.AddWithValue("@assigned", 0);
        SqlDataReader reader = null;
        if (loggedInUserRoleId == Constants.Roles.Admin)
        {
            ddlGroups.Visible = true;
            btnAdd.Visible = true;
            reader = command.ExecuteReader();
            ddlGroups.DataSource = reader;
            ddlGroups.DataMember = "Name";
            ddlGroups.DataTextField = "Name";
            ddlGroups.DataValueField = "StudyGroupId";
            ddlGroups.DataBind();
            reader.Close();
        }
        else
        {
            ddlGroups.Visible = false;
            btnAdd.Visible = false;
        }
        command.Parameters["@assigned"].Value = 1;
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            TableRow currentRow = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.CssClass = "dataCell";
            cell1.Text = reader.GetString(1);
            currentRow.Cells.Add(cell1);
            TableCell cell2 = new TableCell();
            if (loggedInUserRoleId == Constants.Roles.Admin)
            {
                string url = PagesFactory.GetUrl(PagesFactory.Pages.AddStudyGroupPage);
                url += "?" + ParameterNames.Request.StudyId + "=" + studyId.ToString();
                url += "&" + ParameterNames.Request.StudyGroupId + "=" + reader.GetInt32(0).ToString();
                cell2.Text = "<a href=\"" + url + "\" >[Remove]</a>";
            }
            else
            {
                cell2.Text = "&nbsp;";
            }
            cell2.CssClass = "dataCell";
            currentRow.Cells.Add(cell2);
            dataTable.Rows.Add(currentRow);
        }
        reader.Close();
        connection.Close();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlGroups.SelectedIndex > 0)
        {
            StudyGroupObject studyGroup = new StudyGroupObject();
            studyGroup.StudyId.Value = int.Parse(Request[ParameterNames.Request.StudyId]);
            studyGroup.GroupId.Value = int.Parse(ddlGroups.SelectedValue);
            studyGroup.Load(Constants.Database.NullUserId);
            if (studyGroup.IsLoaded == false)
            {
                studyGroup.Save(loggedInUserId);
                BindData();
            }
        }
    }
}
