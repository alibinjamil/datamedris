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
        if (IsPostBack == false)
        {
            StudyDetailObject studyDetail = new StudyDetailObject();
            studyDetail.StudyId.Value = Request[ParameterNames.Request.StudyId];
            studyDetail.Load(loggedInUserId);
            if (studyDetail.IsLoaded)
            {
                tbTechComments.Text = studyDetail.TechComments.Value.ToString();
            }
        }
        //lblStudyId.Text = Request[ParameterNames.Request.StudyId];
    }
    protected override bool IsPopUp()
    {
        return false;
    }
    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        StudyGroupObject studyGroup = new StudyGroupObject();
        studyGroup.StudyId.Value = int.Parse(Request[ParameterNames.Request.StudyId]);
        studyGroup.GroupId.Value = int.Parse(ddlGroups.SelectedValue);
        studyGroup.Load(Constants.Database.NullUserId);
        if (studyGroup.IsLoaded == false)
        {
            studyGroup.Save(loggedInUserId);
            gvGroups.DataBind();
            ddlGroups.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        StudyDetailObject studyDetail = new StudyDetailObject();
        studyDetail.StudyId.Value = Request[ParameterNames.Request.StudyId];
        studyDetail.Load(loggedInUserId);
        studyDetail.TechComments.Value = tbTechComments.Text;
        studyDetail.Save(loggedInUserId);
    }
}
