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

using System.Net.Mail;
using System.Net;
using RIS.RISLibrary.Objects.RIS;

/// <summary>
/// Summary description for EmailSender
/// </summary>
public class EmailSender
{

    private static string SMTP_SERVER = "mail.datamedusa.com";
    private static string USER_NAME = "eservices@datamedusa.com";
    private static string PASSWORD = "datamed";

    private EmailSender()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private static EmailSender instance = new EmailSender();
    public static EmailSender Instance
    {
        get { return instance; }
    }

    public void SendSMS(int carrierId, string cellNumber,string hospitalName, string clientURL)
    {
        try
        {
            CarrierObject carrier = new CarrierObject();
            carrier.CarrierId.Value = carrierId;
            carrier.Load();
            if (carrier.IsLoaded)
            {
                SmtpClient smtp = GetMailClient();
                MailMessage message = new MailMessage();
                message.From = new MailAddress("eservices@datamedusa.com");
                message.To.Add(cellNumber + "@" + carrier.EmailServer.Value);
                message.Subject = "Radiologist report available";
                message.Body = "The radiologist has reported on your patient from " + hospitalName + ".  The report can be accessed online at " + clientURL + ".";
                smtp.Send(message);
            }
        }
        catch (Exception ex)
        {
        }
    }

    private SmtpClient GetMailClient()
    {
        SmtpClient smtp = new SmtpClient(SMTP_SERVER);
        NetworkCredential userInfo = new NetworkCredential(USER_NAME, PASSWORD);
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = userInfo;
        return smtp;
    }
}
