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

using System.Text;
using System.IO;

using J2.eFaxDeveloper.Outbound;
using J2.eFaxDeveloper;

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

    public bool SendFax(string recipientName, string recipientCompany, string recipientFax, string documentPath,int hospitalId)
    {
        try
        {
            // A) Create an instance of OutboundClient to send an outbound fax
            OutboundClient outboundClient = new OutboundClient();

            // B) you have 2 options to configurate the settings of OutboundClient/////////////////////

            // 1) load (previously saved) settings from the configuration file as follow
            //outboundClient.LoadConfiguration();

            // 2) Set the efaxDeveloper settings manualy
            outboundClient.AccountId = "8003387151";
            outboundClient.UserName = "zdogar1";
            outboundClient.Password = "datamed";

            ////////////////////////////////////////////////////////////////////////////////////////

            // C) Create an instance of OutboundRequest, that will encapsulate all the data of the outbound fax 
            OutboundRequest outboundRequest = new OutboundRequest();

            // optionnaly set fax transmission control settings

            // TransmissionID can have a max length 15
            outboundRequest.TransmissionControl.TransmissionID = "TransID_1234";

            // CustomerID can have a max length 20
            outboundRequest.TransmissionControl.CustomerID = hospitalId.ToString();

            // Set NoDuplicates (default is Disable)
            // outboundRequest.TransmissionControl.NoDuplicates = NoDuplicates.Enable;

            // Set Resolution (there is no default settings)
            outboundRequest.TransmissionControl.Resolution = Resolution.Fine;

            // Set Priority (default is Normal)
            // outboundRequest.TransmissionControl.Priority = Priority.High;

            // Set SelfBusy (default is Enable)
            // outboundRequest.TransmissionControl.SelfBusy = SelfBusy.Disable;

            // Set FaxHeader (Default is "the default fax header")
            outboundRequest.TransmissionControl.FaxHeader = "Datamed | Radiology Information System";

            ////////////////////////////////////////////////////////////////////////////////////////

            // D) set the fax recipient ///////////////////////////////////////////////////////////////
            outboundRequest.Recipient.RecipientName = recipientName;
            outboundRequest.Recipient.RecipientCompany = recipientCompany;
            outboundRequest.Recipient.RecipientFax = recipientFax;

            ////////////////////////////////////////////////////////////////////////////////////////

            // E) add file(s) to fax //////////////////////////////////////////////////////////////////
            /*FaxFile faxFile1 = new FaxFile();
            faxFile1.FileType = FaxFileType.txt;
            faxFile1.FileContents = Encoding.ASCII.GetBytes("Fax document");
            outboundRequest.Files.Add(faxFile1);*/

            // example to load add a pdf file
            FaxFile faxFile2 = new FaxFile();
            faxFile2.FileType = FaxFileType.pdf;
            faxFile2.FileContents = File.ReadAllBytes(documentPath);
            outboundRequest.Files.Add(faxFile2);

            /////////////////////////////////////////////////////////////////////////////////////////

            // F) set the disposition settings//////////////////////////////////////////////////////////

            // set the disposition level 
            outboundRequest.DispositionControl.DispositionLevel = DispositionLevel.Both;

            // set the disposition method
            outboundRequest.DispositionControl.DispositionMethod = DispositionMethod.Email;

            // for DispositionMethod.Email set the email address(es) to send the disposition to
            outboundRequest.DispositionControl.DispositionEmails.Add(
                new DispositionEmail { DispositionAddress = "ali@digiply.com", DispositionRecipient = "Ali Bin Jamil" });

            // if you choose to receive the disposition via http POST
            // set the disposition method
            /*outboundRequest.DispositionControl.DispositionMethod = DispositionMethod.Post;

            // for DispositionMethod.Post set the DispositionURL where efaxDeveloper send the XML disposition for this fax transmission
            outboundRequest.DispositionControl.DispositionURL = "https://www.mydoman.com/efaxDisposition";
            */

            ///////////////////////////////////////////////////////////////////////////////////////////

            // G) send the oubound fax to efaxDeveloper and get an instance of OutboundResponse back 
            OutboundResponse outboundResponse = outboundClient.SendOutboundRequest(outboundRequest);

            // check if the fax was accepted successfuly
            if (outboundResponse.StatusCode == StatusCode.Success)
            {
                //Console.WriteLine(string.Format("Success: DOCID {0} TransmissionID {1}", outboundResponse.DOCID, outboundResponse.TransmissionID));
                return true;
            }
            else
            {
                //Console.WriteLine(string.Format("Error: {0} {1}", outboundResponse.ErrorMessage, outboundResponse.ErrorLevel));
                return false;
            }

            /////////////////////////////////////////////////////////////////////////////////////////////
        }
        catch (Exception ex)
        {
            Console.WriteLine(String.Format("Error: {0}", ex.Message));
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
                if (SendFax(name, name, (string)study.Hospital.Fax, reportPath,study.HospitalId.Value))
                {
                    return (string)study.Hospital.Fax;
                }
            }            
        }
        catch { }
        return null;
    }

}
