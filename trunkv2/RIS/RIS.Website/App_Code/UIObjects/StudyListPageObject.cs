using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for FindingPage
/// </summary>
public class StudyListPageObject : GenericUIObject
{
    public StudyListPageObject()
    {
        //
        // TODO: Add constructor logic here
        //
        //m_findingId = 0;
        m_physician = null;
    }
    private Nullable<byte> _reportType;
    public Nullable<byte> ReportType
    {
        get { return _reportType; }
        set { _reportType = value; }
    }

    private Nullable<int> _attachmentId;
    public Nullable<int> AttachmentId
    {
        get { return _attachmentId; }
        set { _attachmentId = value; }
    }
    /*private int m_findingId;
    public int FindingId
    {
        get
        {
            return m_findingId;
        }
        set
        {
            m_findingId = value;
        }
    }*/

    private int m_studyId;
    public int StudyId
    {
        get
        {
            return m_studyId;
        }
        set
        {
            m_studyId = value;
        }
    }


    private string m_patientName;
    public string PatientName
    {
        get
        {
            return m_patientName;
        }
        set
        {
            m_patientName = value;
        }
    }

    private string m_patientId;
    public string PatientId
    {
        get
        {
            return m_patientId;
        }
        set
        {
            m_patientId = value;
        }
    }

    private string originalPatientId;
    public string OriginalPatientId
    {
        get
        {
            return originalPatientId;
        }
        set
        {
            originalPatientId = value;
        }
    }

    private string m_status;
    public string Status
    {
        get
        {
            return m_status;
        }
        set
        {
            m_status = value;
        }
    }

    private int m_statusId;
    public int StatusId
    {
        get
        {
            return m_statusId;
        }
        set
        {
            m_statusId = value;
        }
    }

 
    private string m_studyDate;
    public string StudyDate
    {
        get
        {
            return m_studyDate;
        }
        set
        {
            m_studyDate = value;
        }
    }

    private DateTime m_studyTimeStamp;
    public DateTime StudyTimeStamp
    {
        get
        {
            return m_studyTimeStamp;
        }
        set
        {
            m_studyTimeStamp = value;
        }
    }
    
    
    private string m_modality;
    public string Modality
    {
        get
        {
            return m_modality;
        }
        set
        {
            m_modality = value;
        }
    }
    
    private string m_Procedure;
    public string Procedure
    {
        get
        {
            return m_Procedure;
        }
        set
        {
            m_Procedure = value;
        }
    }
    
    private string m_radiologist;
    public string Radiologist
    {
        get
        {
            return m_radiologist;
        }
        set
        {
            m_radiologist = value;
        }
    }
    
    private string m_physician;
    public string Physician
    {
        get
        {
            return m_physician;
        }
        set
        {
            m_physician = value;
        }
    }

    private string m_findingText;
    public string FindingText
    {
        get
        {
            return m_findingText;
        }
        set
        {
            m_findingText = value;
        }
    }

    private int m_patientRecordCount;
    public int PatientRecordCount
    {
        get
        {
            return m_patientRecordCount;
        }
        set
        {
            m_patientRecordCount = value;
        }
    }

    private int m_radiologistId;
    public int RadiologistId
    {
        get
        {
            return m_radiologistId;
        }
        set
        {
            m_radiologistId = value;
        }
    }

    private bool m_isManual;
    public bool IsManual
    {
        get
        {
            return m_isManual;
        }
        set
        {
            m_isManual = value;
        }
    }

    private string m_accessionNumber;
    public string AccessionNumber
    {
        get { return m_accessionNumber; }
        set { m_accessionNumber = value; }
    }

    private string m_techComments;
    public string TechComments
    {
        get { return m_techComments; }
        set { m_techComments = value; }
    }
}