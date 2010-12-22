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

using RIS.RISLibrary.Objects.RIS;
public partial class WebScan_DownloadAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {    
        AttachmentObject attachment = new AttachmentObject();
        attachment.AttachmentId.Value = Request["attachmentId"];
        attachment.Load();
        if(attachment.IsLoaded)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + attachment.Name.Value + ".pdf");
            Response.Charset = "";
            Response.BinaryWrite((byte[])attachment.AttachmentData.Value);
            Response.End();
        }        
    }
}
