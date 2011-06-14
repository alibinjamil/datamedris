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

using RIS.Common;

public partial class Admin_AddUser : AuthenticatedPage
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
            ddlCarriers.DataSource = (from c in DatabaseContext.Carriers orderby c.Name select c);
            ddlCarriers.DataTextField = "Name";
            ddlCarriers.DataValueField = "CarrierId";
            ddlCarriers.DataBind();

            ddlRoles.Items.Insert(0, new ListItem("[--Select--]", "0"));
            if (loggedInUserRoleId == Constants.Roles.ClientAdmin)
            {                
                ddlRoles.Items.Add(new ListItem("Hospital Administrator", Constants.Roles.HospitalAdmin.ToString()));
                ddlRoles.Items.Add(new ListItem("My Technologist", Constants.Roles.ClientTechnologist.ToString()));
                ddlRoles.Items.Add(new ListItem("Referring Physician", Constants.Roles.ReferringPhysician.ToString()));
            }
            else if (loggedInUserRoleId == Constants.Roles.HospitalAdmin)
            {
                ddlRoles.Items.Add(new ListItem("Hospital Staff", Constants.Roles.HospitalStaff.ToString()));
                ddlRoles.Items.Add(new ListItem("Referring Physician", Constants.Roles.ReferringPhysician.ToString()));
                
            }
            else if (loggedInUserRoleId == Constants.Roles.Admin)
            {
                
                ddlRoles.Items.Add(new ListItem("Client Administrator", Constants.Roles.ClientAdmin.ToString()));
                ddlRoles.Items.Add(new ListItem("Hospital Administrator", Constants.Roles.HospitalAdmin.ToString()));
                ddlRoles.Items.Add(new ListItem("My Technologist", Constants.Roles.ClientTechnologist.ToString()));                
                ddlRoles.Items.Add(new ListItem("Radiologist", Constants.Roles.Radiologist.ToString()));
                ddlRoles.Items.Add(new ListItem("Referring Physician", Constants.Roles.ReferringPhysician.ToString()));
            }

            

          
            User user = GetUser();
            if (user != null)
            {
                BindLists(user);     

                btnUpdate.Visible = true;
                btnSave.Visible = false;
                pnlChangePassword.Visible = false;
                lbChangePassword.Visible = true;
                pnlHospital.Visible = true;

                tbName.Text = user.Name;
                tbLoginName.Text = user.LoginName;
                cbActive.Checked = user.IsActive;
                cbAllowOthersToViewExam.Checked = user.AllowOthers;

                if (user.SecretQuestionId.HasValue)
                {
                    SecretQuestions1.SecretQuestionId = user.SecretQuestionId.Value; 
                }
                if (user.Answer != null)
                {
                    tbAnswer.Text = user.Answer;
                }
                if (user.ResetPassword.HasValue)
                {
                    cbResetPassword.Checked = user.ResetPassword.Value;
                }

                UserRole userRole = user.UserRoles.FirstOrDefault();
                if (userRole != null)
                {
                    ddlRoles.SelectedValue = userRole.RoleId.ToString() ;
                    if (userRole.RoleId == Constants.Roles.ReferringPhysician)
                    {
                        cbAllowOthersToViewExam.Visible = true;
                    }
                    if (user.SendSMS.HasValue)
                    {
                        cbSms.Checked = (bool)user.SendSMS.Value;
                        if (cbSms.Checked)
                        {
                            rfvCarrier.Enabled = true;
                            rfvCellNumber.Enabled = true;
                        }
                    }
                    if (user.CarrierId.HasValue)
                    {
                        ddlCarriers.SelectedValue = user.CarrierId.Value.ToString();
                    }
                    if (user.Mobile != null)
                    {
                        tbCellNumber.Text = user.Mobile;
                    }
                }
            }
          
        }
        
    }

    private User GetUser()
    {
        if(Request["userId"] != null)
        {
            int userId = int.Parse(Request["userId"]);
            return  (from u in DatabaseContext.Users where u.UserId == userId select u).FirstOrDefault();
        }
        return null;
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
            User user = new User();
            user.Name = tbName.Text;
            user.Password = tbPassword.Text;
            user.LoginName = tbLoginName.Text;
            user.IsActive = cbActive.Checked;
            /*if (ddlClients.SelectedIndex > 0)
            {
                user.ClientId.Value = ddlClients.SelectedValue;
            }*/
            user.Answer = tbAnswer.Text;
            user.SecretQuestionId = SecretQuestions1.SecretQuestionId;
            user.ResetPassword = cbResetPassword.Checked;
            /*if (ddlHospitals.SelectedIndex > 0)
            {
                user.HospitalId.Value = ddlHospitals.SelectedValue;
            }*/
            user.SendSMS = cbSms.Checked;
            user.Mobile = tbCellNumber.Text;
            user.AllowOthers = cbAllowOthersToViewExam.Checked;
            if (ddlCarriers.SelectedValue != "0")
            {
                user.CarrierId = int.Parse(ddlCarriers.SelectedValue);
            }
            user.CreatedBy = loggedInUserId;
            user.CreationDate = DateTime.Now;
            user.LastUpdatedBy = loggedInUserId;
            user.LastUpdateDate = DateTime.Now;

            //user role
            UserRole userRole = new UserRole();                
            userRole.RoleId = int.Parse(ddlRoles.SelectedValue);
            userRole.CreatedBy = loggedInUserId;
            userRole.CreationDate = DateTime.Now;
            userRole.LastUpdateDate = DateTime.Now;
            userRole.LastUpdatedBy = loggedInUserId;
            user.UserRoles.Add(userRole);

            foreach(UserClient loggedInUserClient in loggedInUser.UserClients)
            {
                UserClient userClient = new UserClient();
                userClient.ClientId = loggedInUserClient.ClientId;
                user.UserClients.Add(userClient);
            }
            DatabaseContext.AddToUsers(user);
            DatabaseContext.SaveChanges();
            Response.Redirect("~/Admin/AddUser.aspx?userId=" + user.UserId.ToString());
                        
            SetInfoMessage("User saved successfully");
        }
    }

    private void BindLists(User user)
    {
        var hospitals = (from uh in DatabaseContext.UserHospitals where uh.UserId == user.UserId orderby uh.Hospital.Name select uh.Hospital);
        lbHospitals.DataSource = hospitals;
        lbHospitals.DataTextField = "Name";
        lbHospitals.DataValueField = "HospitalId";
        lbHospitals.DataBind();

        if (loggedInUserRoleId == Constants.Roles.Admin)
        {
            lbNotHospitals.DataSource = (from h in DatabaseContext.Hospitals select h).Except(hospitals).OrderBy(h => h.Name);
        }
        else if (loggedInUserRoleId == Constants.Roles.ClientAdmin)
        {
            int clientId = loggedInUser.UserClients.FirstOrDefault().ClientId;
            lbNotHospitals.DataSource = (from h in DatabaseContext.Hospitals where h.ClientId == clientId select h).Except(hospitals).OrderBy(h => h.Name);
        }
        else
        {
            lbNotHospitals.DataSource = (from uh in DatabaseContext.UserHospitals where uh.UserId == loggedInUserId select uh.Hospital).Except(hospitals).OrderBy(h => h.Name);
        }
        lbNotHospitals.DataTextField = "Name";
        lbNotHospitals.DataValueField = "HospitalId";
        lbNotHospitals.DataBind();
    }
    protected void btnAddHospital_Click(object sender, EventArgs e)
    {
        User user = GetUser();
        if (user != null)
        {
            foreach (ListItem listItem in lbNotHospitals.Items)
            {
                if (listItem.Selected)
                {
                    UserHospital userHospital = new UserHospital();
                    userHospital.HospitalId = int.Parse(listItem.Value);
                    user.UserHospitals.Add(userHospital);
                }
            }
            DatabaseContext.SaveChanges();
            BindLists(user);
        }
    }

    protected void btnRemoveHospital_Click(object sender, EventArgs e)
    {
        User user = GetUser();
        if(user != null)
        {
            foreach (ListItem listItem in lbHospitals.Items)
            {
                if (listItem.Selected)
                {               
                    int hospitalId = int.Parse(listItem.Value);
                    UserHospital userHospital = (from uh in DatabaseContext.UserHospitals where uh.UserId == user.UserId && uh.HospitalId == hospitalId select uh).FirstOrDefault();
                    if(userHospital != null)
                    {
                        DatabaseContext.DeleteObject(userHospital);
                    }
               }
            }
            DatabaseContext.SaveChanges();
            BindLists(user);
        }
        
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (ValidData())
        {
            User user = GetUser();
            if (user != null)
            {
                user.LoginName = tbLoginName.Text;
                
                user.Name = tbName.Text;
                user.IsActive = cbActive.Checked;
                if (pnlChangePassword.Visible == true)
                {
                    user.Password = tbPassword.Text;
                }
                user.Answer = tbAnswer.Text;
                user.SecretQuestionId = SecretQuestions1.SecretQuestionId;
                user.ResetPassword = cbResetPassword.Checked;
                user.SendSMS = cbSms.Checked;
                user.Mobile = tbCellNumber.Text;
                if (ddlCarriers.SelectedValue != "0")
                {
                    user.CarrierId = int.Parse(ddlCarriers.SelectedValue);
                }
                user.UserRoles.FirstOrDefault().RoleId = int.Parse(ddlRoles.SelectedValue);
                user.AllowOthers = cbAllowOthersToViewExam.Checked;
                
                DatabaseContext.SaveChanges();
                SetInfoMessage("User updated successfully");
            }
        }
    }
    private bool ValidData()
    {
        User user = GetUser();
        if (user != null)
        {
            if (lbCancelChangePassword.Visible == true)
            {
                if (tbPassword.Text != tbConfirmPassword.Text)
                {
                    SetErrorMessage("Passwords do not match");
                    return false;
                }
            }
            User anotherUser = (from u in DatabaseContext.Users where u.LoginName == tbLoginName.Text && u.UserId != user.UserId select u).FirstOrDefault();
            if (anotherUser != null)
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
            User anotherUser = (from u in DatabaseContext.Users where u.LoginName == tbLoginName.Text select u).FirstOrDefault();
            if (anotherUser != null)
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
       //cbSms.Visible = (int.Parse(ddlRoles.SelectedValue) == Constants.Roles.ReferringPhysician);
        cbAllowOthersToViewExam.Visible = true;
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
