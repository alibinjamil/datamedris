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
using System.IO;

using RIS.Common;

public partial class WebScan_SaveAttachment : GenericPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            /*TextWriter tw = new StreamWriter(@"D:\test.txt");
            foreach (string key in Request.Params.Keys)
            {
                tw.WriteLine("Key:" + key + " Value:" + Request[key]);
            }
            tw.Close();*/

            int iFileLength;
            HttpFileCollection files = HttpContext.Current.Request.Files;
            HttpPostedFile uploadfile = files["RemoteFile"];
            String strImageName = uploadfile.FileName;

            iFileLength = uploadfile.ContentLength;
            Byte[] inputBuffer = new Byte[iFileLength];
            System.IO.Stream inputStream;
            inputStream = uploadfile.InputStream;
            inputStream.Read(inputBuffer, 0, iFileLength);

            Attachment attachment = new Attachment();
            attachment.AttachmentData = inputBuffer;
            attachment.AttachmentType = "PDF";
            attachment.Description = Server.UrlDecode(Request.Form[ParameterNames.Request.Description]);
            attachment.Name = Server.UrlDecode(Request.Form[ParameterNames.Request.Name]);
            attachment.ScannedBy = int.Parse(Request.Form[ParameterNames.Request.UserId]);
            attachment.ScannedTime = DateTime.Now;
            attachment.StudyId = int.Parse(Request.Form[ParameterNames.Request.StudyId]);
            if (Request.Form["isReport"] != null && Request.Form["isReport"] == "true")
            {
                Study study = (from s in DatabaseContext.Studies where s.StudyId == attachment.StudyId select s).FirstOrDefault();
                if (study != null)
                {
                    attachment.AttachmentType = "REPORT";
                    study.Attachment = attachment;
                }
            }
            Log log = new Log();
            log.Action = RIS.RISLibrary.Utilities.Constants.LogActions.AddedAttachment;
            log.ActionTime = DateTime.Now;
            log.StudyId = attachment.StudyId;
            log.UserId = attachment.ScannedBy;
            DatabaseContext.AddToLogs(log);
            DatabaseContext.AddToAttachments(attachment);
            DatabaseContext.SaveChanges();

        }
        catch (Exception)
        {
        }		

    }
}
