using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;
using System.Data.SqlClient;

using RIS.RISLibrary.Database;

/// <summary>
/// Summary description for RecordCountService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class RecordCountService : System.Web.Services.WebService {

    public RecordCountService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    
    [WebMethod]
    public string GetAllCounts(int loggedInUserId,int loggedInUserRoleId)
    {
        return GetCount(0,loggedInUserId,loggedInUserRoleId);
    }
    
    [WebMethod]
    public string GetNewCounts(int loggedInUserId, int loggedInUserRoleId)
    {
        return GetCount(1, loggedInUserId, loggedInUserRoleId);
    }

    [WebMethod]
    public string GetUserCounts(string loggedInUserName,int loggedInUserId, int loggedInUserRoleId)
    {
        StringBuilder counts = new StringBuilder();
        int[] statusCounts = { 0, 4, 5, 7};
        foreach (int statusCount in statusCounts)
        {
            StudyListModal modal = new StudyListModal(0, 0, "", "", "", "", 0, statusCount, "", loggedInUserName, "", 30, loggedInUserRoleId, loggedInUserId);
            counts.Append(statusCount);
            counts.Append("=");
            counts.Append(modal.GetRecordCount());
            counts.Append(",");
        }
        counts.Remove(counts.Length - 1, 1);
        return counts.ToString();
    }

    private string GetCount(int studyStatusTypeId,int loggedInUserId, int loggedInUserRoleId)
    {
        StringBuilder counts = new StringBuilder();
        int[] dayCounts = { 0, 1, 3, 7, 30 };
        foreach (int dayCount in dayCounts)
        {
            StudyListModal modal = new StudyListModal(0, 0,"","", "", "", 0,studyStatusTypeId,"", "", "", dayCount, loggedInUserRoleId, loggedInUserId);
            counts.Append(dayCount);
            counts.Append("=");
            counts.Append(modal.GetRecordCount());
            counts.Append(",");
        }
        counts.Remove(counts.Length - 1, 1);
        return counts.ToString();
    }

    private string GetAllQuery()
    {
        return "SELECT COUNT(0) FROM tStudies WHERE DATEDIFF(day,StudyDate,getDate()) = @ExamDays";
    }

    private string GetNewQuery()
    {
        return "SELECT COUNT(0) FROM tStudies WHERE DATEDIFF(day,StudyDate,getDate()) = @ExamDays AND StudyStatusId = 1";
    }

    private string GetUserQuery(int studyStatusId)
    {
        StringBuilder query = new StringBuilder();
        query.Append(" SELECT COUNT(DISTINCT tStudies.StudyId) FROM tStudies ");
        query.Append(" INNER JOIN tFindings ON tFindings.StudyId = tStudies.StudyId ");
        query.Append(" WHERE DATEDIFF(day,StudyDate,getDate()) = 30 ");
        query.Append(" AND tFindings.AudioUserId = @UserId  ");
        if(studyStatusId > 0)
            query.Append(" AND tStudies.StudyStatusId = ").Append(studyStatusId);
        return query.ToString();
    }
}

