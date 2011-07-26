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

public partial class Common_SecretQuestions : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int SecretQuestionId
    {
        get { return int.Parse(ddlSecretQuestions.SelectedValue); }
        set { ddlSecretQuestions.SelectedValue = value.ToString(); }
    }
}
