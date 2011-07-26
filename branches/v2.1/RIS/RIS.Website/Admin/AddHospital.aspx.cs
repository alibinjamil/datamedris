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
public partial class Admin_AddHospital : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            ddlClients.DataSource = (from u in DatabaseContext.UserClients where u.UserId == loggedInUserId select u.Client);
            ddlClients.DataTextField = "Name";
            ddlClients.DataValueField = "ClientId";
            ddlClients.DataBind();

            Hospital hospital = GetHospital();
            if (hospital != null)
            {
                /*if (hospital.Zip != null )
                {*/
                    tbZip.Text = hospital.Zip;
                //}
                /*if (hospital.Phone != null)
                {*/
                    tbPhone.Text = hospital.Phone;
                //}
                /*if (hospital.Address.Value != null)
                {*/
                    tbAddress.Text = hospital.Address;
                //}
                /*if (hospital.City.Value != null)
                {*/
                    tbCity.Text = hospital.City;
                //}
                /*if (hospital.ClientId.Value != null)
                {*/
                    ddlClients.SelectedValue = hospital.ClientId.ToString();
                //}
                /*if (hospital.Code.Value != null)
                {*/
                    tbCode.Text = hospital.Code;
                //}
                /*if (hospital.Fax.Value != null)
                {*/
                    tbFax.Text = hospital.Fax;
                //}
                /*if (hospital.Name.Value != null)
                {*/
                    tbName.Text = hospital.Name;
                //}
                /*if (hospital.State.Value != null)
                {*/
                    ddlStates.SelectedValue = hospital.State;
                //}
                btnSave.Visible = false;
                btnUpdate.Visible = true;
            }
        }
    }

    private Hospital GetHospital()
    {
        if (Request["hospitalId"] != null)
        {
            int hospitalId = int.Parse(Request["hospitalId"]);
            return (from h in DatabaseContext.Hospitals where h.HospitalId == hospitalId select h).FirstOrDefault();
        }
        return null;
    }
    protected override bool IsPopUp()
    {
        return false;
    }

    protected void ddlClients_DataBound(object sender, EventArgs e)
    {
/*        ddlClients.Items.Insert(0, new ListItem("[--Select--]", "0"));
        if (loggedInUserRoleId == Constants.Roles.ClientAdmin ||
            loggedInUserRoleId == Constants.Roles.HospitalAdmin)
        {
            ddlClients.SelectedValue = loggedInUserClientId.ToString();
            ddlClients.Enabled = false;
        }//it should be the case always so sticking with that
        else if (loggedInUserClientId != null )
        {
            ddlClients.SelectedValue = loggedInUserClientId.ToString();
        }*/
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidData())
        {
            Hospital hospital = new Hospital();
            hospital.Name = tbName.Text;
            hospital.Code = tbCode.Text;
            hospital.Address = tbAddress.Text;
            hospital.City = tbCity.Text;
            hospital.ClientId = int.Parse(ddlClients.SelectedValue);
            hospital.Phone = tbPhone.Text;
            hospital.Fax = tbFax.Text;
            hospital.State = ddlStates.SelectedValue;
            hospital.Zip = tbZip.Text;
            hospital.CreatedBy = loggedInUserId;
            hospital.CreationDate = DateTime.Now;
            hospital.LastUpdateDate = DateTime.Now;
            hospital.LastUpdatedBy = loggedInUserId;
            
            UserHospital userHospital = new UserHospital();
            userHospital.UserId = loggedInUserId;
            
            hospital.UserHospitals.Add(userHospital);

            DatabaseContext.AddToHospitals(hospital);
            DatabaseContext.SaveChanges();
        }
    }
    private bool ValidData()
    {
        return true;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ValidData())
        {
            Hospital hospital = GetHospital();
            if (hospital != null)
            {
                hospital.Name = tbName.Text;
                hospital.Code = tbCode.Text;
                hospital.Address = tbAddress.Text;
                hospital.City = tbCity.Text;
                hospital.ClientId = int.Parse(ddlClients.SelectedValue);
                hospital.Fax = tbFax.Text;
                hospital.Phone = tbPhone.Text;
                hospital.State = ddlStates.SelectedValue;
                hospital.Zip = tbZip.Text;
                DatabaseContext.SaveChanges();
            }
        }
    }
}
