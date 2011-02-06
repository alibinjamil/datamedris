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
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;

using RIS.Common;
/// <summary>
/// Summary description for ReportGenerator
/// </summary>
public class ReportGenerator
{
    private ReportGenerator()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static ReportGenerator _instance = new ReportGenerator();
    public static ReportGenerator Instance
    {
        get { return _instance; }
    }
    

    public string Generate(Study study)
    {
        ReportObject report = new ReportObject(study,false);
        if(report.Load())
        {
            string filePath = ConfigurationManager.AppSettings["ReportPath"];
            string fileName = Guid.NewGuid().ToString();
            string completePath = filePath + "\\" + fileName + ".pdf";
            Document doc = new Document();
            doc.SetMargins(103f,103f,103f,103f);
            PdfWriter.GetInstance(doc, new FileStream(completePath,FileMode.Create));
            doc.Open();
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.DefaultCell.Colspan = 2;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(report.ClientName);
            table.AddCell(report.ClientAddress);
            table.AddCell(report.HospitalName);
            table.DefaultCell.Colspan = 1;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell("PATIENT NAME:");
            table.AddCell(report.PatientName);
            table.AddCell("DATE OF BIRTH:");
            table.AddCell(report.DateOfBirth);
            table.AddCell("DATE OF EXAM:");
            table.AddCell(report.StudyDate);
            table.AddCell("TYPE OF EXAM:");
            table.AddCell(report.Modality);
            table.AddCell("REFERRING PHYSICAN:");
            table.AddCell(report.ReferringPhysician);
            table.AddCell("REPORT DATE:");
            table.AddCell(report.ReportDate);
            table.DefaultCell.Colspan = 2;
            table.AddCell(report.Transcription);
            table.AddCell(report.Radiologist);
            table.AddCell(report.ReportDateTime);
            table.AddCell("Report Status: " + report.Status);
            table.AddCell(report.ManualStatus);
            doc.Add(table);
            doc.Close();
            return completePath;
        }
        return null;
    }
}
