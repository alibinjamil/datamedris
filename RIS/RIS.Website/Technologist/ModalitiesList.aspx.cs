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

public partial class Technologist_ModalitiesList : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        LabelError.Visible = false;
    }
    protected override bool IsPopUp()
    {
        return false;
    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            LabelError.Visible = true;
            e.ExceptionHandled = true;
        }
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        ObjectDataSourceModalities.InsertParameters["Name"].DefaultValue = TextBoxName.Text;
        ObjectDataSourceModalities.InsertParameters["CreatedBy"].DefaultValue = ((int)Session[ParameterNames.Session.LoggedInUserId]).ToString();
        ObjectDataSourceModalities.Insert();
    }
}
