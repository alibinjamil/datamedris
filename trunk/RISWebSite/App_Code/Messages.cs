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
/// Summary description for Messages
/// </summary>
public static class Messages
{
    public static class Error
    {
        public static string InValidUserNamePassword = "Please enter a valid username and password";
        public static string YouMustBeLoggedIn = "Please login to proceed further";
        public static string NoRolesDefined = "No roles defined for this user please contact admin";
        public static string LoginNameAlreadyExists = "The specified Login Name already exists";
        public static string PasswordsDoNotMatch = "Passwords to no match";
        public static string ErrorSavingDataToDICOM = "Unable to save data on DICOM server. Please try again later";
        public static string SessionExpired = "Your Session has expired please login again";
        public static string ConnectionError = "Error in connection";
public static string CannotDelete = "Unable to delete record as it has references else where";
    }
    public static class Information
    {
        public static string DataSaved = "Data saved successfully";
        public static string NoRecordsFound = "No matching records found";
        public static string DataUpdated = "Data updated successfully";
    }
    public static class Exception
    {
        public static string UserIdNotFound = "The specified User Id could not be found";
    }

}
