using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text;

using RIS.RISLibrary.Utilities;
/// <summary>
/// Summary description for PagesFactory
/// </summary>
public class PagesFactory
{
    private static Hashtable pages = null;

    public static class Pages
    {
        public static string ErrorPage = "ErrorPage";
        public static string WorkListPage = "WorkListPage";
        public static string LoginPage = "LoginPage";
        public static string ExamsListPage = "StudyListPage";
        public static string SelectRolePage = "SelectRolePage";
        public static string FindingPage = "FindingPage";
        public static string AddStudyGroupPage = "AddStudyGroupPage";
        public static string FindingReportPage = "FindingReportPage";
        public static string DataSavedPage = "DataSavedPage";
        public static string CloseWindowPage = "CloseWindowPage";
        public static string AddStudyPage = "AddStudyPage";
        public static string DisplayStudyPage = "DisplayStudyPage";
        public static string Dashboard = "Dashboard";
    }

    public PagesFactory()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void InstantiatePageMap()
    {
        if (pages == null)
        {
            pages = new Hashtable();
            pages.Add(Pages.ErrorPage, "../SharedPages/ErrorPage.aspx");
            pages.Add(Pages.LoginPage, "../SharedPages/Login.aspx");
            pages.Add(Pages.SelectRolePage, "../SharedPages/SelectRole.aspx");
            pages.Add(Pages.WorkListPage, "../Technologist/WorkList.aspx");
            pages.Add(Pages.ExamsListPage, "../Exams/StudyList.aspx");
            pages.Add(Pages.FindingPage, "../Exams/Finding.aspx");
            pages.Add(Pages.FindingReportPage, "../Exams/FindingReport.aspx");
            pages.Add(Pages.AddStudyGroupPage, "../Exams/AddStudyGroup.aspx");
            pages.Add(Pages.DataSavedPage, "../Technologist/DataSave.aspx");
            pages.Add(Pages.CloseWindowPage, "../Exams/CloseWindow.htm");
            pages.Add(Pages.AddStudyPage, "../Technologist/AddStudy.aspx");
            pages.Add(Pages.DisplayStudyPage, "../WebViewer/DisplayStudyPage.aspx");
            pages.Add(Pages.Dashboard, "../SharedPages/Dashboard.aspx");
        }
    }

    public static void Transfer(String page)
    {
        if (pages != null)
        {
            string path = (string)pages[page];
            //HttpContext.Current.Response.Redirect(path);
            HttpContext.Current.Response.Redirect(path);
        }
    }
    
    public static void Transfer(String page,String arguments)
    {
        if (pages != null)
        {
            StringBuilder path = new StringBuilder((string)pages[page]);
            path.Append("?");
            path.Append(arguments);
            HttpContext.Current.Response.Redirect(path.ToString());
        }
    }
    public static string GetUrl(String page)
    {
        if (pages != null)
        {
            return (string)pages[page];
        }
        return null;
    }

    public static void TransferAfterLogin(int roleId)
    {
        if (roleId == Constants.Roles.Radiologist)
        {
            PagesFactory.Transfer(Pages.ExamsListPage);

            //Response.Redirect("~/Radiologist/StudyList.aspx");
        }
        else if (roleId == Constants.Roles.Technologist)
        {
            PagesFactory.Transfer(Pages.WorkListPage);
        }
        else if (roleId == Constants.Roles.ReferringPhysician)
        {
            PagesFactory.Transfer(Pages.ExamsListPage);
        }
        else if (roleId == Constants.Roles.Admin)
        {
            PagesFactory.Transfer(Pages.ExamsListPage);
        }
        else if (roleId == Constants.Roles.Transcriptionist)
        {
            PagesFactory.Transfer(Pages.ExamsListPage);
        }
        else if (roleId == Constants.Roles.ChiefTechnologist)
        {
            PagesFactory.Transfer(Pages.ExamsListPage);
        }
        else if (roleId == Constants.Roles.ClientAdmin)
        {
            PagesFactory.Transfer(Pages.ExamsListPage);
        }
        else if (roleId == Constants.Roles.ClientTechnologist)
        {
            PagesFactory.Transfer(Pages.ExamsListPage);
        }
        else if (roleId == Constants.Roles.HospitalAdmin)
        {
            PagesFactory.Transfer(Pages.ExamsListPage);
        }
        else if (roleId == Constants.Roles.HospitalStaff)
        {
            PagesFactory.Transfer(Pages.ExamsListPage);
        }
    }
}
