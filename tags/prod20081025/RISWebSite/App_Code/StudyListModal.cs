using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using RIS.RISLibrary.Database;
using RIS.RISLibrary.Utilities;
/// <summary>
/// Summary description for StudyListModal
/// </summary>
public class StudyListModal
{
    enum QueryType
    {
        COUNT,
        SELECT,
        LOG
    }
    int currentPage = 0;
    int sortBy = 0;
    string isAsc = null;
    string hdPatientId = null;
    string patientId = null;
    string patientName = null;
    int modalityId = 0;
    int studyStatusId = 0;
    string procedure = null;
    string radiologist = null;
    string physician = null;
    int examDate = 0;
    int loggedInUserRoleId = 0;
    int loggedInUserId = 0;
    
    private StudyListModal()
	{
	}

    public StudyListModal(int currentPage,int sortBy,string isAsc,string hdPatientId,string patientId,string patientName,int modalityId,int studyStatusId,string procedure,string radiologist,string physician,int examDate,int loggedInUseRoleId,int loggedInUserId)
    {
        this.currentPage = currentPage;
        this.sortBy = sortBy;
        this.isAsc = isAsc;
        this.hdPatientId = hdPatientId;
        this.patientId = patientId;
        this.patientName = patientName;
        this.modalityId = modalityId;
        this.studyStatusId = studyStatusId;
        this.procedure = procedure;
        this.radiologist = radiologist;
        this.physician = physician;
        this.examDate = examDate;
        this.loggedInUserRoleId = loggedInUserRoleId;
        this.loggedInUserId = loggedInUserId; 
    }

    public int GetRecordCount()
    {
        SqlConnection connection = null;
        int recCount = 0;
        try
        {
            RISDatabaseAccessLayer dal = new RISDatabaseAccessLayer();
            connection = (SqlConnection)dal.GetConnection();
            connection.Open();
            SqlCommand command = GetQuery(QueryType.COUNT);
            command.Connection = connection;
            recCount = (int)command.ExecuteScalar();
        }
        finally
        {
            if (connection != null) connection.Close();
        }
        return recCount;
    }

