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

using System.Data.SqlClient;
using System.Text;

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;
using RIS.RISLibrary.Fields;

public partial class WebViewer_DiplayStudyPage : AuthenticatedPage
{
    StringBuilder appletParams = new StringBuilder();
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;
        try
        {
            int studyId = int.Parse(Request[ParameterNames.Request.StudyId]);
            StringBuilder query = new StringBuilder(" SELECT tImages.Path FROM tImages ");
            query.Append(" INNER JOIN tSeries ON tSeries.SeriesId = tImages.SeriesId ");
            query.Append(" INNER JOIN tStudies ON tSeries.StudyId = tStudies.StudyId ");
            query.Append(" WHERE tStudies.StudyId = @StudyId ");

            RISDatabaseAccessLayer risDatabase = new RISDatabaseAccessLayer();
            connection = (SqlConnection)risDatabase.GetConnection();
            connection.Open();
            command = new SqlCommand(query.ToString(), connection);
            command.Parameters.AddWithValue("@StudyId", studyId);
            reader = command.ExecuteReader();
            int count = 1;
            while (reader.Read())
            {
                appletParams.Append(getAppletParam(count++, reader.GetString(0)));
            }
        }
        finally
        {
            if (reader != null) reader.Close();
            //if (command != null) command.Close();
            if (connection != null) connection.Close();
        }
    }

    protected string Parameters
    {
        get
        {
            return appletParams.ToString();
        }
    }

    private string getAppletParam(int count,String imagePath)
    {
        StringBuilder appletParam = new StringBuilder();
        string isWeb = ConfigurationManager.AppSettings["IsWeb"];
        if (null != isWeb)
        {
            StringBuilder completePath = new StringBuilder();
            if (isWeb.Equals("true"))
            {
                completePath.Append("http://").Append(Request.Url.Authority);
                if (Request.ApplicationPath.Length > 0)
                    completePath.Append(Request.ApplicationPath);
            }
            string imagesDirectory = ConfigurationManager.AppSettings["ImagesDirectory"];
            if (null != imagesDirectory)
            {
                completePath.Append(imagesDirectory);
                completePath.Append(imagePath);
                appletParam.Append(" <PARAM NAME=DicomImg").Append(count).Append(" VALUE=\"");
                appletParam.Append(completePath);
                appletParam.Append("\" /> ");         
            }
        }
        return appletParam.ToString();
    }

    protected override bool IsPopUp()
    {
        Session[ParameterNames.Session.ErrorMessage] = Messages.Error.SessionExpired;
        return true;
    }
}
