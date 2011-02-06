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


using RIS.RISLibrary.Utilities;
using RIS.Common;
public partial class Radiologist_RejectExam : StudyPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (loggedInUserRoleId == Constants.Roles.Radiologist)
        {
            btnReject.Visible = true;
        }
        if (IsPostBack == false)
        {
            Study study = GetStudy();
            if (study != null && study.RejectionReason != null)
            {
                tbRejectionReason.Text = study.RejectionReason;
            }
        }
    }
    protected override bool IsPopUp()
    {
        return true;
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        Study study = GetStudy();
        if (study != null)
        {
            study.RejectionReason = tbRejectionReason.Text;
            study.StudyStatusId = Constants.StudyStatusTypes.Rejected;
            DatabaseContext.SaveChanges();
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Close", "parent.closeRejectionWindow();parent.aspnetForm.submit();", true);
    }
}
