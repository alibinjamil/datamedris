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
using System.Web.Services;
using System.Web.Services.Protocols;
using RIS.RISLibrary.Database;
using System.Data.SqlClient;


/// <summary>
/// Summary description for TemplatesData
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class TemplatesData : System.Web.Services.WebService
{

    public TemplatesData()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod]
    public string TemplateText(int templateId)
    {
        RISDatabaseAccessLayer db = new RISDatabaseAccessLayer();
        SqlConnection con = (SqlConnection)db.GetConnection(); 
        string query = "select Text from tTemplates where TemplateId="+templateId.ToString();
        SqlCommand cmd = new SqlCommand(query,con);
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        da.SelectCommand = cmd;
        da.Fill(dt);
        string report = dt.Rows[0][0].ToString();
        return report;
    }

}

