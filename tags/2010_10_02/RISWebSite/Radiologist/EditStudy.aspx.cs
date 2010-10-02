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

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;
using RIS.RISLibrary.Utilities;
public partial class Radiologist_EditStudy : AuthenticatedPage
{
    private StudyObject study = null;
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
                study = new StudyObject();
                study.StudyId.Value = Request["studyId"];
                study.Load();
                if (study.IsLoaded)
                {
                    BindBodyPart((int)study.ModalityId.Value);
                    if (study.RejectionReason.Value != null)
                    {
                        tbRejectionReason.Text = study.RejectionReason.Value.ToString();
                    }
                    if (study.TechComments != null && study.TechComments.Value != null)
                    {
                        tbTechComments.Text = study.TechComments.Value.ToString();
                    }
                    if (study.BodyPartExamined != null && study.BodyPartExamined.Value != null)
                    {
                        ddlBodyParts.SelectedValue = (string)study.BodyPartExamined.Value;
                    }
                    if ((int)study.StudyStatusId.Value == Constants.StudyStatusTypes.New)
                    {
                        btnUnrelease.Visible = true;
                    }                    
                    if ((int)study.StudyStatusId.Value == Constants.StudyStatusTypes.PreRelease 
                        || (int)study.StudyStatusId.Value == Constants.StudyStatusTypes.Qaed
                        || (int)study.StudyStatusId.Value == Constants.StudyStatusTypes.Rejected)
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
    protected void ddlHospitals_DataBound(object sender, EventArgs e)
    {
        ddlHospitals.Items.Insert(0,new ListItem("[-- Select --]","-1"));
        if (study != null && study.IsLoaded && study.HospitalId != null && study.HospitalId.Value != null)
        {
            ddlHospitals.SelectedValue = study.HospitalId.Value.ToString();
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
        StudyObject study = new StudyObject();
        study.StudyId.Value = Request["studyId"];
        study.Load();
        if (study.IsLoaded)
        {
            if (ddlClient.SelectedIndex > 0)
            {
                study.ClientId.Value = ddlClient.SelectedValue;
                if (ddlHospitals.SelectedIndex > 0)
                {
                    study.HospitalId.Value = ddlHospitals.SelectedValue;
                    if (ddlRefPhy.SelectedIndex > 0)
                    {
                        study.ReferringPhysicianId.Value = ddlRefPhy.SelectedValue;
                    }
                    else
                    {
                        study.ReferringPhysicianId.Value = null;
                    }
                }
                else
                {
                    study.HospitalId.Value = null;
                }
            }
            else
            {
                study.ClientId.Value = null;
                study.HospitalId.Value = null;
                study.ReferringPhysicianId.Value = null;
            }
            if (ddlBodyParts.SelectedIndex > 0)
            {
                study.BodyPartExamined.Value = ddlBodyParts.SelectedValue;
            }
            else
            {
                study.BodyPartExamined.Value = null;
            }
            study.TechComments.Value = tbTechComments.Text;
            if (studyStatusId != null)
            {
                study.StudyStatusId.Value = studyStatusId.Value;
            }
            study.Update(loggedInUserId);
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
        StudyObject study = new StudyObject();
        study.StudyId.Value = Request["studyId"];
        study.Load();
        if (study.IsLoaded)
        {
            study.StudyStatusId.Value = Constants.StudyStatusTypes.PreRelease;
            study.Save();
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Close", "parent.closeStudyEditWindow();parent.aspnetForm.submit();", true);
    }
    protected void ddlRefPhy_DataBound(object sender, EventArgs e)
    {
        ddlRefPhy.Items.Insert(0, new ListItem("[-- Select --]", "-1"));
        if (study != null && study.IsLoaded && study.ReferringPhysicianId != null && study.ReferringPhysicianId.Value != null 
            && ddlRefPhy.Items.FindByValue(study.ReferringPhysicianId.Value.ToString()) != null)
        {
                ddlRefPhy.SelectedValue = study.ReferringPhysicianId.Value.ToString();
        }
    }
    protected void ddlClient_DataBound(object sender, EventArgs e)
    {
        ddlClient.Items.Insert(0, new ListItem("[-- Select --]", "-1"));
        if (study != null && study.IsLoaded && study.ClientId != null && study.ClientId.Value != null)
        {
            ddlClient.SelectedValue = study.ClientId.Value.ToString();
        }
        if (loggedInUserRoleId != Constants.Roles.Admin)
        {
            ddlClient.Enabled = false;
        }
    }
    private void BindBodyPart(int modalityId)
    {
        RISDatabaseAccessLayer db = new RISDatabaseAccessLayer();
        string query = "SELECT '[-- Select --]' AS BodyPart UNION select DISTINCT BodyPart AS BodyPart from tTemplates "
            + " WHERE tTemplates.ModalityId = " + modalityId;

        SqlConnection con = (SqlConnection)db.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        da.SelectCommand = cmd;
        da.Fill(dt);

        //Populating Drop down list of templates
        if (dt.Rows.Count > 0)
        {
            ddlBodyParts.DataSource = dt;
            ddlBodyParts.DataTextField = "BodyPart";
            ddlBodyParts.DataValueField = "BodyPart";
            ddlBodyParts.DataBind();
        }
    }
    protected string GetAddURL()
    {
        return "~/WebScan/AddAttachment.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId];
    }
}
