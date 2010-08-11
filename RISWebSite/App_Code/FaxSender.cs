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

using RIS.RISLibrary.Objects.RIS;

/// <summary>
/// Summary description for FaxSender
/// </summary>
public class FaxSender
{
    private FaxSender()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static FaxSender _instance = new FaxSender();
    public static FaxSender Instance { get { return _instance; } }

    public bool SendFax(string recipientName, string recipientCompany, string recipientFax, string documentPath)
    {
        try
        {
            eFaxDeveloper.Outbound.IOutboundClient api = new eFaxDeveloper.Outbound.OutboundClient();
            api.SetXMLResponse(false);
            api.SetTransmissionID("123");
            api.SetFineResolution(true);
            api.SetHighPriority(false);
            api.SetSelfBusy(true);
            api.SetFaxHeader("");
            api.SetDispositionMethod("none");
            api.SetDispositionLevel("NONE");
            api.SetRecipientName(recipientName);
            api.SetRecipientCompany(recipientCompany);
            api.SetRecipientFax(recipientFax);
            api.SetAccountID("8003387151");
            api.SetUserName("zdogar1");
            api.SetPassword("datamed");



            eFaxDeveloper.Outbound.DocumentBundler docBundler = new eFaxDeveloper.Outbound.DocumentBundler();
            docBundler.Add(documentPath);

            api.SetDocuments(docBundler);

            api.PostRequest();
            return true;
        }
        catch { }
        return false;
    }

    public string SendFax(int studyId)
    {
        try
        {
            
            StudyObject study = new StudyObject();
            study.StudyId.Value = studyId;
            study.Load();
            if (study.IsLoaded && study.HospitalId.Value != null)
            {
                HospitalObject hospital = new HospitalObject();
                hospital.HospitalId.Value = study.HospitalId.Value;
                hospital.Load();
                if (hospital.IsLoaded && hospital.Fax.Value != null)
                {
                    string reportPath = ReportGenerator.Instance.Generate(studyId);
                    string name = (hospital.Name.Value != null)? (string)hospital.Name.Value:"";
                    if (SendFax(name, name, (string)hospital.Fax.Value, reportPath))
                    {
                        return (string)hospital.Fax.Value;
                    }
                }
            }            
        }
        catch { }
        return null;
    }

}
