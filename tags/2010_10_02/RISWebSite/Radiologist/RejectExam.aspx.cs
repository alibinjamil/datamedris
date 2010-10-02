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
using RIS.RISLibrary.Utilities;
public partial class Radiologist_RejectExam : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (loggedInUserRoleId == Constants.Roles.Radiologist)
        {
            btnReject.Visible = true;
        }
        if (IsPostBack == false)
        {
            StudyObject study = new StudyObject();
            study.StudyId.Value = Request[ParameterNames.Request.StudyId];
            study.Load(loggedInUserId);
            if (study.IsLoaded && study.RejectionReason.Value != null)
            {
                tbRejectionReason.Text = study.RejectionReason.Value.ToString();
            }
        }
    }
    protected override bool IsPopUp()
    {
        return true;
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        StudyObject study = new StudyObject();
        study.StudyId.Value = Request[ParameterNames.Request.StudyId];
        study.Load(loggedInUserId);
        if (study.IsLoaded)
        {
            study.RejectionReason.Value = tbRejectionReason.Text;
            study.StudyStatusId.Value = Constants.StudyStatusTypes.Rejected;
            study.Save(loggedInUserId);
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Close", "parent.closeRejectionWindow();parent.aspnetForm.submit();", true);
    }
}
