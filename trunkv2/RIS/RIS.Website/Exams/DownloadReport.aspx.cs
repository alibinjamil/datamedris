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
using System.IO;

public partial class Radiologist_DownloadReport : StudyPage
{
    protected override bool IsPopUp()
    {
        return false;
    }

    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        Study study = GetStudy();
        if (study != null)
        {
            string filePath = ReportGenerator.Instance.Generate(study);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=DatamedReport.pdf");
            Response.Charset = "";
            Response.BinaryWrite(File.ReadAllBytes(filePath));
            Response.End();
        }
    }
}
