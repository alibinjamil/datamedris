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
using System.Xml;
using System.Data.SqlClient;

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;
using RIS.RISLibrary.Database;

public partial class Radiologist_EditFinding : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            tbDescription.Text = "";
            tbHeading.Text = "";
            tbImpression.Text = "";

            if (Request[ParameterNames.Request.StudyId] != null)
            {
                
                hlAttach.NavigateUrl = "~/WebScan/AttachmentsList.aspx?" + ParameterNames.Request.StudyId + "=" + Request[ParameterNames.Request.StudyId];
                StudyObject study = new StudyObject();
                study.StudyId.Value = Request[ParameterNames.Request.StudyId];
                study.Load(loggedInUserId);
                if(study.IsLoaded)
                {
                    if(study.TechComments.Value != null)
                    {
                        techComments.Text = study.TechComments.Value.ToString();
                    }
                    if (loggedInUserRoleId == Constants.Roles.Radiologist && int.Parse(study.StudyStatusId.Value.ToString()) != Constants.StudyStatusTypes.Verified)
                    {
                        btnSave.Visible = true;
                        btnVerify.Visible = true;
                        btnReject.Visible = true;
                        ddlBodyParts.Visible = true;
                        ddlTemplates.Visible = true;
                        btnApplyTemplate.Visible = true;
                        BindBodyPart((int)study.ModalityId.Value);
                        if (study.BodyPartExamined.Value != null)
                        {
                            ddlBodyParts.SelectedValue = study.BodyPartExamined.Value.ToString();
                            BindTemplate((int)study.ModalityId.Value, ddlBodyParts.SelectedValue);
                            if (study.TemplateId.Value != null)
                            {
                                ddlTemplates.SelectedValue = study.TemplateId.Value.ToString();
                            }
                        }
                    }

                    lblExamDate.Text = study.StudyDate.Value.ToString();
                    ModalityObject modality = new ModalityObject();
                    modality.ModalityId.Value = study.ModalityId.Value;
                    modality.Load(loggedInUserId);
                    if (modality.IsLoaded)
                    {
                        lblModality.Text = modality.Name.Value.ToString();
                    }

                    PatientObject patient = new PatientObject();
                    patient.PatientId.Value = study.PatientId.Value;
                    patient.Load(loggedInUserId);
                    if (patient.IsLoaded)
                    {
                        lblPatientId.Text = patient.ExternalPatientId.Value.ToString();
                        lblPatientName.Text = patient.Name.Value.ToString();
                    }

                    if (study.StudyStatusId.Value != null)
                    {
                        StudyStatusTypeObject studyStatus = new StudyStatusTypeObject();
                        studyStatus.StudyStatusTypeId.Value = study.StudyStatusId.Value;
                        studyStatus.Load(loggedInUserId);
                        if(studyStatus.IsLoaded)
                        {
                            lblStatus.Text = studyStatus.Status.Value.ToString();
                        }
                    }

                    UserObject physician = new UserObject();
                    physician.UserId.Value = study.ReferringPhysicianId.Value;
                    physician.Load(loggedInUserId);
                    if (physician.IsLoaded)
                    {
                        lblPhysician.Text = physician.Name.Value.ToString();
                    }

                    if (study.ProcedureId.Value != null)
                    {
                        ProcedureObject procedure = new ProcedureObject();
                        procedure.ProcedureId.Value = study.ProcedureId.Value;
                        procedure.Load(loggedInUserId);
                        if (procedure.IsLoaded)
                        {
                            lblProcedure.Text = procedure.Name.Value.ToString();
                        }
                    }

                    if (study.LatestFindingId.Value != null)
                    {
                        FindingObject finding = new FindingObject();
                        finding.FindingId.Value = study.LatestFindingId.Value;
                        finding.Load(loggedInUserId);
                        if (finding.IsLoaded)
                        {
                            UserObject radiologist = new UserObject();
                            radiologist.UserId.Value = finding.AudioUserId.Value;
                            radiologist.Load(loggedInUserId);
                            if (radiologist.IsLoaded)
                            {
                                lblRadiologist.Text = radiologist.Name.Value.ToString();
                            }

                            if (finding.TextualTranscript != null)
                            {
                                string heading = "";
                                string description = "";
                                string impression = "";
                                try
                                {
                                    XmlDocument xDoc = new XmlDocument();
                                    xDoc.LoadXml(finding.TextualTranscript.Value.ToString());
                                    heading = xDoc.ChildNodes[0].ChildNodes[0].InnerText;
                                    description = xDoc.ChildNodes[0].ChildNodes[1].InnerText;
                                    impression = xDoc.ChildNodes[0].ChildNodes[2].InnerText;
                                }
                                catch (Exception ex)
                                {
                                    description = finding.TextualTranscript.Value.ToString();
                                }
                                tbHeading.Text = heading;
                                tbDescription.Text = description;
                                tbImpression.Text = impression;
                            }
                        }
                    }
                }                
            }
        }
    }
    private void BindBodyPart(int modalityId)
    {
        RISDatabaseAccessLayer db = new RISDatabaseAccessLayer();
        string query = "SELECT '[-- Select --]' AS BodyPart UNION select DISTINCT BodyPart AS BodyPart from tTemplates "
            + " inner join tTemplateUsers on tTemplates.TemplateId = tTemplateUsers.TemplateId "
            + " where tTemplateUsers.UserId = " + loggedInUserId
            + " and tTemplates.ModalityId = " + modalityId ;

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

    private void BindTemplate(int modalityId,string bodyPart)
    {
        RISDatabaseAccessLayer db = new RISDatabaseAccessLayer();
        string query = "select tTemplates.TemplateId,tTemplates.[Name] from tTemplates "
            + " inner join tTemplateUsers on tTemplates.TemplateId = tTemplateUsers.TemplateId "
            + " where tTemplateUsers.UserId = " + loggedInUserId
            + " and tTemplates.BodyPart = '" + bodyPart +"'"
            + " and tTemplates.ModalityId = '" + modalityId + "'";

        SqlConnection con = (SqlConnection)db.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        da.SelectCommand = cmd;
        da.Fill(dt);

        //Populating Drop down list of templates
        if (dt.Rows.Count > 0)
        {
            ddlTemplates.DataSource = dt;
            ddlTemplates.DataTextField = "Name";
            ddlTemplates.DataValueField = "TemplateId";
            ddlTemplates.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateStudy(Constants.StudyStatusTypes.PendingVerification);
        ClientScript.RegisterStartupScript(this.GetType(), "CloseFinding", "parent.document.aspnetForm.submit();",true);
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        StudyObject study = UpdateStudy(Constants.StudyStatusTypes.Verified);
        HospitalObject hospital = new HospitalObject();
        hospital.HospitalId.Value = study.HospitalId.Value;
        hospital.Load();
        //creat pdf report
        string filePath = ReportGenerator.Instance.Generate(int.Parse(Request[ParameterNames.Request.StudyId]));
        //fax the pdf report
        if (filePath != null)
        {            
            if(hospital.IsLoaded && hospital.Fax.Value != null)
            {
                string name = (hospital.Name.Value == null)? "":(string)hospital.Name.Value; 
                FaxSender.Instance.SendFax(name,name,(string)hospital.Fax.Value,filePath);
            }
        }
        //send sms to ref phy
        if (study.ReferringPhysicianId.Value != null && study.ClientId.Value != null)
        {
            UserObject refPhy = new UserObject();
            refPhy.UserId.Value = study.ReferringPhysicianId.Value;
            refPhy.Load(loggedInUserId);
            if (refPhy.IsLoaded && refPhy.SendSMS.Value != null && (bool)refPhy.SendSMS.Value)
            {
                string clientURL = "www.datamedusa.com";
                string hospitalName = "DataMed";
                ClientObject client = new ClientObject();
                client.ClientId.Value = study.ClientId.Value;
                client.Load();
                if (client.IsLoaded && client.Website.Value != null)
                {
                    clientURL = (string)client.Website.Value;
                }
                if (hospital.IsLoaded)
                {
                    hospitalName = (string)hospital.Name.Value;
                }
                EmailSender.Instance.SendSMS((int)refPhy.CarrierId.Value,(string)refPhy.Mobile.Value, hospitalName, clientURL);
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "CloseFinding", "parent.document.aspnetForm.submit();", true);
    }

    private StudyObject UpdateStudy(Nullable<int> studyStatusId)
    {
        StudyObject study = null;
        if (Request[ParameterNames.Request.StudyId] != null)
        {
            study = new StudyObject();
            study.StudyId.Value = Request[ParameterNames.Request.StudyId];
            study.Load(loggedInUserId);
            if (study.IsLoaded)
            {
                FindingObject finding = new FindingObject();
                if (study.LatestFindingId.Value != null)
                {
                    finding.FindingId.Value = study.LatestFindingId.Value;
                    finding.Load();
                }
                if(finding.IsLoaded == false)
                {
                    finding.StudyId.Value = study.StudyId.Value;
                }
                //adding this code to put in radiologist is and name. 
                finding.AudioDate.Value = DateTime.Now;
                finding.AudioUserId.Value = loggedInUserId;
                if (loggedInUser != null)
                {
                    finding.AudioUserName.Value = loggedInUser.Name.Value;
                }

                if (loggedInUserRoleId == Constants.Roles.Transcriptionist)
                {
                    finding.TranscriptUserId.Value = loggedInUserId;
                    finding.TranscriptionDate.Value = DateTime.Now;
                }
                finding.TextualTranscript.Value = "<data><heading>" + tbHeading.Text + "</heading><description>" + tbDescription.Text + "</description><impression>"
                    + tbImpression.Text + "</impression></data>";
                finding.Save(loggedInUserId);
                study.LatestFindingId.Value = finding.FindingId.Value;
                if (studyStatusId != null)
                {
                    study.StudyStatusId.Value = studyStatusId;
                }
                study.Save(loggedInUserId);
            }
        }
        return study;
    }
    protected override bool IsPopUp()
    {
        return true;
    }
    protected void ddlBodyParts_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBodyParts.SelectedIndex > 0)
        {
            if(Request[ParameterNames.Request.StudyId] != null)
            {
                StudyObject study = new StudyObject();
                study.StudyId.Value = Request[ParameterNames.Request.StudyId];
                study.Load(loggedInUserId);
                if(study.IsLoaded)
                {
                    BindTemplate((int)study.ModalityId.Value, ddlBodyParts.SelectedValue);
                }
            }            
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            int templateId = int.Parse(ddlTemplates.SelectedValue);

            StudyObject study = new StudyObject();
            study.StudyId.Value = Request[ParameterNames.Request.StudyId];
            study.Load();
            if (study.IsLoaded)
            {
                study.BodyPartExamined.Value = ddlBodyParts.SelectedValue;
                study.TemplateId.Value = ddlTemplates.SelectedValue;
                study.Update(loggedInUserId);
            }

            TemplateObject template = new TemplateObject();
            template.TemplateId.Value = templateId;
            template.Load(loggedInUserId);
            if (template.IsLoaded)
            {
                if (template.Heading.Value != null)
                {
                    tbHeading.Text = template.Heading.Value.ToString();
                }
                if (template.Description.Value != null)
                {
                    tbDescription.Text = template.Description.Value.ToString();
                }
                if (template.Impression.Value != null)
                {
                    tbImpression.Text = template.Impression.Value.ToString();
                }
            }
        }
        catch
        {
        }
    }
}
