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
using System.Net.Mail;
using System.Net;

public partial class TestPages_TestEmail : System.Web.UI.Page
{
    private static string SMTP_SERVER = "mail.datamedusa.com";
    private static string USER_NAME = "eservices@datamedusa.com";
    private static string PASSWORD = "datamed";

    protected void Page_Load(object sender, EventArgs e)
    {
        SmtpClient smtp = new SmtpClient(SMTP_SERVER);
        NetworkCredential userInfo = new NetworkCredential(USER_NAME, PASSWORD);
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = userInfo;
        MailMessage message = new MailMessage();
        message.From = new MailAddress("eservices@datamedusa.com");
        message.To.Add("alibinjamil@gmail.com");
        message.Subject = "Radiologist report available";
        message.Body = "The radiologist has reported on your patient from DataMed.  The report can be accessed online at DataMed.";
        smtp.Send(message);
    }
}
