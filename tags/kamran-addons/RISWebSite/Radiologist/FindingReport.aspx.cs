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
using System.Text;

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;

public partial class Radiologist_FindingReport : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        int studyId = int.Parse(Request[ParameterNames.Request.StudyId]);
        StudyObject study = new StudyObject();
        study.GetPrimaryKey().Value = studyId;
        study.Load();
        if (study.IsLoaded)
        {
            if (study.IsManual.Value != null && study.IsManual.Value.ToString().Equals("Y"))
            {
                lblManualStatus.Text = "This report was imported from another system.";
            }
            PatientObject patient = new PatientObject();
            patient.GetPrimaryKey().Value = study.PatientId.Value;
            patient.Load();
            if (patient.IsLoaded)
            {
                lblPatientId.Text = (string)patient.ExternalPatientId.Value ;
                lblPatientName.Text = (string)patient.Name.Value;
                lblSex.Text = (string)patient.Gender.Value;
                if(patient.DateOfBirth.Value != null)
                {
                    DateTime dob = (DateTime)patient.DateOfBirth.Value;
                    lblAge.Text = (DateTime.Now.Year - dob.Year).ToString();
                }
            }
            if (study.ReferringPhysicianId.Value != null)
            {
                UserObject requestingPhysician = new UserObject();
                requestingPhysician.UserId.Value = study.ReferringPhysicianId.Value;
                requestingPhysician.Load();
                if (requestingPhysician.IsLoaded)
                {
                    lblDoctor.Text = (string)requestingPhysician.Name.Value;
                }
                else
                {
                    lblDoctor.Text = "(N/A)";
                }
            }
            else
            {
                lblDoctor.Text = "(N/A)";
            }

            lblStudyDate.Text = ((DateTime)study.StudyDate.Value).ToString();
            FindingObject finding = new FindingObject();
            finding.FindingId.Value = (int)study.LatestFindingId.Value;
            finding.Load();
            if(finding.IsLoaded)
            {
                if(finding.TranscriptionDate.Value != null)
                    lblTranscriptionDate.Text = ((DateTime)finding.TranscriptionDate.Value).ToString();
                lblTranscription.Text = GetTrascription((string)finding.TextualTranscript.Value);
                UserObject transcriptionist = new UserObject();
                transcriptionist.UserId.Value = finding.TranscriptUserId.Value;
                transcriptionist.Load();
                if (transcriptionist.IsLoaded)
                {
                    lblTranscriptionist.Text = (string)transcriptionist.Name.Value;
                }
                if (finding.AudioUserId.Value != null)
                {
                    UserObject radiologist = new UserObject();
                    radiologist.UserId.Value = finding.AudioUserId.Value;
                    radiologist.Load();
                    if (radiologist.IsLoaded)
                        lblRadiologist.Text = (string)radiologist.Name.Value;
                }
                if (finding.AudioDate.Value != null)
                    lblDictationDate.Text = ((DateTime)finding.AudioDate.Value).ToString();
            }
            StudyStatusTypeObject studyStatus = new StudyStatusTypeObject();
            studyStatus.StudyStatusTypeId.Value = study.StudyStatusId.Value;
            studyStatus.Load();
            if (studyStatus.IsLoaded)
            {
                lblStatus.Text = (string)studyStatus.Status.Value;
            }

            LogObject log = new LogObject();
            log.UserId.Value = loggedInUserId;
            log.StudyId.Value = study.StudyId.Value;
            log.PatientId.Value = study.PatientId.Value;
            log.Action.Value = Constants.LogActions.ViewedStudy;
            log.ActionTime.Value = DateTime.Now;
            log.Save();
        }
    }
    protected override bool IsPopUp()
    {
        return true;
    }

    private string GetTrascription(string transcription)
    {
        StringBuilder newString = new StringBuilder();
        bool skip = false;
        for (int i = 0; i < transcription.Length; i++)
        {
            int currentChar = (int)transcription[i];
            switch (currentChar)
            {
                case 10:
                    if (skip == false)
                    {
                        newString.Append("<BR/>");
                        skip = true;
                    }
                    else
                        skip = false;
                    break;
                case 13:
                    if (skip == false)
                    {
                        newString.Append("<BR/>");
                        skip = true;
                    }
                    else
                        skip = false;
                    break;
                default:
                    skip = false;
                    newString.Append((char)currentChar);
                    break;
            }
        }
        return newString.ToString();
    }
}
