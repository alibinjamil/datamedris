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

using RIS.RISLibrary.Objects.RIS;
public partial class AdminPages_AddGroup : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            if (Request[ParameterNames.Request.GroupId] != null)
            {
                GroupObject group = new GroupObject();
                group.GroupId.Value = Request[ParameterNames.Request.GroupId];
                group.Load(loggedInUserId);
                if (group.IsLoaded)
                {
                    tbName.Text = group.Name.Value.ToString();
                    tbDesc.Text = group.Name.Value.ToString();

                }
            }
        }
    }
    protected override bool IsPopUp()
    {
        return false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        GroupObject group = new GroupObject();

        if (Request[ParameterNames.Request.GroupId] != null)
        {
            group.GroupId.Value = int.Parse(Request[ParameterNames.Request.GroupId]);
            group.Load(loggedInUserId);
        }
        group.Name.Value = tbName.Text;
        group.Description.Value = tbDesc.Text;
        group.IsDefault.Value = false;
        group.Save(loggedInUserId);

        foreach (ListItem li in lbUsers.Items)
        {
            UserGroupObject userGroup = new UserGroupObject();
            userGroup.GroupId.Value = group.GroupId.Value;
            userGroup.UserId.Value = li.Value;
            userGroup.Load(loggedInUserId);
            if (userGroup.IsLoaded == false)
            {
                userGroup.Save(loggedInUserId);
            }
        }
    }
    private void MoveItems(ListBox from, ListBox to)
    {
        ArrayList removedItems = new ArrayList();
        foreach (ListItem item in from.Items)
        {
            if (item.Selected)
            {
                to.Items.Add(item);
                removedItems.Add(item);
            }
        }
        foreach (ListItem item in removedItems)
        {
            from.Items.Remove(item);
        }
    }
    protected void btnAddRole_Click(object sender, EventArgs e)
    {
        MoveItems(lbOtherUsers, lbUsers);
    }
    protected void btnRemoveRole_Click(object sender, EventArgs e)
    {
        MoveItems(lbUsers, lbOtherUsers);
    }
}
