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

using System.Linq;
using System.Xml.Linq;

public partial class AdminPages_ManageTemplates : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        gvTemplates.DataSource = (from tu in DatabaseContext.TemplateUsers
                                  where tu.UserId == loggedInUserId
                                  select new 
                                  { 
                                      TemplateId = tu.TemplateId,
                                      ModalityName = tu.Template.BodyPart.Modality.Name,
                                      BodyPart = tu.Template.BodyPart.Name,
                                      TemplateName = tu.Template.Name,
                                      Heading = tu.Template.Heading,
                                      Description = tu.Template.Description,
                                      Impression = tu.Template.Impression
                                  });
        gvTemplates.DataBind();
    }
    protected override bool IsPopUp()
    {
        return false;
    }
}