    public List<StudyListPageObject> GetData()
    {
        SqlConnection connection = null;
        List<StudyListPageObject> data = new List<StudyListPageObject>();
        try
        {
            RISDatabaseAccessLayer dal = new RISDatabaseAccessLayer();
            connection = (SqlConnection)dal.GetConnection();
            connection.Open();
            SqlCommand command = GetQuery(QueryType.SELECT);
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                data.Add(GetStudyListPageObject(reader));
            }
            reader.Close();
        }
        finally
        {
            if(connection != null)connection.Close();
        }
        return data;
    }

    private SqlCommand GetQuery(QueryType queryType)
    {
        SqlCommand queryCommand = new SqlCommand();
        string orderByQuery = GetOrderByQuery();
        string joinQuery = GetJoinQuery(queryCommand);
        string whereQuery = GetWhereQuery(queryCommand);
        StringBuilder sqlQuery = new StringBuilder();
        switch(queryType)
        {
            case QueryType.COUNT:
                sqlQuery.Append(" SELECT COUNT(0) FROM tStudies ");
                break;
            case QueryType.SELECT:
                sqlQuery.Append(" SELECT TOP ").Append(WebConstants.PageSize).Append(" * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() OVER (").Append(orderByQuery).Append(") AS rowNum, tStudies.StudyId,tPatients.Name AS PatientName,tPatients.ExternalPatientId,tStudies.StudyStatusId,tStudyStatusTypes.Status,convert(varchar(10),tStudies.StudyDate,101) AS StudyDate,tStudies.StudyDate AS StudyTimestamp,tModalities.Name AS Modality,tProcedures.Name AS ProcedureName, ttFindings.RadiologistName, tUsers.Name AS ReferringPhysicianName,ttFindings.FindingId,ttPatients.PatRecCount,ttFindings.RadiologistId,tStudies.IsManual from tStudies ");
                //queryCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                break;
            case QueryType.LOG:
                sqlQuery.Append(" INSERT INTO tStudyLog(StudyId,PatientId,UserId,ActionId,ActionTime) ");
                sqlQuery.Append(" SELECT TOP ").Append(WebConstants.PageSize).Append(" StudyId,PatientId,UserId,StatusTypeId,ActionTime FROM ( ");
                //sqlQuery.Append(" SELECT ROW_NUMBER() OVER (").Append(orderByQuery).Append(") AS rowNum,tStudies.StudyId,tPatients.PatientId,").Append(loggedInUserId).Append(" AS UserId,").Append(Constants.StudyStatusTypes.View).Append("AS StatusTypeId,GETDATE() AS ActionTime FROM tStudies ");
                break;
        }
        sqlQuery.Append(joinQuery);
        sqlQuery.Append(whereQuery);
        if (queryType == QueryType.SELECT || queryType == QueryType.LOG)
        {
            sqlQuery.Append(")AS A WHERE rowNum > @MaxRecords");
            queryCommand.Parameters.Add(new SqlParameter("@MaxRecords", (currentPage - 1) * WebConstants.PageSize));
        }
        //Console.WriteLine(sqlQuery.ToString());
        queryCommand.CommandText = sqlQuery.ToString();
        return queryCommand;
    }

    private string GetOrderByQuery()
    {
        StringBuilder orderByQuery = new StringBuilder(" ORDER BY ");
        switch (sortBy)
        {
            case 0:
                orderByQuery.Append(" tStudyStatusTypes.ColumnOrder,convert(varchar(10),tStudies.StudyDate,101) ");
                break;
            case 1:
                orderByQuery.Append(" tPatients.Name ");
                break;
            case 2:
                orderByQuery.Append(" tPatients.ExternalPatientId ");
                break;
            case 3:
                orderByQuery.Append(" tStudyStatusTypes.Status ");
                break;
            case 4:
                orderByQuery.Append(" tStudies.StudyDate ");
                break;
            case 5:
                orderByQuery.Append(" tModalities.Name ");
                break;
            case 6:
                orderByQuery.Append(" tProcedures.Name ");
                break;
            case 7:
                orderByQuery.Append(" ttFindings.RadiologistName ");
                break;
            case 8:
                orderByQuery.Append(" tUsers.Name ");
                break;
            default:
                orderByQuery.Append(" tStudyStatusTypes.ColumnOrder,convert(varchar(10),tStudies.StudyDate,101) ");
                break;
        }
        if (isAsc.Equals("0"))
            orderByQuery.Append(" DESC ");
        return orderByQuery.ToString();
    }
    private string GetWhereQuery(SqlCommand command)
    {
        StringBuilder whereQuery = new StringBuilder();
        if (loggedInUserRoleId != Constants.Roles.Admin)
        {
            whereQuery.Append(" WHERE EXISTS ( ");
            whereQuery.Append("  SELECT tStudyGroups.StudyId ");
            whereQuery.Append("  FROM tStudyGroups ");
            whereQuery.Append("  INNER JOIN tUserGroups ON tStudyGroups.GroupId = tUserGroups.GroupId ");
            whereQuery.Append("  WHERE tUserGroups.UserId = @UserId ");
            whereQuery.Append(" ) ");
            command.Parameters.Add(new SqlParameter("@UserId", loggedInUserId));
        }
        else
        {
            whereQuery.Append(" WHERE 1 = 1 ");
        }
        if (this.hdPatientId.Length > 0)
        {
            whereQuery.Append(" AND tPatients.ExternalPatientId = @PatientId ");
            command.Parameters.Add(new SqlParameter("@PatientId", this.hdPatientId));
        }
        if (this.patientName.Length > 0)
        {
            StringBuilder patientName = new StringBuilder();
            patientName.Append("%").Append(this.patientName.ToUpper()).Append("%");
            whereQuery.Append(" AND UPPER(tPatients.Name) LIKE @PatientName ");
            command.Parameters.Add(new SqlParameter("@PatientName", patientName.ToString()));
        }
        if (this.patientId.Length > 0)
        {
            StringBuilder patientId = new StringBuilder();
            patientId.Append("%").Append(this.patientId).Append("%");
            whereQuery.Append(" AND tPatients.ExternalPatientId LIKE @PatientId ");
            command.Parameters.Add(new SqlParameter("@PatientId", patientId.ToString()));
        }
        if (this.modalityId > 0)
        {
            whereQuery.Append(" AND tStudies.ModalityId = @ModalityId ");
            command.Parameters.Add(new SqlParameter("@ModalityId", this.modalityId));
        }
        if (this.studyStatusId > 0)
        {
            whereQuery.Append(" AND tStudies.StudyStatusId = @StatusId ");
            command.Parameters.Add(new SqlParameter("@StatusId", this.studyStatusId));
        }
        if (this.procedure.Length > 0)
        {
            StringBuilder procedure = new StringBuilder();
            procedure.Append("%").Append(this.procedure).Append("%");
            whereQuery.Append(" AND UPPER(tProcedures.Name) LIKE @ProcedureName ");
            command.Parameters.Add(new SqlParameter("@ProcedureName", procedure.ToString()));
        }
        if (this.radiologist.Length > 0)
        {
            StringBuilder radiologist = new StringBuilder();
            radiologist.Append("%").Append(this.radiologist).Append("%");
            whereQuery.Append(" AND UPPER(ttFindings.RadiologistName) LIKE @Radiologist ");
            command.Parameters.Add(new SqlParameter("@Radiologist", radiologist.ToString()));
        }
        if (this.physician.Length > 0)
        {
            StringBuilder physician = new StringBuilder();
            physician.Append("%").Append(this.physician).Append("%");
            whereQuery.Append(" AND UPPER(tUsers.Name) LIKE @Physician ");
            command.Parameters.Add(new SqlParameter("@Physician", physician.ToString()));
        }
        if (this.examDate >= 0)
        {
            if (examDate == 0 || examDate == 1)
            {
                whereQuery.Append(" AND DATEDIFF(day,tStudies.StudyDate,getdate()) = @ExamDate ");
            }
            else
            {
                whereQuery.Append(" AND DATEDIFF(day,tStudies.StudyDate,getdate()) >= 0 AND DATEDIFF(day,tStudies.StudyDate,getdate())<= @ExamDate ");
            }
            command.Parameters.Add(new SqlParameter("@ExamDate", this.examDate));
        }
        return whereQuery.ToString();
    }

    private string GetJoinQuery(SqlCommand command)
    {
        StringBuilder joinQuery = new StringBuilder();
        //Commeting this code as this is causing perfomance issue. Replacing by EXISTS in WHERE 
        /*if (loggedInUserRoleId != Constants.Roles.Admin)
        {
            joinQuery.Append(" INNER JOIN ( ");
            joinQuery.Append("  SELECT DISTINCT tStudies.StudyId AS UniqueStudyId FROM tStudies ");
            joinQuery.Append("  INNER JOIN tStudyGroups ON tStudyGroups.StudyId = tStudies.StudyId ");
            joinQuery.Append("  INNER JOIN tUserGroups ON tStudyGroups.GroupId = tUserGroups.GroupId ");
            joinQuery.Append("  WHERE tUserGroups.UserId = @UserId ");
            joinQuery.Append(" ) ttStudies ON ttStudies.UniqueStudyId = tStudies.StudyId ");
            command.Parameters.Add(new SqlParameter("@UserId", loggedInUserId));
        }*/
        joinQuery.Append(" INNER JOIN tPatients ON tPatients.PatientId = tStudies.PatientId ");
        joinQuery.Append(" INNER JOIN (SELECT COUNT(0) AS PatRecCount,PatientId from tStudies GROUP BY PatientId) ttPatients ON ttPatients.PatientId=tStudies.PatientId ");
        joinQuery.Append(" INNER JOIN tStudyStatusTypes ON tStudyStatusTypes.StudyStatusTypeId = tStudies.StudyStatusId ");
        joinQuery.Append(" LEFT OUTER JOIN tModalities ON tStudies.ModalityId = tModalities.ModalityId ");
        joinQuery.Append(" LEFT OUTER JOIN tProcedures ON tProcedures.ProcedureId = tStudies.ProcedureId ");
        joinQuery.Append(" LEFT OUTER JOIN ( ");
        joinQuery.Append("   SELECT tFindings.FindingId,tFindings.AudioUserId AS RadiologistId, tFindings.AudioUserName AS RadiologistName, tFindings.AudioReportPath,tFindings.TextualTranscript,ttTranscriptionists.UserId as TranscriptionistUserId,ttTranscriptionists.Name AS TranscriptionistName ");
        joinQuery.Append("   FROM tFindings ");        
        joinQuery.Append("   LEFT OUTER JOIN tUsers AS ttTranscriptionists ON ttTranscriptionists.UserId = TranscriptUserId ");
        joinQuery.Append(" )ttFindings ON ttFindings.FindingId = tStudies.LatestFindingId ");
        joinQuery.Append(" LEFT OUTER JOIN tUsers ON tStudies.ReferringPhysicianId = tUsers.UserId ");
        return joinQuery.ToString();
    }

    private StudyListPageObject GetStudyListPageObject(SqlDataReader reader)
    {
        StudyListPageObject studyList = new StudyListPageObject();
        studyList.PatientRecordCount = (int)reader["PatRecCount"];
        studyList.StudyId = (int)reader["StudyId"];
        studyList.StatusId = (int)reader["StudyStatusId"];
        studyList.Status = reader["Status"].ToString();
        studyList.StudyDate = reader["StudyDate"].ToString();
        studyList.StudyTimeStamp = (DateTime)reader["StudyTimeStamp"];
        if (reader.IsDBNull(reader.GetOrdinal("FindingId")) == false)
        {
            studyList.FindingId = (int)reader["FindingId"];
        }
        studyList.PatientId = reader["ExternalPatientId"].ToString();
        studyList.PatientName = reader["PatientName"].ToString();
        studyList.Modality = reader["Modality"].ToString();
        studyList.Procedure = reader["ProcedureName"].ToString();
        studyList.Radiologist = reader["RadiologistName"].ToString();
        int radiologistIdOrdinal = reader.GetOrdinal("RadiologistId");
        if(reader.IsDBNull(radiologistIdOrdinal) == false)
        {
            studyList.RadiologistId = reader.GetInt32(radiologistIdOrdinal);
        }
        if (reader.IsDBNull(reader.GetOrdinal("ReferringPhysicianName")))
        {
            studyList.Physician = "(None)";
        }
        else
        {
            studyList.Physician = reader["ReferringPhysicianName"].ToString();
        }
        if (reader.IsDBNull(reader.GetOrdinal("IsManual")) == false)
        {
            studyList.IsManual = (string)reader["IsManual"];
        }
        else
        {
            studyList.IsManual = "N";
        }

        return studyList;
    }

 
}
