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
public partial class Admin_AddHospital : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (Request["hospitalId"] != null)
        {
            if (IsPostBack == false)
            {
                HospitalObject hospital = new HospitalObject();
                hospital.HospitalId.Value = Request["hospitalId"];
                hospital.Load(loggedInUserId);
                if (hospital.IsLoaded)
                {
                    if (hospital.Zip.Value != null )
                    {
                        tbZip.Text = hospital.Zip.Value.ToString();
                    }
                    if (hospital.Phone.Value != null)
                    {
                        tbPhone.Text = hospital.Phone.Value.ToString();
                    }
                    if (hospital.Address.Value != null)
                    {
                        tbAddress.Text = hospital.Address.Value.ToString();
                    }
                    if (hospital.City.Value != null)
                    {
                        tbCity.Text = hospital.City.Value.ToString();
                    }
                    if (hospital.ClientId.Value != null)
                    {
                        ddlClients.SelectedValue = hospital.ClientId.Value.ToString();
                    }
                    if (hospital.Code.Value != null)
                    {
                        tbCode.Text = hospital.Code.Value.ToString();
                    }
                    if (hospital.Fax.Value != null)
                    {
                        tbFax.Text = hospital.Fax.Value.ToString();
                    }
                    if (hospital.Name.Value != null)
                    {
                        tbName.Text = hospital.Name.Value.ToString();
                    }
                    if (hospital.State.Value != null)
                    {
                        ddlStates.SelectedValue = hospital.State.Value.ToString();
                    }
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
            }
        }
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
            HospitalObject hospital = new HospitalObject();
            hospital.Name.Value = tbName.Text;
            hospital.Code.Value = tbCode.Text;
            hospital.Address.Value = tbAddress.Text;
            hospital.City.Value = tbCity.Text;
            hospital.ClientId.Value = ddlClients.SelectedValue;
            hospital.Phone.Value = tbPhone.Text;
            hospital.Fax.Value = tbFax.Text;
            hospital.State.Value = ddlStates.SelectedValue;
            hospital.Zip.Value = tbZip.Text;
            hospital.Save(loggedInUserId);

            UserHospitalObject userHospital = new UserHospitalObject();
            userHospital.HospitalId.Value = hospital.HospitalId.Value;
            userHospital.UserId.Value = loggedInUserId;
            userHospital.Save(loggedInUserId);
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
            if (Request["hospitalId"] != null)
            {
                HospitalObject hospital = new HospitalObject();
                hospital.HospitalId.Value = Request["hospitalId"];
                if (hospital.IsLoaded)
                {
                    hospital.Name.Value = tbName.Text;
                    hospital.Code.Value = tbCode.Text;
                    hospital.Address.Value = tbAddress.Text;
                    hospital.City.Value = tbCity.Text;
                    hospital.ClientId.Value = ddlClients.SelectedValue;
                    hospital.Fax.Value = tbFax.Text;
                    hospital.Phone.Value = tbPhone.Text;
                    hospital.State.Value = ddlStates.SelectedValue;
                    hospital.Zip.Value = tbZip.Text;
                    hospital.Save(loggedInUserId);
                }
            }
        }
    }
}
