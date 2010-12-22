using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;
using RIS.RISLibrary.Utilities;
using System.Data.SqlClient;

public partial class AdminPages_AddTemplate : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TemplateObject template = LoadTemplate();
            if (template != null)
            {
                if (template.BodyPart.Value != null)
                {
                    tbBodyPart.Text = (string)template.BodyPart.Value;
                }
                tbName.Text = (string)template.Name.Value;
                ddlModalities.SelectedValue = template.ModalityId.Value.ToString();
                if (template.Heading.Value != null)
                {
                    tbHeading.Text = (string)template.Heading.Value;
                }
                if (template.Description.Value != null)
                {
                    tbDescription.Text = (string)template.Description.Value;
                }
                if (template.Impression.Value != null)
                {
                    tbImpression.Text = (string)template.Impression.Value;
                }
                btnAdd.Visible = false;
                btnUpdate.Visible = true;
            }
        }
    }
    protected override bool IsPopUp()
    {
        return false;
    }

    protected void ddlModalities_DataBound(object sender, EventArgs e)
    {
        ddlModalities.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        TemplateObject template = new TemplateObject();
        SetTemplate(template);
        template.Save(loggedInUserId);
        if (template.IsLoaded)
        {
            TemplateUserObject templateUser = new TemplateUserObject();
            templateUser.TemplateId.Value = template.TemplateId.Value;
            templateUser.UserId.Value = loggedInUserId;
            templateUser.Save(loggedInUserId);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        TemplateObject template = LoadTemplate();
        if (template != null)
        {
            SetTemplate(template);
            template.Save(loggedInUserId);                
        }
    }

    private void SetTemplate(TemplateObject template)
    {
        if (template != null)
        {
            template.Name.Value = tbName.Text;
            template.ModalityId.Value = ddlModalities.SelectedValue;
            template.BodyPart.Value = tbBodyPart.Text;
            template.Heading.Value = tbHeading.Text;
            template.Description.Value = tbDescription.Text;
            template.Impression.Value = tbImpression.Text;
            template.Text.Value = GetText();
        }
    }

    private string GetText()
    {
        StringBuilder text = new StringBuilder();
        text.Append("<data>");
        text.Append("<heading><![CDATA[").Append(tbHeading.Text).Append("]]></heading>");
        text.Append("<description><![CDATA[").Append(tbDescription.Text).Append("]]></description>");
        text.Append("<impression><![CDATA[").Append(tbImpression.Text).Append("]]></impression>");
        text.Append("</data>");
        return text.ToString();
    }

    private TemplateObject LoadTemplate()
    {
        if (Request[ParameterNames.Request.TemplateId] != null)
        {
            TemplateObject template = new TemplateObject();
            template.TemplateId.Value = int.Parse(Request[ParameterNames.Request.TemplateId]);
            template.Load();
            if (template.IsLoaded)
            {
                return template;
            }
        }
        return null;
    }
}
