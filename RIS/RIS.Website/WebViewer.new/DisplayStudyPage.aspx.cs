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
using System.IO;

using System.Data.SqlClient;
using System.Text;

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;
using RIS.RISLibrary.Fields;
using RIS.RISLibrary.Utilities;

public partial class WebViewer_DiplayStudyPage : AuthenticatedPage
{
    StringBuilder appletParams = new StringBuilder();
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        int studyId = int.Parse(Request[ParameterNames.Request.StudyId]);
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;
        try
        {
           
            StringBuilder query = new StringBuilder(" SELECT tImages.Path FROM tImages ");
            query.Append(" INNER JOIN tSeries ON tSeries.SeriesId = tImages.SeriesId ");
            query.Append(" WHERE tSeries.StudyId = @StudyId ");

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
            if (appletParams.Length > 0)
            {                
                Log(studyId);
            }
            else
            {
                Session[ParameterNames.Session.ErrorMessage] = "No files found for the study";
                Response.Redirect("~/SharedPages/ErrorPage.aspx");
            }
        }
        finally
        {
            if (reader != null) reader.Close();
            //if (command != null) command.Close();
            if (connection != null) connection.Close();
        }
    }

    protected void Log(int studyId)
    {
        StudyObject study = new StudyObject();
        study.StudyId.Value = studyId;
        study.Load();
        if (study.IsLoaded)
        {
            LogObject log = new LogObject();
            log.Action.Value = Constants.LogActions.ViewedExam;
            log.ActionTime.Value = DateTime.Now;
            log.PatientId.Value = study.PatientId.Value;
            log.StudyId.Value = studyId;
            log.UserId.Value = loggedInUserId;
            log.Save();
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
                if(File.Exists(completePath.ToString()))
                {
                    appletParam.Append(" <PARAM NAME=DicomImg").Append(count).Append(" VALUE=\"");
                    appletParam.Append(completePath);
                    appletParam.Append("\" /> ");         
                }
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
