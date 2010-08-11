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
public partial class SharedPages_ForgotPassword : GenericPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        UserObject user = new UserObject();
        user.LoginName.Value = tbLoginName.Text;
        user.Load();
        if (user.IsLoaded && user.SecretQuestionId.Value != null && user.Answer.Value != null
            && (int)user.SecretQuestionId.Value == SecretQuestions1.SecretQuestionId
            && user.Answer.Value.ToString().Equals(tbAnswer.Text))
        {
            tbLoginName.Enabled = false;
            pnlPassword.Visible = true;
            pnlSecretQuestion.Visible = false;
        }
        else
        {
            SetErrorMessage("Please enter the correct information");
        }
    }

    protected void btnResetPassword2_Click(object sender, EventArgs e)
    {
        UserObject user = new UserObject();
        user.LoginName.Value = tbLoginName.Text;
        user.Load();
        if (user.IsLoaded && tbPassword.Text.Equals(tbConfirmPassword.Text))
        {
            user.Password.Value = tbPassword.Text;
            user.Save();
            Response.Redirect("~/SharedPages/Login.aspx");
        }
    }
}
