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

using RIS.RISLibrary.Utilities;
using RIS.RISLibrary.Objects.RIS;

public partial class Radiologist_Finding : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            lblStudyId.Value = Request[ParameterNames.Request.StudyId];
            if (IsFindingInRequest)
                lblFindingId.Value = Request[ParameterNames.Request.FindingId];
            ShowButtons();

            StudyObject study = new StudyObject();
            study.StudyId.Value = int.Parse(Request[ParameterNames.Request.StudyId]);
            study.Load(loggedInUserId);
            lblExamDate.Text = study.StudyDate.Value.ToString();
            PatientObject patient = new PatientObject();
            patient.PatientId.Value = study.PatientId.Value;
            patient.Load(loggedInUserId);
            lblPatientId.Text = (string)patient.ExternalPatientId.Value;
            lblPatientName.Text = (string)patient.Name.Value;
            StudyStatusTypeObject studyStatusType = new StudyStatusTypeObject();
            studyStatusType.StudyStatusTypeId.Value = study.StudyStatusId.Value;
            studyStatusType.Load(loggedInUserId);
            lblStatus.Text = (string)studyStatusType.Status.Value;
            ProcedureObject procedure = new ProcedureObject();
            procedure.ProcedureId.Value = study.ProcedureId.Value;
            procedure.Load(loggedInUserId);
            if (procedure.IsLoaded)
            {
                lblProcedure.Text = (string)procedure.Name.Value;
            }
            ModalityObject modality = new ModalityObject();
            modality.ModalityId.Value = procedure.ModalityId.Value;
            modality.Load(loggedInUserId);
            lblModality.Text = (string)modality.Name.Value;
            if (study.ReferringPhysicianId.Value != null)
            {
                UserObject referringPhysician = new UserObject();
                referringPhysician.UserId.Value = study.ReferringPhysicianId.Value;
                referringPhysician.Load(loggedInUserId);
                lblPhysician.Text = (string)referringPhysician.Name.Value;
            }
            FindingObject finding = new FindingObject();
            if (IsFindingInRequest)
            {
                finding.GetPrimaryKey().Value = int.Parse(Request[ParameterNames.Request.FindingId]);
                finding.Load(loggedInUserId);
            }
            else if (study.LatestFindingId.Value != null)
            {
                finding.FindingId.Value = study.LatestFindingId.Value;
                finding.Load(loggedInUserId);
            }
            if (finding.IsLoaded)
            {
                tbTrancription.Text = (string)finding.TextualTranscript.Value;
                if (finding.AudioUserId.Value != null)
                {
                    UserObject radiologist = new UserObject();
                    radiologist.UserId.Value = finding.AudioUserId.Value;
                    radiologist.Load(loggedInUserId);
                    lblRadiologist.Text = (string)radiologist.Name.Value;
                }
            }
            int studyStatusId = (int)study.StudyStatusId.Value;

            LogObject log = new LogObject();
            log.UserId.Value = loggedInUserId;
            log.StudyId.Value = study.StudyId.Value;
            log.PatientId.Value = study.PatientId.Value;
            log.Action.Value = Constants.LogActions.ViewedStudy;
            log.ActionTime.Value = DateTime.Now;
            log.Save();
        }        
    }

    private bool IsFindingInRequest
    {
        get
        {
            if(Request[ParameterNames.Request.FindingId] == null || Request[ParameterNames.Request.FindingId].Length == 0 || Request[ParameterNames.Request.FindingId] == "0")
                return false;
            return true;
        }
    }

    protected override bool IsPopUp()
    {
        return true;
    }
    protected void ShowButtons()
    {
        StudyObject study = new StudyObject();
        study.GetPrimaryKey().Value = int.Parse(lblStudyId.Value);
        study.Load(loggedInUserId);
        int studyStatusId = (int)study.StudyStatusId.Value;
        btnApprove.Visible = false;
        btnMarkForApproval.Visible = false;
        tbTrancription.Enabled = false;
        rfvTbTranscription.Enabled = false;
        if(studyStatusId == Constants.StudyStatusTypes.Verified)
            return;

        if (loggedInUserRoleId == Constants.Roles.Radiologist)
        {
            //(study.IsManual.Value == null || study.IsManual.Value.ToString().Length == 0 || study.IsManual.Value.Equals("N")) &&
            if ( studyStatusId == Constants.StudyStatusTypes.PendingVerification)
            {
                btnApprove.Visible = true;
                tbTrancription.Enabled = true;
                rfvTbTranscription.Enabled = true;
            }
            else
            {
                tbTrancription.Enabled = false;
                rfvTbTranscription.Enabled = false;
            }
        }
        else if (loggedInUserRoleId == Constants.Roles.Transcriptionist)
        {
            if (studyStatusId != Constants.StudyStatusTypes.New)
            {
                btnMarkForApproval.Visible = true;
                tbTrancription.Enabled = true;
                rfvTbTranscription.Enabled = true;
            }
        }
        else if (loggedInUserRoleId == Constants.Roles.ChiefTechnologist)
        {
            if (study.IsManual.Value.Equals("Y") && studyStatusId == Constants.StudyStatusTypes.PendingVerification)
            {
                btnApprove.Visible = true;
                tbTrancription.Enabled = true;
                rfvTbTranscription.Enabled = true;
            }
        }
    }

    private object SaveFinding(bool removeAudioData)
    {
        FindingObject finding = new FindingObject();
        int studyId = int.Parse(lblStudyId.Value);
        if (lblFindingId.Value != null && lblFindingId.Value.Length > 0)
        {
            finding.GetPrimaryKey().Value = int.Parse(lblFindingId.Value);
            finding.Load(loggedInUserId);
            if (finding.IsLoaded)
            {
                if (removeAudioData)
                {
                    //byte[] temp = new byte[1];
                    finding.AudioData.Value = null;
                }
                else if (tbTrancription.Text.Equals(finding.TextualTranscript.Value)) // no need to update in case the text is same and not to remove audio data
                    return 0;
            }
        }
        
        finding.StudyId.Value = studyId;        
        finding.TextualTranscript.Value = tbTrancription.Text;
        if (loggedInUserRoleId == Constants.Roles.Transcriptionist)
        {
            finding.TranscriptUserId.Value = loggedInUserId;            
        }
        finding.TranscriptionDate.Value = DateTime.Now;
        finding.Save(loggedInUserId);
        
        StudyObject study = new StudyObject();
        study.StudyId.Value = studyId;
        study.Load(loggedInUserId);
        if (study.IsLoaded == true && study.LatestFindingId.Value == null)
        {          
            study.LatestFindingId.Value = finding.FindingId.Value;         
            study.Save(loggedInUserId);
        }
        return finding.FindingId.Value;
    }

    /*protected void btnSave_Click(object sender, EventArgs e)
    {
        object findingId = SaveFinding(false);
        int studyId = int.Parse(lblStudyId.Value);
        StudyObject study = new StudyObject();
        study.StudyId.Value = studyId;
        study.Load(loggedInUserId);
        if (study.IsLoaded == true)
        {
            if (study.LatestFindingId.Value == null)
                study.LatestFindingId.Value = findingId;
            study.StudyStatusId.Value = Constants.StudyStatusTypes.Transcribed;
            study.Save(loggedInUserId);
            StudyStatusTypeObject statusType = new StudyStatusTypeObject();
            statusType.StudyStatusTypeId.Value = Constants.StudyStatusTypes.Transcribed;
            statusType.Load();
            lblStatus.Text = (string)statusType.Status.Value;
            if (loggedInUserRoleId == Constants.Roles.Radiologist)
            {
                btnApprove.Visible = true;
                btnRetranscribe.Visible = true;
            }
            else if (loggedInUserRoleId == Constants.Roles.Transcriptionist)
            {
                btnMarkForApproval.Visible = true;
            }
        }
    }*/

    protected void btnMarkForApproval_Click(object sender, EventArgs e)
    {
        SaveFinding(false);
        UpdateStudyStatus(Constants.StudyStatusTypes.PendingVerification);
        //btnMarkForApproval.Visible = false;
        //tbTrancription.Enabled = false;
        //rfvTbTranscription.Enabled = false;
        lblRadiologist.Visible = true;


        PagesFactory.Transfer(PagesFactory.Pages.CloseWindowPage);
    }

    protected void UpdateStudyStatus(int statusId)
    {
        StudyObject study = new StudyObject();
        study.StudyId.Value = int.Parse(lblStudyId.Value);
        study.Load(Constants.Database.NullUserId);
        if (study.IsLoaded == true)
        {
            study.StudyStatusId.Value = statusId;
            study.Save(loggedInUserId);

            LogObject log = new LogObject();
            log.UserId.Value = loggedInUserId;
            log.StudyId.Value = study.StudyId.Value;
            log.PatientId.Value = study.PatientId.Value;
            log.ActionTime.Value = DateTime.Now;

            if (statusId == Constants.StudyStatusTypes.PendingVerification)
                log.Action.Value = Constants.LogActions.MarkedStudyForVerification;
            else if (statusId == Constants.StudyStatusTypes.Verified)
                log.Action.Value = Constants.LogActions.VerifiedStudy;
            log.Save();
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        SaveFinding(true);
        UpdateStudyStatus(Constants.StudyStatusTypes.Verified);
        ShowButtons();
        PagesFactory.Transfer(PagesFactory.Pages.CloseWindowPage);
    }
    /*protected void btnRetranscribe_Click(object sender, EventArgs e)
    {
        SaveFinding(false);
        UpdateStudyStatus(Constants.StudyStatusTypes.MarkForRetranscription);
    }*/
    public int UserId
    {
        get
        {
            return loggedInUserId;
        }
    }
    public int FindingId
    {
        get
        {
            if (lblFindingId.Value != null && lblFindingId.Value.Length > 0)
            {
                return int.Parse(lblFindingId.Value);
            }
            return 0;
        }
    }
    public int StudyId
    {
        get
        {
            return int.Parse(lblStudyId.Value);
        }
    }

    public bool IsVerified
    {
        get
        {
            if (loggedInUserRoleId == Constants.Roles.ChiefTechnologist)
                return true;
            StudyObject study = new StudyObject();
            study.StudyId.Value = int.Parse(Request[ParameterNames.Request.StudyId]);
            study.Load(loggedInUserId);
            if (study.IsLoaded)
            {
                if ((int)study.StudyStatusId.Value == Constants.StudyStatusTypes.Verified)
                    return true;
                else
                    return false;
            }
            return true;
        }
    }

    public bool CanRecord
    {
        get
        {
            StudyObject study = new StudyObject();
            study.StudyId.Value = int.Parse(Request[ParameterNames.Request.StudyId]);
            study.Load(loggedInUserId);
            if (study.IsLoaded)
            {
                if ((int)study.StudyStatusId.Value == Constants.StudyStatusTypes.Verified)
                    return false;
                else
                {
                    if (loggedInUserRoleId == Constants.Roles.Radiologist)
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    public bool IsTranscriptionist
    {
        get
        {
            return (loggedInUserRoleId == Constants.Roles.Transcriptionist) ? true : false;
        }
    }
}
