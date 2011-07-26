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
using System.Linq;
using System.Xml.Linq;

using RIS.Common;

public partial class AdminPages_AddTemplate : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindModalitiesDDL();
            Template template = null;
            if (Session[ParameterNames.Session.Template] != null)
            {
                template = (Template)Session[ParameterNames.Session.Template];
                Session[ParameterNames.Session.Template] = null;
            }
            else
            {
                template = GetTemplate();
            }
            if (template != null)
            {
                
                tbName.Text = template.Name;
                ddlModalities.SelectedValue = template.BodyPart.ModalityId.ToString();
                BindBodyPartsDDL();
                if (template.BodyPart != null)
                {
                    ddlbodyParts.SelectedValue = template.BodyPartId.ToString();
                }
                if (template.Heading != null)
                {
                    tbHeading.Text = template.Heading;
                }
                if (template.Description != null)
                {
                    tbDescription.Text = template.Description;
                }
                if (template.Impression != null)
                {
                    tbImpression.Text = template.Impression;
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
    private void BindModalitiesDDL()
    {
        ddlModalities.DataSource = (from m in DatabaseContext.Modalities select m);
        ddlModalities.DataTextField = "Name";
        ddlModalities.DataValueField = "ModalityId";
        ddlModalities.DataBind();
    }
    protected void ddlModalities_DataBound(object sender, EventArgs e)
    {
        ddlModalities.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
        BindBodyPartsDDL();
    }
    private void BindBodyPartsDDL()
    {
        int modalityId = int.Parse(ddlModalities.SelectedValue);
        ddlbodyParts.DataSource = (from bp in DatabaseContext.BodyParts where bp.ModalityId == modalityId select bp);
        ddlbodyParts.DataTextField = "Name";
        ddlbodyParts.DataValueField = "BodyPartId";
        ddlbodyParts.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Template template = new Template();
        SetTemplate(template);
        TemplateUser templateUser = new TemplateUser();
        templateUser.UserId = loggedInUserId;
        template.TemplateUsers.Add(templateUser);
        DatabaseContext.SaveChanges();
        SetInfoMessage("Data saved successfully");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Template template = GetTemplate();
        if (template != null)
        {
            SetTemplate(template);
            DatabaseContext.SaveChanges();
            SetInfoMessage("Data saved successfully");
        }
    }

    private void SetTemplate(Template template)
    {
        if (template != null)
        {
            template.Name = tbName.Text;
            template.BodyPartId = int.Parse(ddlbodyParts.SelectedValue);
            template.Heading = tbHeading.Text;
            template.Description = tbDescription.Text;
            template.Impression = tbImpression.Text;
            template.Text = GetText();
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

    private Template GetTemplate()
    {
        if (Request[ParameterNames.Request.TemplateId] != null)
        {
            int templateId = int.Parse(Request[ParameterNames.Request.TemplateId]);
            return (from t in DatabaseContext.Templates where t.TemplateId == templateId select t).FirstOrDefault();            
        }
        return null;
    }
    protected void ddlModalities_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBodyPartsDDL();
    }
    protected void ddlBodyParts_DataBound(object sender, EventArgs e)
    {
        ddlbodyParts.Items.Insert(0, new ListItem(Labels.DDLTexts.PleaseSelect, "0"));
    }
}
