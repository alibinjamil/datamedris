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
public partial class Admin_AddUser : AuthenticatedPage
{
    UserObject user = null;
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
            ddlRoles.Items.Insert(0, new ListItem("[--Select--]", "0"));
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
                
                ddlRoles.Items.Add(new ListItem("Client Administrator", Constants.Roles.ClientAdmin.ToString()));
                ddlRoles.Items.Add(new ListItem("My Technologist", Constants.Roles.ClientTechnologist.ToString()));
                ddlRoles.Items.Add(new ListItem("Hospital Administrator", Constants.Roles.HospitalAdmin.ToString()));
                ddlRoles.Items.Add(new ListItem("Radiologist", Constants.Roles.Radiologist.ToString()));
                ddlRoles.Items.Add(new ListItem("Referring Physician", Constants.Roles.ReferringPhysician.ToString()));
            }
            if (Request["userId"] != null)
            {
                user = new UserObject();
                user.UserId.Value = Request["userId"];
                user.Load(loggedInUserId);
                if (user.IsLoaded)
                {
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                    pnlChangePassword.Visible = false;
                    lbChangePassword.Visible = true;
                    pnlHospital.Visible = true;

                    tbName.Text = user.Name.Value.ToString();
                    tbLoginName.Text = user.LoginName.Value.ToString();
                    cbActive.Checked = (bool)user.IsActive.Value;


                    if (user.SecretQuestionId.Value != null)
                    {
                        SecretQuestions1.SecretQuestionId = (int)user.SecretQuestionId.Value;
                    }
                    if (user.Answer.Value != null)
                    {
                        tbAnswer.Text = (string)user.Answer.Value;
                    }
                    if (user.ResetPassword.Value != null)
                    {
                        cbResetPassword.Checked = (bool)user.ResetPassword.Value;
                    }

                    UserRoleObject userRole = new UserRoleObject();
                    userRole.UserId.Value = user.UserId.Value;
                    userRole.Load(loggedInUserId);
                    if (userRole.IsLoaded)
                    {
                        ddlRoles.SelectedValue = userRole.RoleId.Value.ToString() ;
                        if (user.SendSMS.Value != null)
                        {
                            cbSms.Checked = (bool)user.SendSMS.Value;
                            if (cbSms.Checked)
                            {
                                rfvCarrier.Enabled = true;
                                rfvCellNumber.Enabled = true;
                            }
                        }
                        if (user.CarrierId.Value != null)
                        {
                            ddlCarriers.SelectedValue = user.CarrierId.Value.ToString();
                        }
                        if (user.Mobile.Value != null)
                        {
                            tbCellNumber.Text = (string)user.Mobile.Value;
                        }
                    }
                }
            }

        }
        
    }
    protected override bool IsPopUp()
    {
        return false;
    }
    /*protected void ddlClients_DataBound(object sender, EventArgs e)
    {
        ddlClients.Items.Insert(0, new ListItem("[--Select--]", "0"));
        if (loggedInUserRoleId == Constants.Roles.ClientAdmin ||
            loggedInUserRoleId == Constants.Roles.HospitalAdmin)
        {
            ddlClients.SelectedValue = loggedInUserClientId.ToString();
            ddlClients.Enabled = false;
        }//it should be the case always so sticking with that
        else if (user != null && user.ClientId.Value != null)
        {
            ddlClients.SelectedValue = user.ClientId.Value.ToString();
        }
    }*/
    protected void ddlHospitals_DataBound(object sender, EventArgs e)
    {
        /*ddlHospitals.Items.Insert(0, new ListItem("[--Select--]", "0"));
        if (loggedInUserRoleId == Constants.Roles.HospitalAdmin)
        {
            ddlHospitals.SelectedValue = loggedInUserHospitalId.ToString();
            ddlHospitals.Enabled = false;
        }
        else if (user != null && user.HospitalId.Value != null)
        {
            ddlHospitals.SelectedValue = user.HospitalId.Value.ToString();
        }*/
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidData())
        {
            UserObject user = new UserObject();
            user.Name.Value = tbName.Text;
            user.Password.Value = tbPassword.Text;
            user.LoginName.Value = tbLoginName.Text;
            user.IsActive.Value = cbActive.Checked;
            /*if (ddlClients.SelectedIndex > 0)
            {
                user.ClientId.Value = ddlClients.SelectedValue;
            }*/
            user.Answer.Value = tbAnswer.Text;
            user.SecretQuestionId.Value = SecretQuestions1.SecretQuestionId;
            user.ResetPassword.Value = cbResetPassword.Checked;
            /*if (ddlHospitals.SelectedIndex > 0)
            {
                user.HospitalId.Value = ddlHospitals.SelectedValue;
            }*/
            user.SendSMS.Value = cbSms.Checked;
            user.Mobile.Value = tbCellNumber.Text;
            if (ddlCarriers.SelectedValue != "0")
            {
                user.CarrierId.Value = ddlCarriers.SelectedValue;
            }


            user.Save(loggedInUserId);
            if (user.IsLoaded)
            {
                UserRoleObject userRole = new UserRoleObject();
                userRole.UserId.Value = user.UserId.Value;
                userRole.RoleId.Value = ddlRoles.SelectedValue;
                userRole.Save(loggedInUserId);
                UsersTableAdapters.tUsersTableAdapter userTA = new UsersTableAdapters.tUsersTableAdapter();
                userTA.AssignClientsToUser(int.Parse(user.UserId.Value.ToString()),loggedInUserId);
                /*
                UsersTableAdapters.tUsersTableAdapter userTA = new UsersTableAdapters.tUsersTableAdapter();
                if (user.HospitalId.Value != null)
                {
                    Nullable<int> userId = int.Parse(user.UserId.Value.ToString());
                    Nullable<int> hospitalId = int.Parse(user.HospitalId.Value.ToString());
                    userTA.AssignHospitalToUser(userId, hospitalId, false, loggedInUserId);
                }
                else
                {
                    userTA.AssignHospitalToUser((Nullable<int>)user.UserId.Value, null, false, loggedInUserId);
                }
                */
                Response.Redirect("~/Admin/AddUser.aspx?userId=" + user.UserId.Value);
            }
            
            SetInfoMessage("User saved successfully");
        }
    }
    protected void btnAddHospital_Click(object sender, EventArgs e)
    {
        foreach (ListItem listItem in lbNotHospitals.Items)
        {
            if (listItem.Selected)
            {
                UserHospitalObject userHospital = new UserHospitalObject();
                userHospital.UserId.Value = Request["userId"];
                userHospital.HospitalId.Value = listItem.Value;
                userHospital.Save();
            }
        }
        lbHospitals.DataBind();
        lbNotHospitals.DataBind();
    }

    protected void btnRemoveHospital_Click(object sender, EventArgs e)
    {
        foreach (ListItem listItem in lbHospitals.Items)
        {
            if (listItem.Selected)
            {
                UserHospitalObject userHospital = new UserHospitalObject();
                userHospital.UserId.Value = Request["userId"];
                userHospital.HospitalId.Value = listItem.Value;
                userHospital.Load(loggedInUserId);
                if (userHospital.IsLoaded)
                {
                    userHospital.Remove(loggedInUserId);                
                }
            }
        }
        lbHospitals.DataBind();
        lbNotHospitals.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ValidData())
        {
            UserObject user = new UserObject();
            user.UserId.Value = Request["userId"];
            user.Load(loggedInUserId);
            if (user.IsLoaded)
            {
                user.LoginName.Value = tbLoginName.Text;
                /*if(ddlClients.SelectedIndex > 0)
                {
                    user.ClientId.Value = ddlClients.SelectedValue;
                }
                else
                {
                    user.ClientId.Value = null;
                }*/

                /*if (ddlHospitals.SelectedIndex > 0)
                {
                    user.HospitalId.Value = ddlHospitals.SelectedValue;
                }
                else
                {
                    user.HospitalId.Value = null;
                }
                */
                user.Name.Value = tbName.Text;
                user.IsActive.Value = cbActive.Checked;
                if (pnlChangePassword.Visible == true)
                {
                    user.Password.Value = tbPassword.Text;
                }
                user.Answer.Value = tbAnswer.Text;
                user.SecretQuestionId.Value = SecretQuestions1.SecretQuestionId;
                user.ResetPassword.Value = cbResetPassword.Checked;
                user.SendSMS.Value = cbSms.Checked;
                user.Mobile.Value = tbCellNumber.Text;
                if (ddlCarriers.SelectedValue != "0")
                {
                    user.CarrierId.Value = ddlCarriers.SelectedValue;
                }
                user.Update(loggedInUserId);
                if (user.IsLoaded)
                {
                    UserRoleObject userRole = new UserRoleObject();
                    userRole.UserId.Value = user.UserId.Value;
                    userRole.Load(loggedInUserId);
                    if (userRole.IsLoaded)
                    {
                        userRole.RoleId.Value = ddlRoles.SelectedValue;
                        userRole.Update(loggedInUserId);
                    }
                    
                    //this is not good. we should remove
                
                    UsersTableAdapters.tUsersTableAdapter userTA = new UsersTableAdapters.tUsersTableAdapter();
                    /*if (user.HospitalId.Value != null)
                    {
                        userTA.AssignHospitalToUser(int.Parse(user.UserId.Value.ToString()), int.Parse(user.HospitalId.Value.ToString()), true, loggedInUserId);
                    }
                    else
                    {
                        userTA.AssignHospitalToUser(int.Parse(user.UserId.Value.ToString()), null, true, loggedInUserId);
                    }*/
                }
                SetInfoMessage("User updated successfully");
            }
        }
    }
    private bool ValidData()
    {
        if (Request["userId"] != null)
        {
            if (lbCancelChangePassword.Visible == true)
            {
                if (tbPassword.Text != tbConfirmPassword.Text)
                {
                    SetErrorMessage("Passwords do not match");
                    return false;
                }
            }
            UserObject anotherUserObject = new UserObject();
            anotherUserObject.LoginName.Value = tbLoginName.Text;
            anotherUserObject.Load(loggedInUserId);
            if (anotherUserObject.IsLoaded && anotherUserObject.UserId.Value.ToString().Equals(Request["userId"]) == false)
            {
                SetErrorMessage("User with the given user name already exists");
                return false;
            }
        }
        else
        {
            if (tbPassword.Text != tbConfirmPassword.Text)
            {
                SetErrorMessage("Passwords do not match");
                return false;
            }
            UserObject anotherUserObject = new UserObject();
            anotherUserObject.LoginName.Value = tbLoginName.Text;
            anotherUserObject.Load(loggedInUserId);
            if (anotherUserObject.IsLoaded)
            {
                SetErrorMessage("User with the given user name already exists");
                return false;
            }
        }
        return true;
    }
    protected void lbChangePassword_Click(object sender, EventArgs e)
    {
        pnlChangePassword.Visible = true;
        lbCancelChangePassword.Visible = true;
        lbChangePassword.Visible = false;
    }
    protected void lbCancelChangePassword_Click(object sender, EventArgs e)
    {
        pnlChangePassword.Visible = false;
        lbCancelChangePassword.Visible = false;
        lbChangePassword.Visible = true;
        
    }
    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
       cbSms.Visible = (int.Parse(ddlRoles.SelectedValue) == Constants.Roles.ReferringPhysician);
    }
    
    protected void cbSms_CheckedChanged(object sender, EventArgs e)
    {
        //pnlRefPhy.Visible = cbSms.Checked;
        rfvCarrier.Enabled = cbSms.Checked;
        rfvCellNumber.Enabled = cbSms.Checked;
    }
    protected void ddlCarriers_DataBound(object sender, EventArgs e)
    {
        ddlCarriers.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
    }
}
