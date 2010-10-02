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

public partial class Radiologist_DownloadReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string filePath = ReportGenerator.Instance.Generate(int.Parse(Request[ParameterNames.Request.StudyId]));
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=DatamedReport.pdf");
        Response.Charset = "";
        Response.BinaryWrite(File.ReadAllBytes(filePath));
        Response.End();
    }
}
