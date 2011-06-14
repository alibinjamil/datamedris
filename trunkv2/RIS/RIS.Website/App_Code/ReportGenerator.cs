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
            //doc.SetMargins(70f,70f,70f,70f);
            PdfWriter.GetInstance(doc, new FileStream(completePath,FileMode.Create));
            doc.Open();
            float[] widths = new float[2];
            widths[0] = 50;
            widths[1] = 50;
            
            PdfPTable table = new PdfPTable(widths);
            
            string defaultFontFamily = "Arial";
            Font defaultFont = FontFactory.GetFont(defaultFontFamily, 12);
            Font defaultHeading = FontFactory.GetFont(defaultFontFamily, 12, Font.BOLD);
            Font pageHeading = FontFactory.GetFont(defaultFontFamily, 14, Font.BOLD);
            Font undelineFont = FontFactory.GetFont(defaultFontFamily,12,Font.UNDERLINE);
            Font footerFont = FontFactory.GetFont(defaultFontFamily,10);
            Phrase defaultPhrase = null;

            
            table.WidthPercentage = 100;
            
            table.DefaultCell.Colspan = 2;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.DefaultCell.BorderWidth = 0;            
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(report.HeaderUrl));
            table.AddCell(image);
            table.DefaultCell.Colspan = 1;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(report.ClientAddress);
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell("Telephone: " + study.Client.Phone);
            table.DefaultCell.Colspan = 2;
            table.DefaultCell.BorderWidthBottom = 1;
            table.DefaultCell.BorderColorBottom = Color.BLACK;
            table.AddCell(" ");
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(new Phrase(report.HospitalName,pageHeading));
            table.DefaultCell.BorderWidthBottom = 0;
            table.AddCell(" ");
            table.DefaultCell.Colspan = 1;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //table.DefaultCell.FixedHeight = 30;
            table.DefaultCell.BorderWidthBottom = 0;

            defaultPhrase = new Phrase();
            defaultPhrase.Add(new Chunk("MRN:", defaultHeading));
            defaultPhrase.Add(new Chunk(" " + report.PatientId, defaultFont));
            table.AddCell(defaultPhrase);

            defaultPhrase = new Phrase();
            defaultPhrase.Add(new Chunk("Physician:", defaultHeading));
            defaultPhrase.Add(new Chunk(" " + report.ReferringPhysician, defaultFont));
            table.AddCell(defaultPhrase);

            defaultPhrase = new Phrase();
            defaultPhrase.Add(new Chunk("Patient Name:", defaultHeading));
            defaultPhrase.Add(new Chunk(" " + report.PatientName, defaultFont));
            table.AddCell(defaultPhrase);

            defaultPhrase = new Phrase();
            defaultPhrase.Add(new Chunk("Date of Exam:", defaultHeading));
            defaultPhrase.Add(new Chunk(" " + report.StudyDate, defaultFont));
            table.AddCell(defaultPhrase);

            defaultPhrase = new Phrase();
            defaultPhrase.Add(new Chunk("Date of Birth:", defaultHeading));
            defaultPhrase.Add(new Chunk(" " + report.DateOfBirth, defaultFont));
            table.AddCell(defaultPhrase);

            defaultPhrase = new Phrase();
            defaultPhrase.Add(new Chunk("Accession:", defaultHeading));
            defaultPhrase.Add(new Chunk(" " + study.AccessionNumber, defaultFont));
            table.AddCell(defaultPhrase);

            defaultPhrase = new Phrase();
            defaultPhrase.Add(new Chunk("Sex:", defaultHeading));
            defaultPhrase.Add(new Chunk(" " + study.PatientGender, defaultFont));
            table.AddCell(defaultPhrase);

            defaultPhrase = new Phrase();
            defaultPhrase.Add(new Chunk("Date of Report:", defaultHeading));
            defaultPhrase.Add(new Chunk(" " + report.ReportDate, defaultFont));
            table.AddCell(defaultPhrase);

            defaultPhrase = new Phrase();
            defaultPhrase.Add(new Chunk("Modality:", defaultHeading));
            defaultPhrase.Add(new Chunk(" " + report.Modality, defaultFont));
            table.AddCell(defaultPhrase);

            defaultPhrase = new Phrase();
            defaultPhrase.Add(new Chunk("Date of Last Addendum:", defaultHeading));
            defaultPhrase.Add(new Chunk(" " + report.DateAmmendtment, defaultFont));
            table.AddCell(defaultPhrase);

            table.DefaultCell.Colspan = 2;
            table.DefaultCell.BorderWidthBottom = 1;
            table.AddCell(" ");
            table.DefaultCell.BorderWidthBottom = 0;
            table.AddCell(" ");

            table.AddCell(new Phrase(study.Heading, defaultHeading));
            table.AddCell(" ");
            table.AddCell(new Phrase(study.Description, defaultFont));
            table.AddCell(" ");
            defaultPhrase = new Phrase();
            defaultPhrase.Add(new Chunk("IMPRESSION: ",defaultHeading));
            defaultPhrase.Add(new Chunk(study.Impression,defaultFont));
            table.AddCell(defaultPhrase);

            table.DefaultCell.BorderWidthBottom = 1;
            table.AddCell(" ");
            
            defaultPhrase = new Phrase();
            if (study.StudyStatusId == RIS.RISLibrary.Utilities.Constants.StudyStatusTypes.Verified || report.Ammendments.Count > 0)
            {
                defaultPhrase.Add(new Chunk("Electronically Approved and Signed by:", defaultFont));
            }
            else
            {
                defaultPhrase.Add(new Chunk("Unverified draft report:", defaultFont));
            }

            defaultPhrase.Add(new Chunk("\t" + report.Radiologist + "\t" + report.ReportDateTime ,defaultFont));
            table.AddCell(defaultPhrase);
            table.DefaultCell.BorderWidthBottom = 0;
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            if (report.Ammendments.Count > 0)
            {
                table.AddCell(new Phrase("ADDENDUM:", defaultHeading));
                
                
                foreach(Ammendment ammendment in report.Ammendments)
                {
                    table.AddCell(" ");
                    table.AddCell(ammendment.Text);
                    table.DefaultCell.BorderWidthBottom = 1;
                    table.AddCell(" ");
                    defaultPhrase = new Phrase();
                    if (ammendment.Status == RIS.RISLibrary.Utilities.Constants.StudyStatusTypes.Verified)
                    {
                        defaultPhrase.Add(new Chunk("Electronically Approved and Signed by:", defaultFont));
                    }
                    else
                    {
                        defaultPhrase.Add(new Chunk("Unverified draft report:", defaultFont));
                    }
                    defaultPhrase.Add(new Chunk("\t" + ammendment.Radiologist + "\t" + ammendment.ReportDateTime, defaultFont));
                    table.AddCell(defaultPhrase);
                    table.DefaultCell.BorderWidthBottom = 0;
                }
            }
            
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            table.DefaultCell.BorderWidthTop = 1;
            table.DefaultCell.BorderWidthBottom = 1;
            table.AddCell(new Phrase(report.FooterText,footerFont));
            doc.Add(table);
            doc.Close();
            return completePath;
        }
        return null;
    }
}
