using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ParameterNames
/// </summary>
public static class ParameterNames
{
    public static class Session
    {
        public static string LoggedInUser = "LoggedInUser";
        public static string LoggedInUserId = "LoggedInUserId";
        public static string ExceptionString = "ExceptionString";
        public static string LoggedInUserRoleId = "LoggedInUserRoleId";
        public static string ErrorMessage = "ErrorMessage";
        public static string InformationMessage = "InformationMessage";
        public static string LastLoginTime = "LastLoginTime";
        public static string Template = "Template";
        //public static string LoggedInUserClientId = "LoggedInUserClientId";
    }
    public static class Request
    {
        public static string StudyId = "StudyId";
        public static string StudyGroupId = "StudyGroupId";
        public static string FindingId = "FindingId";
        public static string PatientId = "PatientId";
        public static string PatientName = "PatientName";
        public static string ExternalPatientId = "ExternalPatientId";
        public static string ReturnPage = "ReturnPage";
        public static string GroupId = "GroupId";
        public static string ClientId = "ClientId";
        public static string TemplateId = "TemplateId";
        public static string UserId = "UserId";
        public static string Name = "Name";
        public static string Description = "Description";
    }
    public static class Cookie
    {
        public static string ClientId = "ClientId";
    }
}
