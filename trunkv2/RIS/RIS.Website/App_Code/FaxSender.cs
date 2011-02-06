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

using RIS.Common;

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
        catch (Exception ex){
            //most probably the number is 
        }
        return false;
    }

    public string SendFax(Study study)
    {
        try
        {
          
            
            if (study != null && study.HospitalId.HasValue && study.Hospital.Fax != null)
            {
                string reportPath = ReportGenerator.Instance.Generate(study);
                string name = (study.Hospital.Name != null) ? (string)study.Hospital.Name : "";
                if (SendFax(name, name, (string)study.Hospital.Fax, reportPath))
                {
                    return (string)study.Hospital.Fax;
                }
            }            
        }
        catch { }
        return null;
    }

}
