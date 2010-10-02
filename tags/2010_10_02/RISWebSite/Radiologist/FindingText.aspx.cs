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
using System.Xml;

public partial class Radiologist_FindingText : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["templateId"] != null)
        {
            LoadFindingFromTemplate();
        }
        else if (Request["findingId"] != null)
        {
            LoadFindingForStudy();
        }
    }

    private void LoadFindingForStudy()
    {
        FindingObject finding = new FindingObject();
        finding.FindingId.Value = Request["findingId"];
        finding.Load();
        if (finding.IsLoaded)
        {
            LoadFinding(finding.TextualTranscript.Value.ToString());
        }
        else
        {
            LoadFinding("");
        }
    }

    private void LoadFindingFromTemplate()
    {
        TemplateObject template = new TemplateObject();
        template.TemplateId.Value = Request["templateId"];
        template.Load();
        if (template.IsLoaded)
        {
            LoadFinding(template.Text.Value.ToString());
        }
    }

    private void LoadFinding(string text)
    {
        System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);

        string heading = "";
        string description = "";
        string impression = "";
        try
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(text);
            heading = xDoc.ChildNodes[0].ChildNodes[0].InnerText;
            description = xDoc.ChildNodes[0].ChildNodes[1].InnerText;
            impression = xDoc.ChildNodes[0].ChildNodes[2].InnerText;
        }
        catch (Exception ex)
        {
            description = text;
        }
        TextBox headingTB = new TextBox();
        headingTB.Text = heading;
        headingTB.Width = Unit.Pixel(580);
        headingTB.ID = "headingTextBox";

        htmlTextWriter.Write("<div><span style='display:inline-block;width:100px;'><b>Heading:</b></span>");
        headingTB.RenderControl(htmlTextWriter);
        htmlTextWriter.Write("</div>");

        TextBox descTB = new TextBox();
        descTB.TextMode = TextBoxMode.MultiLine;
        descTB.Rows = 10;
        descTB.Width = Unit.Pixel(680);
        descTB.Text = description;
        descTB.ID = "descriptionTextBox";

        htmlTextWriter.Write("<div><span style='display:inline-block;width:75px;'><b>Description:</b></span></div><div>");
        descTB.RenderControl(htmlTextWriter);
        htmlTextWriter.Write("</div>");

        TextBox impTB = new TextBox();
        impTB.TextMode = TextBoxMode.MultiLine;
        impTB.Rows = 3;
        impTB.Text = impression;
        impTB.Width = Unit.Pixel(680);
        impTB.ID = "impressionTextBox";

        htmlTextWriter.Write("<div><span style='display:inline-block;width:100px;'><b>Impression:</b></span></div><div>");
        impTB.RenderControl(htmlTextWriter);
        htmlTextWriter.Write("</div>");

        Response.Write(htmlTextWriter.InnerWriter);
        Response.End();

    }
}
