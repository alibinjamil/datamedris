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

using RIS.Common;

public partial class WebScan_DownloadAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RISEntities db = new RISEntities();
        int attachmentId = int.Parse(Request["attachmentId"]);
        Attachment attachment = (from a in db.Attachments where a.AttachmentId == attachmentId select a).FirstOrDefault();
        if(attachment != null)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + attachment.Name + ".pdf");
            Response.Charset = "";
            Response.BinaryWrite((byte[])attachment.AttachmentData);
            Response.End();
        }        
    }
}
