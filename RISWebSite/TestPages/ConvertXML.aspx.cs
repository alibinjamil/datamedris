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
using System.Data.SqlClient;

public partial class TestPages_ConvertXML : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string html = "FANNIE%20CARATHERS";
        string temp = Server.HtmlDecode(html);
        System.IO.StringWriter sw = new System.IO.StringWriter();
        Server.HtmlDecode(html, sw);
        temp = Server.UrlDecode(html);
    }
}
