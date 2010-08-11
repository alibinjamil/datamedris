using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;

/// <summary>
/// Summary description for LoggingService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class LoggingService : System.Web.Services.WebService {

    public LoggingService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public void Log(int studyId,int patientId,string action)
    {
        if(Session[ParameterNames.Session.LoggedInUserId] != null)
        {
            LogObject log = new LogObject();
            log.Action.Value = action;
            log.ActionTime.Value = DateTime.Now;
            log.PatientId.Value = patientId;
            log.StudyId.Value = studyId;
            log.UserId.Value = (int)Session[ParameterNames.Session.LoggedInUserId];
            log.Save();
        }
    }
}

