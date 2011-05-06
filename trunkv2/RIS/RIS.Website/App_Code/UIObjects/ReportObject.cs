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
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Xml.Linq;

using RIS.Common;
/// <summary>
/// Summary description for ReportObject
/// </summary>
public class ReportObject : GenericUIObject
{
    private Study study;
    private bool isHTML;

    public ReportObject(Study study,bool isHTML)
    {
        this.study = study;
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
    private string _patientId;
    private string _fax;
    private string _headerUrl;
    private string _ammendment;
    //private string _footerText;

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
    public string PatientId { get { return _patientId; } }
    public string HeaderUrl { get { return _headerUrl; } }
    public string Ammendment { get { return _ammendment; } }
    public string FooterText
    {
        get
        {
            return "This Facsimile may contain PRIVILEGED, CONFIDENTIAL AND/OR OTHERWISE PROTECTED INFORMATION intended only for the use of addressee. Unauthorized distribution of the Facsimile or its contents is strictly prohibited according to the Health Insurance Portability and Accountability Act(HIPAA) of 1996. If you are not the addressee, the person responsible for delivering the message to the addressee, or have received this facsimile in error, please immediately notify us by telephone at the number above and destroy the information. Thank you.";
        }
    }
    public bool Load()
    {
        if (study != null)
        {
            if (study.ClientId.HasValue)
            {
                _clientName = study.Client.Name;
                if (study.Client.Address != null)
                    _clientAddress = study.Client.Address + ", ";
                if (study.Client.City != null)
                    _clientAddress += study.Client.City + ", ";
                if (study.Client.State != null)
                    _clientAddress += study.Client.State + " ";
                if (study.Client.Zip != null)
                    _clientAddress += study.Client.Zip;

                if (File.Exists(HttpContext.Current.Server.MapPath("~/Images/" + study.Client.ClientGUID + "_Header.jpg")))
                {
                    _headerUrl = "~/Images/" + study.Client.ClientGUID + "_Header.jpg";
                }
                else
                {
                    _headerUrl = "~/Images/Datamed_Header.jpg";
                }
            }            
            if (study.ModalityId.HasValue)
            {
                _modality = study.Modality.Name;
            }
            if (study.IsManual != null && study.IsManual.ToUpper().Equals("Y"))
            {
                _manualStatus = "This report was imported from another system.";
            }            
            if (study.ExternalPatientId != null)
            {
                _patientId = study.ExternalPatientId;
                _patientName = study.PatientName;
                if (study.PatientDOB != null)
                {
                    _dateOfBirth = study.PatientDOB.Value.ToShortDateString();
                }
            }
            if (study.ReferringPhysicianId.HasValue)
            {
                _referringPhysician = study.ReferringPhysician.Name;
            }
            else
            {
                _referringPhysician = "(N/A)";
            }
            _studyDate = study.StudyDate.Value.ToShortDateString();

            
            _transcription = GetTrascription(study);
            _ammendment = GetAmmendment(study);
            if (study.ReportDate.HasValue)
            {
                _reportDateTime = study.ReportDate.Value.ToString();
                _reportDate = study.ReportDate.Value.ToShortDateString();
            }

            if (study.Radiologist != null)
            {                   
                string[] names = study.Radiologist.Name.Split(',');
                if (names != null && names.Length == 2)
                {
                    string space = " ";
                    if (isHTML) space = "&nbsp;" ;
                    _radiologist = names[1] + space + names[0] + "," + space + "M.D.";
                }
                else
                {
                    _radiologist = study.Radiologist.Name;
                }                    
            }
            

            if (study.StudyStatusType != null)
            {
                _status = study.StudyStatusType.Status;
            }

            if (study.Hospital != null)
            {
                _hospitalName = study.Hospital.Name;
                if (study.Hospital.Fax != null)
                {
                    _fax = (string)study.Hospital.Fax;
                }
            }
            return true;
        }
        return false;
    }
    private string GetTrascription(Study study)
    {   
        String html = null;
        
        
        if (isHTML)
        {
            html = "<b>" + study.Heading + "</b><br/><br/>";
            html += ReplaceNewLine(study.Description) + "<br/><br/>";
            html += "<b>IMPRESSION:</b>&nbsp;" + study.Impression;
            
        }
        else
        {
            html = study.Heading + "\n\n";
            html += study.Description + "\n\n";
            html += "IMPRESSION: " + study.Impression;
            
            
        }
        return html;
    }
    private string GetAmmendment(Study study)
    {
        string ammendment = null;
        List<Study> parents = study.GetParents();
        List<Study> amendments = (from s in parents where s.Amendment != null && s.Amendment.Length > 0  orderby s.ReportDate select s).ToList();
        bool addedHeading = true;
        if (isHTML)
        {            
            foreach (Study amendment in amendments)
            {
                if (addedHeading)
                {
                    ammendment += "<b>ADDENDUM:</b>&nbsp;";
                    addedHeading = false;
                }
                ammendment += "<br/><br/>" + amendment.Amendment + "<br/>--" + amendment.Radiologist.Name + " " + amendment.ReportDate.ToString();
            }
            if (study.Amendment != null && study.Amendment.Length > 0)
            {
                if (addedHeading)
                {
                    ammendment += "<b>ADDENDUM:</b>&nbsp;";
                    addedHeading = false;
                }
                ammendment += "<br/><br/>" + study.Amendment + "<br/>--" + study.Radiologist.Name + " " + study.ReportDate.ToString();
            }
        }
        else
        {            
            foreach (Study amendment in amendments)
            {
                if (addedHeading)
                {
                    ammendment += "ADDENDUM: ";
                    addedHeading = false;
                }
                ammendment += "\n\n" + amendment.Amendment + "\n--" + amendment.Radiologist.Name + " " + amendment.ReportDate.ToString();
            }
            if (study.Amendment != null && study.Amendment.Length > 0)
            {
                if (addedHeading)
                {
                    ammendment += "ADDENDUM: ";
                    addedHeading = false;
                }
                ammendment += "\n\n" + study.Amendment + "\n--" + study.Radiologist.Name + " " + study.ReportDate.ToString();
            }
        }
        return ammendment;
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
