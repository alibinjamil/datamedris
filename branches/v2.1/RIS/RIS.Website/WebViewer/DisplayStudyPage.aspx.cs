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

using RIS.Common;
using RIS.RISLibrary.Utilities;

public partial class WebViewer_DiplayStudyPage : StudyPage
{
    StringBuilder appletParams = new StringBuilder();
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        Study study = GetStudy();
        if(study != null)
        {
            int count = 1;
            foreach(Series series in study.Series)
            {
                foreach(RIS.Common.Image image in series.Images)
                {
                    appletParams.Append(getAppletParam(count++,image.Path));
                }
            }
            if (appletParams.Length > 0)
            {                
                Log(study);
            }
            else
            {
                Session[ParameterNames.Session.ErrorMessage] = "No files found for the study";
                Response.Redirect("~/SharedPages/ErrorPage.aspx");
            }
        }
    }

    protected void Log(Study study)
    {
        Log log = new Log();
        log.Action =   Constants.LogActions.ViewedExam;
        log.ActionTime = DateTime.Now;
        log.StudyId = study.StudyId;
        log.UserId = loggedInUserId;
        DatabaseContext.AddToLogs(log);
        DatabaseContext.SaveChanges();
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
