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
using RIS.RISLibrary.Database;
using System.Data.SqlClient;

public partial class Radiologist_TemplateList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillTemplatesList();
    }
    private void FillTemplatesList()
    {
        RISDatabaseAccessLayer db = new RISDatabaseAccessLayer();
        string query = "select tTemplates.TemplateId,tTemplates.[Name] from tTemplates "
            + " inner join tTemplateUsers on tTemplates.TemplateId = tTemplateUsers.TemplateId "
            + " inner join tModalities on tTemplates.ModalityId = tModalities.ModalityId "
            + " where tTemplateUsers.UserId = " + Request["userId"]
            + " and tModalities.Name = '" + Request["modalityName"] + "'";

        SqlConnection con = (SqlConnection)db.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        da.SelectCommand = cmd;
        da.Fill(dt);

        //Populating Drop down list of templates
        if (dt.Rows.Count > 0)
        {
            DropDownList ddlTemplates = new DropDownList();
            ddlTemplates.ID = "ddlTemplates";
            ddlTemplates.DataSource = dt;
            ddlTemplates.DataTextField = "Name";
            ddlTemplates.DataValueField = "TemplateId";
            ddlTemplates.DataBind();

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            Label label = new Label();
            label.ID = "lblTemplate";
            label.Text = "Template:";
            label.RenderControl(oHtmlTextWriter);

            ddlTemplates.RenderControl(oHtmlTextWriter);

            Response.Write(oHtmlTextWriter.InnerWriter);
            Response.Write("<input id='btnLoadTemplate' type='button' value='Apply Template' onclick='getReportText()'/>");
            Response.End();
        }
        else
        {
            Response.Write("<i>No Templates Found</i>");
            Response.End();
        }
    }

}
