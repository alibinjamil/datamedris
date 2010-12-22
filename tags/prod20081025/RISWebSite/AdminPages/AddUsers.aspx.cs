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

public partial class AdminPages_AddUsers : System.Web.UI.Page
{
    public static class Params
    {
        public static string UserId = "UserId";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            if (Request[Params.UserId] != null)
            {
                lbUserId.Text = Request[Params.UserId];
                
                UserObject user = new UserObject();
                user.UserId.Value = int.Parse(lbUserId.Text.Trim());
                user.Load();
                if (user.IsLoaded)
                {
                    tbLoginName.Text = user.LoginName.Value.ToString();
                    tbLoginName.Enabled = false;
                    tbPassword.Text = user.Password.Value.ToString();
                    tbConfirmPassword.Text = user.Password.Value.ToString();
                    string strFirstName = "";
                    string strLastName = "";
                    RISUtility.GetFirstLastName(user.Name.Value.ToString(), ref strFirstName, ref strLastName);
                    tbFirstName.Text = strFirstName.Trim();
                    tbLastName.Text = strLastName.Trim();
                    cbIsActive.Checked = (user.IsActive.Value.ToString() == "Y" ? true : false);

                }
                RISDatabaseAccessLayer dataAccess = new RISDatabaseAccessLayer();
                SqlConnection connection = (SqlConnection)dataAccess.GetConnection();
                connection.Open();
                SqlCommand command = new SqlCommand("sp_get_roles_for_user", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userId", int.Parse(Request[Params.UserId]));

                BindListBox(command, lbUserRoles);
                SqlCommand command2 = new SqlCommand("sp_get_roles_not_for_user", connection);
                command2.CommandType = CommandType.StoredProcedure;
                command2.Parameters.AddWithValue("@userId", int.Parse(Request[Params.UserId]));
                BindListBox(command2, lbOtherRoles);
                connection.Close();
            }
            else
            {
                fillAllRolesList();
            }
        }
    }
    private void fillAllRolesList()
    {
        
        RISDatabaseAccessLayer dataAccess = new RISDatabaseAccessLayer();
        SqlConnection connection = (SqlConnection)dataAccess.GetConnection();
        connection.Open();
        SqlCommand command = new SqlCommand("select RoleId,Name from tRoles", connection);
        BindListBox(command, lbOtherRoles);
        connection.Close();
    }
    private void BindListBox(SqlCommand command, ListBox lb)
    {
        SqlDataReader reader = command.ExecuteReader();
        lb.DataSource = reader;
        lb.DataMember = "Name";
        lb.DataTextField = "Name";
        lb.DataValueField = "RoleId";
        lb.DataBind();
        reader.Close();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        UserObject user = new UserObject();
        if (tbPassword.Text.Trim().Equals(tbConfirmPassword.Text.Trim()) == false)
        {
            ((Main)this.Master).ErrorMessage = Messages.Error.PasswordsDoNotMatch;
            return;
        }
        if (lbUserId.Text.Length == 0)
        {
            user.LoginName.Value = tbLoginName.Text.Trim();
            user.Load();
            if (user.IsLoaded)
            {
                ((Main)this.Master).ErrorMessage = Messages.Error.LoginNameAlreadyExists;
                return;
            }
        }
        else
        {
            user.UserId.Value = int.Parse(lbUserId.Text.Trim());
            user.Load();
            if (user.IsLoaded == false)
            {
                Session[ParameterNames.Session.ExceptionString] = Messages.Exception.UserIdNotFound;
                PagesFactory.Transfer(PagesFactory.Pages.ErrorPage);
            }
    
        }
        user.Password.Value = tbPassword.Text.Trim();
        user.Name.Value = RISUtility.GetFullName(tbFirstName.Text.Trim(), tbLastName.Text.Trim());
        user.IsActive.Value = (cbIsActive.Checked)?"Y":"N";
        if (lbUserId.Text.Length == 0)
            user.Save();
        else
            user.Update((int)user.UserId.Value);

        foreach (ListItem item in lbUserRoles.Items)
        {
            UserRoleObject userRole = new UserRoleObject();
            userRole.UserId.Value = user.UserId.Value;
            userRole.RoleId.Value = int.Parse(item.Value);
            if (lbUserId.Text.Length > 0)
            {
                userRole.Load();
                if (userRole.IsLoaded == false)
                {
                    userRole.Save();
                }
            }
            else
            {
                userRole.Save();
            }
        }
        
        //if (lbUserId.Text.Length > 0)
        //{
        //    foreach (ListItem item in lbOtherRoles.Items)
        //    {
        //        UserRoleObject userRole = new UserRoleObject();
        //        userRole.UserId.Value = user.UserId.Value;
        //        userRole.RoleId.Value = int.Parse(item.Value);
        //        userRole.Load();
        //        if (userRole.IsLoaded == true)
        //        {
        //            userRole.Remove(0);
        //        }
        //    }
        //}
        if (lbUserId.Text.Length == 0)
        {
            ((Main)this.Master).InformationMessage = Messages.Information.DataSaved;
        }
        else
        {
            ((Main)this.Master).InformationMessage = Messages.Information.DataUpdated ;
        }
        lbUserId.Text = "";
        tbLoginName.Text = "";
        tbLoginName.Enabled = true;
        tbPassword.Text = "";
        tbConfirmPassword.Text = "";
        tbFirstName.Text = "";
        tbLastName.Text = "";
        lbOtherRoles.Items.Clear();
        lbUserRoles.Items.Clear();
        fillAllRolesList();
        //int count = lbUserRoles.Items.Count;
        //for(int i=0;i<count;i++)
        //    lbUserRoles.Items.RemoveAt(0);
        cbIsActive.Checked = false;
    }
    protected void btnAddRole_Click(object sender, EventArgs e)
    {
        MoveItems(lbOtherRoles, lbUserRoles);
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
    protected void btnRemoveRole_Click(object sender, EventArgs e)
    {
        MoveItems(lbUserRoles,lbOtherRoles);
    }
}
