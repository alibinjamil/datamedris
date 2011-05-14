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

public partial class Radiologist_DownloadReport : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
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
    private Study GetStudy()
    {
        Study study = null;
        RISEntities db = new RISEntities();
        if(Request[ParameterNames.Request.StudyId] != null)
        {
            int studyId = int.Parse(Request[ParameterNames.Request.StudyId]);
            study = (from s in db.Studies
                     where s.StudyId == studyId
                     select s).FirstOrDefault(); 
        }
        return study;
    }
}
