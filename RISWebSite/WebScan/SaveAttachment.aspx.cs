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

using RIS.RISLibrary.Objects.RIS;

public partial class WebScan_SaveAttachment : System.Web.UI.Page
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

            AttachmentObject attachment = new AttachmentObject();
            attachment.AttachmentData.Value = inputBuffer;
            attachment.AttachmentType.Value = "PDF";
            attachment.Description.Value = Request.Form[ParameterNames.Request.Description];
            attachment.Name.Value = Request.Form[ParameterNames.Request.Name];
            attachment.ScannedBy.Value = Request.Form[ParameterNames.Request.UserId];
            attachment.ScannedTime.Value = DateTime.Now;
            attachment.StudyId.Value = Request.Form[ParameterNames.Request.StudyId];
            attachment.Save();

        }
        catch (Exception ex)
        {
        }		

    }
}
