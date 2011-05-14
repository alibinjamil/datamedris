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
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;


using RIS.RISLibrary.Utilities;

using RIS.Common;
public partial class Radiologist_EditStudy : StudyPage
{
    private Study study = null;

    protected override bool IsPopUp()
    {
        return true;
    }
   
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (Request["studyId"] != null)
        {
            if (IsPostBack == false)
            {
                hlAddAttachment1.NavigateUrl = GetAddURL();
                //hlAddAttachment2.NavigateUrl = GetAddURL();
                if (loggedInUserRoleId == Constants.Roles.Admin)
                {
                    rfv1.Enabled = false;
                    rfv2.Enabled = false;
                    rfv3.Enabled = false;
                    RequiredFieldValidator1.Enabled = false;
                }
                study = GetStudy();

                if (study != null)
                {
                    BindBodyPart((int)study.ModalityId);
                    BindClientList();
                    if (study.PatientDOB.HasValue)
                    {
                        tbDOB.SelectedDate = study.PatientDOB.Value;
                    }
                    else
                    {
                        tbDOB.SelectedDate = new DateTime(1960, 1, 1);
                    }
                    tbPatientId.Text = study.ExternalPatientId;
                    tbPatientName.Text = study.PatientName;
                    if (study.PatientWeight != null)
                    {
                        tbPatientWeight.Text = study.PatientWeight;
                    }
                    rbGender.SelectedValue = study.PatientGender;
                   

                    if (study.RejectionReason != null)
                    {
                        tbRejectionReason.Text = study.RejectionReason;
                    }
                    if (study.TechComments != null )
                    {
                        tbTechComments.Text = study.TechComments;
                    }
                    if (study.BodyPartId.HasValue )
                    {
                        ddlBodyParts.SelectedValue = study.BodyPartId.Value.ToString();
                    }
                    if (study.StudyStatusId == Constants.StudyStatusTypes.New)
                    {
                        btnUnrelease.Visible = true;
                    }                    
                    if (study.StudyStatusId == Constants.StudyStatusTypes.PreRelease 
                        || study.StudyStatusId == Constants.StudyStatusTypes.Qaed
                        || study.StudyStatusId == Constants.StudyStatusTypes.Rejected)
                    {
                        btnSave.Visible = true;
                        btnRelease.Visible = true;
                        if (loggedInUserRoleId == Constants.Roles.ClientAdmin)
                        {
                            btnHospital.Visible = true;
                        }
                        if (loggedInUserRoleId == Constants.Roles.Admin)
                        {
                            btnRelease.Visible = false;
                        }
                    }

                }
            }            
        }
    }
    private void BindClientList()
    {
        if (loggedInUserRoleId != Constants.Roles.Admin)
        {
            ddlClient.DataSource = (from uc in DatabaseContext.UserClients where uc.UserId == loggedInUserId orderby uc.Client.Name select uc.Client);
        }
        else
        {
            ddlClient.DataSource = (from c in DatabaseContext.Clients orderby c.Name select c);
        }
        ddlClient.DataTextField = "Name";
        ddlClient.DataValueField = "ClientId";
        ddlClient.DataBind();

        
    }
    private void BindHospitalList()
    {
        if (loggedInUserRoleId != Constants.Roles.Admin)
        {
            ddlHospitals.DataSource = (from uh in DatabaseContext.UserHospitals where uh.UserId == loggedInUserId orderby uh.Hospital.Name select uh.Hospital);
        }
        else
        {
            int clientId = int.Parse(ddlClient.SelectedValue);
            ddlHospitals.DataSource = (from h in DatabaseContext.Hospitals where h.ClientId == clientId orderby h.Name select h);
        }
        ddlHospitals.DataTextField = "Name";
        ddlHospitals.DataValueField = "HospitalId";
        ddlHospitals.DataBind();
    }
    protected void ddlHospitals_DataBound(object sender, EventArgs e)
    {
        ddlHospitals.Items.Insert(0,new ListItem("[-- Select --]","-1"));
        if (IsPostBack == false && study != null && study.HospitalId != null )
        {
            ddlHospitals.SelectedValue = study.HospitalId.ToString();
        }
        BindRefPhyList();
    }

    private void BindRefPhyList()
    {
        //if (ddlHospitals.SelectedIndex > 0)
        {
            int hospitalId = int.Parse(ddlHospitals.SelectedValue);
            ddlRefPhy.DataSource = (from uh in DatabaseContext.UserHospitals
                                    where uh.HospitalId == hospitalId
                                    && uh.User.UserRoles.FirstOrDefault().RoleId == Constants.Roles.ReferringPhysician
                                    orderby uh.User.Name
                                    select uh.User);
            ddlRefPhy.DataTextField = "Name";
            ddlRefPhy.DataValueField = "UserId";
            ddlRefPhy.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (loggedInUserRoleId == Constants.Roles.Admin)
        {
            UpdateStudy(null);
        }
        else
        {
            UpdateStudy(Constants.StudyStatusTypes.Qaed);
        }        
    }
    protected void btnHospital_Click(object sender, EventArgs e)
    {
        UpdateStudy(null);
    }
    private void UpdateStudy(Nullable<int> studyStatusId)
    {
        study = GetStudy();
        if (study != null)
        {
            if (ddlClient.SelectedIndex > 0)
            {
                study.ClientId = int.Parse(ddlClient.SelectedValue);
                if (ddlHospitals.SelectedIndex > 0)
                {
                    study.HospitalId = int.Parse(ddlHospitals.SelectedValue);
                    if (ddlRefPhy.SelectedIndex > 0)
                    {
                        study.ReferringPhysicianId = int.Parse(ddlRefPhy.SelectedValue);
                    }
                    else
                    {
                        study.ReferringPhysicianId = null;
                    }
                }
                else
                {
                    study.HospitalId = null;
                }
            }
            else
            {
                study.ClientId = null;
                study.HospitalId = null;
                study.ReferringPhysicianId = null;
            }
            if (ddlBodyParts.SelectedIndex > 0)
            {
                study.BodyPartId = int.Parse(ddlBodyParts.SelectedValue);
            }
            else
            {
                study.BodyPartId = null;
            }
            study.TechComments = tbTechComments.Text;
            if (studyStatusId != null)
            {
                study.StudyStatusId = studyStatusId;
            }

            study.ExternalPatientId = tbPatientId.Text;
            study.PatientDOB = tbDOB.SelectedDate;
            study.PatientGender = rbGender.SelectedValue;
            study.PatientWeight = tbPatientWeight.Text;
            study.PatientName = tbPatientName.Text;
            DatabaseContext.SaveChanges();
            /*RISDatabaseAccessLayer databaseAccessLayer = new RISDatabaseAccessLayer();
            SqlConnection connection = (SqlConnection)databaseAccessLayer.GetConnection();
            connection.Open();
            SqlCommand command = new SqlCommand("sp_insert_study_group", connection);
            command.Parameters.AddWithValue("@studyId", Request["studyId"]);
            command.Parameters.AddWithValue("@hospitalId", ddlHospitals.SelectedValue);
            command.Parameters.AddWithValue("@adminUserId", loggedInUserId);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            connection.Close();*/
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Close", "parent.closeStudyEditWindow();parent.aspnetForm.submit();", true);
    }
    protected void btnRelease_Click(object sender, EventArgs e)
    {
        if (loggedInUserRoleId == Constants.Roles.Admin)
        {
            UpdateStudy(null);
        }
        else
        {
            UpdateStudy(Constants.StudyStatusTypes.New);
        }
    }
    protected void btnUnrelease_Click(object sender, EventArgs e)
    {
        study = GetStudy();
        if (study != null)
        {
            study.StudyStatusId = Constants.StudyStatusTypes.PreRelease;
            DatabaseContext.SaveChanges();
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Close", "parent.closeStudyEditWindow();parent.aspnetForm.submit();", true);
    }
    protected void ddlRefPhy_DataBound(object sender, EventArgs e)
    {
        ddlRefPhy.Items.Insert(0, new ListItem("[-- Select --]", "-1"));
        if (IsPostBack == false && study != null && study.ReferringPhysicianId != null && ddlRefPhy.Items.FindByValue(study.ReferringPhysicianId.ToString()) != null)
        {
                ddlRefPhy.SelectedValue = study.ReferringPhysicianId.ToString();
        }
    }
    protected void ddlClient_DataBound(object sender, EventArgs e)
    {
        ddlClient.Items.Insert(0, new ListItem("[-- Select --]", "-1"));
        if (IsPostBack == false && study != null && study.ClientId != null)
        {
            ddlClient.SelectedValue = study.ClientId.ToString();
        }
        if (loggedInUserRoleId != Constants.Roles.Admin)
        {
            ddlClient.Enabled = false;
        }
        BindHospitalList();
    }
    private void BindBodyPart(int modalityId)
    {
        ddlBodyParts.DataSource = (from bp in DatabaseContext.BodyParts orderby bp.Name where bp.ModalityId == modalityId select bp);
        ddlBodyParts.DataTextField = "Name";
        ddlBodyParts.DataValueField = "BodyPartId";
        ddlBodyParts.DataBind();
    }
    protected string GetAddURL()
    {
        return "~/WebScan/AddAttachment.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId];
    }
    protected void ddlBodyParts_DataBound(object sender, EventArgs e)
    {
        ddlBodyParts.Items.Insert(0, new ListItem("[-- Select --]"));
    }
    protected void ddlHospitals_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRefPhyList();
    }
    protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindHospitalList();
    }
}
