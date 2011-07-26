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

public partial class Technologist_DataSave : AuthenticatedPage
{
    override protected void Page_Load_Extended(object sender, EventArgs e)
    {
        lblPatientName.Text = Request[ParameterNames.Request.PatientName];
        lblPatientId.Value = Request[ParameterNames.Request.ExternalPatientId];
    }
    protected override bool IsPopUp()
    {
        return false;
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        string args = ParameterNames.Request.ExternalPatientId + "=" + lblPatientId.Value;
        if (Request[ParameterNames.Request.ReturnPage] != null)
        {
            PagesFactory.Transfer(Request[ParameterNames.Request.ReturnPage], args);
        }
        else
        {
            PagesFactory.Transfer(PagesFactory.Pages.WorkListPage, args);
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        if (Request[ParameterNames.Request.ReturnPage] != null)
        {
            PagesFactory.Transfer(Request[ParameterNames.Request.ReturnPage]);
        }
        else
        {
            PagesFactory.Transfer(PagesFactory.Pages.WorkListPage);
        }
    }
}
