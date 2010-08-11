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
        RIS.RISLibrary.Database.RISDatabaseAccessLayer dl = new RIS.RISLibrary.Database.RISDatabaseAccessLayer();
        SqlConnection connection = (SqlConnection) dl.GetConnection();
        connection.Open();
        SqlCommand command = new SqlCommand();
        command.CommandText = "SELECT * FROM tTemplates";
        command.Connection = connection;
        SqlDataReader reader =  command.ExecuteReader();
        while(reader.Read())
        {

        }
    }
}
