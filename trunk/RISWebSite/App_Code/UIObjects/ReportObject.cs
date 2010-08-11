using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Text;

using RIS.RISLibrary.Objects.RIS;
/// <summary>
/// Summary description for ReportObject
/// </summary>
public class ReportObject : GenericUIObject
{
    private int studyId;
    private bool isHTML;
    public ReportObject(int studyId,bool isHTML)
    {
        this.studyId = studyId;
        this.isHTML = isHTML;
    }
    private string _clientName;
    private string _clientAddress;
    private string _modality;
    private string _manualStatus;
    private string _patientName;
    private string _dateOfBirth;
    private string _referringPhysician;
    private string _studyDate;
    private string _transcription;
    private string _reportDateTime;
    private string _reportDate;
    private string _radiologist;
    private string _status;
    private string _hospitalName;
    private int _patientId;
    private string _fax;
    public string ClientName { get { return _clientName; } }
    public string ClientAddress { get { return _clientAddress; } }
    public string Modality { get { return _modality; } }
    public string ManualStatus { get { return _manualStatus; } }
    public string PatientName { get { return _patientName; } }
    public string DateOfBirth { get { return _dateOfBirth; } }
    public string ReferringPhysician { get { return _referringPhysician; } }
    public string StudyDate { get { return _studyDate; } }
    public string Transcription { get { return _transcription; } }
    public string ReportDateTime { get { return _reportDateTime; } }
    public string ReportDate { get { return _reportDate; } }
    public string Radiologist { get { return _radiologist; } }
    public string Status { get { return _status; } }
    public string HospitalName { get { return _hospitalName; } }
    public string Fax { get { return _fax; } }
    public int PatientId { get { return _patientId; } }
    public bool Load()
    {
        StudyObject study = new StudyObject();
        study.StudyId.Value = studyId;
        study.Load();
        if (study.IsLoaded)
        {
            if (study.ClientId.Value != null)
            {
                ClientObject client = new ClientObject();
                client.ClientId.Value = study.ClientId.Value;
                client.Load();
                if (client.IsLoaded)
                {
                    _clientName = client.Name.Value.ToString();
                    if (client.Address.Value != null)
                        _clientAddress = client.Address.Value.ToString() + " ";
                    if (client.City.Value != null)
                        _clientAddress += client.City.Value.ToString() + " ";
                    if (client.State.Value != null)
                        _clientAddress += client.State.Value.ToString();
                }
            }
            ModalityObject modality = new ModalityObject();
            modality.ModalityId.Value = study.ModalityId.Value;
            modality.Load();
            if (modality.IsLoaded)
            {
                _modality = modality.Name.Value.ToString();
            }
            if (study.IsManual.Value != null && study.IsManual.Value.ToString().Equals("Y"))
            {
                _manualStatus = "This report was imported from another system.";
            }
            PatientObject patient = new PatientObject();
            patient.GetPrimaryKey().Value = study.PatientId.Value;
            patient.Load();
            if (patient.IsLoaded)
            {
                _patientId = (int)patient.PatientId.Value;
                _patientName = (string)patient.Name.Value;
                if (patient.DateOfBirth.Value != null)
                {
                    _dateOfBirth = ((DateTime)patient.DateOfBirth.Value).ToShortDateString();
                }
            }
            if (study.ReferringPhysicianId != null && study.ReferringPhysicianId.Value != null)
            {
                UserObject user = new UserObject();
                user.UserId.Value = study.ReferringPhysicianId.Value;
                user.Load();
                _referringPhysician = user.Name.Value.ToString();

            }
            else
            {
                _referringPhysician = "(N/A)";
            }
            _studyDate = ((DateTime)study.StudyDate.Value).ToShortDateString();


            FindingObject finding = new FindingObject();
            finding.FindingId.Value = (int)study.LatestFindingId.Value;
            finding.Load();
            if (finding.IsLoaded)
            {
                _transcription = GetTrascription((string)finding.TextualTranscript.Value);
                if (finding.AudioDate.Value != null)
                {
                    _reportDateTime = ((DateTime)finding.AudioDate.Value).ToString();
                    _reportDate = ((DateTime)finding.AudioDate.Value).ToShortDateString();
                }

                if (finding.AudioUserId.Value != null)
                {
                    UserObject radiologist = new UserObject();
                    radiologist.UserId.Value = finding.AudioUserId.Value;
                    radiologist.Load();
                    if (radiologist.IsLoaded)
                    {
                        string[] names = radiologist.Name.Value.ToString().Split(',');
                        if (names != null && names.Length == 2)
                        {
                            string space = " ";
                            if (isHTML) space = "&nbsp;" ;
                            _radiologist = names[1] + space + names[0] + "," + space + "M.D";
                        }
                        else
                        {
                            _radiologist = radiologist.Name.Value.ToString();
                        }
                    }
                }
            }
            StudyStatusTypeObject studyStatus = new StudyStatusTypeObject();
            studyStatus.StudyStatusTypeId.Value = study.StudyStatusId.Value;
            studyStatus.Load();
            if (studyStatus.IsLoaded)
            {
                _status = (string)studyStatus.Status.Value;
            }

            HospitalObject hospital = new HospitalObject();
            hospital.HospitalId.Value = study.HospitalId.Value;
            hospital.Load();
            if (hospital.IsLoaded)
            {
                _hospitalName = hospital.Name.Value.ToString();
                if (hospital.Fax.Value != null)
                {
                    _fax = (string)hospital.Fax.Value;
                }
            }
            return true;
        }
        return false;
    }
    private string GetTrascription(string transcription)
    {   
        try
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(transcription);
            String html = null;
            if (isHTML)
            {
                html = "<b>" + xDoc.ChildNodes[0].ChildNodes[0].InnerText + "</b><br/><br/>";
                html += ReplaceNewLine(xDoc.ChildNodes[0].ChildNodes[1].InnerText) + "<br/><br/>";
                html += "<b>IMPRESSION:</b>" + xDoc.ChildNodes[0].ChildNodes[2].InnerText;
            }
            else
            {
                html = xDoc.ChildNodes[0].ChildNodes[0].InnerText + "\n\n";
                html += xDoc.ChildNodes[0].ChildNodes[1].InnerText + "\n\n";
                html += "IMPRESSION: " + xDoc.ChildNodes[0].ChildNodes[2].InnerText;
            }
            return html;
        }
        catch (Exception ex)
        {
            return ReplaceNewLine(transcription);
        }
    }
    private string ReplaceNewLine(string transcription)
    {
        StringBuilder newString = new StringBuilder();
        bool skip = false;
        if (transcription != null)
        {
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
        }
        return newString.ToString();

    }

}
