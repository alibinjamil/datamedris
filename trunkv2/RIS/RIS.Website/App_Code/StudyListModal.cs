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
using System.Linq;
using System.Xml.Linq;

using RIS.RISLibrary.Database;
using RIS.RISLibrary.Utilities;
using RIS.Common;
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
    //int[] modalityIds;
    //int[] statusIds;
    int studyStatusId=0;
    string procedure = null;
    string radiologist = null;
    string physician = null;
    int examDate = 0;
    DateTime fromDate = new DateTime(1900,1,1);
    DateTime toDate = new DateTime(1900,1,1);
    DateTime Default = new DateTime(1900,1,1);
    int loggedInUserRoleId = 0;
    int loggedInUserId = 0;
    Nullable<int> clientId = null;
    //Nullable<int> hospitalId = null;


    private StudyListModal()
	{
	}

    /*public StudyListModal(int currentPage, int sortBy, string isAsc, string hdPatientId, string patientId, string patientName, string procedure, string radiologist, string physician, int examDate, int loggedInUseRoleId, int loggedInUserId,int[] StatusIds,int[] ModalityIds)
    {
        this.currentPage = currentPage;
        this.sortBy = sortBy;
        this.isAsc = isAsc;
        this.hdPatientId = hdPatientId;
        this.patientId = patientId;
        this.patientName = patientName;
        this.modalityIds = ModalityIds;
        this.statusIds = StatusIds;
        this.procedure = procedure;
        this.radiologist = radiologist;
        this.physician = physician;
        this.examDate = examDate;
        this.loggedInUserRoleId = loggedInUserRoleId;
        this.loggedInUserId = loggedInUserId; 
    }*/
    public StudyListModal(int currentPage, int sortBy, string isAsc, string hdPatientId, string patientId, string patientName, int modalityId,int studyStatusId, string procedure, string radiologist, string physician, int examDate, int loggedInUserRoleId, int loggedInUserId,Nullable<int> clientId/*,Nullable<int> hospitalId*/)
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
        this.clientId = clientId;
        //this.hospitalId = hospitalId;

    }
    //public StudyListModal(int currentPage,int sortBy,string isAsc,string hdPatientId,string patientId,string patientName,int modalityId,int studyStatusId,string procedure,string radiologist,string physician,DateTime fromDate,DateTime toDate,int loggedInUseRoleId,int loggedInUserId)
    //{
    //    this.currentPage = currentPage;
    //    this.sortBy = sortBy;
    //    this.isAsc = isAsc;
    //    this.hdPatientId = hdPatientId;
    //    this.patientId = patientId;
    //    this.patientName = patientName;
    //    this.modalityId = modalityId;
    //    this.studyStatusId = studyStatusId;
    //    this.procedure = procedure;
    //    this.radiologist = radiologist;
    //    this.physician = physician;
    //    this.fromDate = fromDate;
    //    this.examDate = -2;
    //    this.toDate = toDate;
    //    this.loggedInUserRoleId = loggedInUserRoleId;
    //    this.loggedInUserId = loggedInUserId;
    //}

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
                sqlQuery.Append(" SELECT COUNT(0) FROM Studies ");
                break;
            case QueryType.SELECT:
                sqlQuery.Append(" SELECT TOP ").Append(WebConstants.PageSize).Append(" * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() OVER (").Append(orderByQuery).Append(") AS rowNum, Studies.StudyId,Studies.PatientName AS PatientName,Studies.ExternalPatientId,Studies.StudyStatusId,StudyStatusTypes.Status,convert(varchar(10),Studies.StudyDate,101) AS StudyDate,Studies.StudyDate AS StudyTimestamp,Modalities.Name AS Modality,Procedures.Name AS ProcedureName, Radiologists.Name AS RadiologistName, Users.Name AS ReferringPhysicianName,tPatients.PatRecCount,Studies.RadiologistId,Studies.IsManual,Studies.AccessionNumber,Studies.TechComments,Studies.OriginalPatientId from Studies ");
                //queryCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                break;
            case QueryType.LOG:
                sqlQuery.Append(" INSERT INTO tStudyLog(StudyId,UserId,ActionId,ActionTime) ");
                sqlQuery.Append(" SELECT TOP ").Append(WebConstants.PageSize).Append(" StudyId,UserId,StatusTypeId,ActionTime FROM ( ");
                //sqlQuery.Append(" SELECT ROW_NUMBER() OVER (").Append(orderByQuery).Append(") AS rowNum,Studies.StudyId,Patients.PatientId,").Append(loggedInUserId).Append(" AS UserId,").Append(Constants.StudyStatusTypes.View).Append("AS StatusTypeId,GETDATE() AS ActionTime FROM Studies ");
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
                orderByQuery.Append(" StudyStatusTypes.ColumnOrder,convert(varchar(10),Studies.StudyDate,101) ");
                break;
            case 1:
                orderByQuery.Append(" Studies.PatientName ");
                break;
            case 2:
                orderByQuery.Append(" Studies.ExternalPatientId ");
                break;
            case 3:
                orderByQuery.Append(" StudyStatusTypes.Status ");
                break;
            case 4:
                orderByQuery.Append(" Studies.StudyDate ");
                break;
            case 5:
                orderByQuery.Append(" Modalities.Name ");
                break;
            case 6:
                orderByQuery.Append(" Procedures.Name ");
                break;
            case 7:
                orderByQuery.Append(" Radiologists.Name ");
                break;
            case 8:
                orderByQuery.Append(" Users.Name ");
                break;
            default:
                orderByQuery.Append(" StudyStatusTypes.ColumnOrder,convert(varchar(10),Studies.StudyDate,101) ");
                break;
        }
        if (isAsc.Equals("0"))
            orderByQuery.Append(" DESC ");
        return orderByQuery.ToString();
    }
    private string GetWhereQuery(SqlCommand command)
    {
        StringBuilder whereQuery = new StringBuilder();
        /*if (loggedInUserRoleId != Constants.Roles.Admin)
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
        {*/
        whereQuery.Append(" WHERE Studies.IsLatest = 1 ");
        //}
        if (loggedInUserRoleId != Constants.Roles.ClientAdmin 
            && loggedInUserRoleId != Constants.Roles.Admin 
            && loggedInUserRoleId != Constants.Roles.ClientTechnologist
            && loggedInUserRoleId != Constants.Roles.HospitalAdmin)
        {
            //do not show PreRelease studies
            whereQuery.Append(" AND Studies.StudyStatusId NOT IN (8,9)");//Qaed and Prerelease
        }
        if (loggedInUserRoleId == Constants.Roles.Radiologist)
        {
            whereQuery.Append(" AND StudyUsers.UserId = @UserId");
            //command.Parameters.Add(new SqlParameter("@UserId", clientId));
        }
        /*if (loggedInUserRoleId == Constants.Roles.ClientTechnologist)//show studies 
        {
            whereQuery.Append(" AND Studies.HospitalId IN (SELECT ");
            command.Parameters.Add(new SqlParameter("@HospitalId", hospitalId));
        }*/

        if (loggedInUserRoleId == Constants.Roles.ClientAdmin)
        {
            whereQuery.Append(" AND UserClients.UserId = @UserId ");
        }
        else if (loggedInUserRoleId == Constants.Roles.ClientTechnologist || loggedInUserRoleId == Constants.Roles.HospitalAdmin
            || loggedInUserRoleId == Constants.Roles.HospitalStaff )
        {
            whereQuery.Append(" AND UserHospitals.UserId = @UserId ");
        }
        else if (loggedInUserRoleId == Constants.Roles.ReferringPhysician)
        {
            whereQuery.Append(" AND UserHospitals.UserId = @UserId ");
            whereQuery.Append(" AND Studies.StudyId NOT IN ( SELECT StudyId FROM Studies INNER JOIN Users ON Studies.ReferringPhysicianId = Users.UserId WHERE Users.AllowOthers = 0 AND Users.UserId <> " + loggedInUserId + ") ");
        }

        if (this.patientName.Length > 0)
        {
            StringBuilder patientName = new StringBuilder();
            patientName.Append("%").Append(this.patientName.ToUpper()).Append("%");
            whereQuery.Append(" AND UPPER(Studies.PatientName) LIKE @PatientName ");
            command.Parameters.Add(new SqlParameter("@PatientName", patientName.ToString()));
        }
        if (this.hdPatientId.Length > 0)
        {
            whereQuery.Append(" AND Studies.ExternalPatientId = @PatientId ");
            command.Parameters.Add(new SqlParameter("@PatientId", this.hdPatientId));
        }
        else if (this.patientId.Length > 0)
        {
            StringBuilder patientId = new StringBuilder();
            patientId.Append("%").Append(this.patientId).Append("%");
            whereQuery.Append(" AND Studies.ExternalPatientId LIKE @PatientId ");
            command.Parameters.Add(new SqlParameter("@PatientId", patientId.ToString()));
        }

        if (this.modalityId > 0)
        {
            whereQuery.Append(" AND Studies.ModalityId = @ModalityId ");
            command.Parameters.Add(new SqlParameter("@ModalityId", this.modalityId));
        }
        /*if(this.modalityIds.Length>0)
        {
            string param = "";
            int i;
            for (i = 0; i < modalityIds.Length; i++)
            {
                if (modalityIds[i] == 0)
                    continue;
                if (param != "")
                    param += ",";
                param += modalityIds[i].ToString();
            }
            if(param != "")
                whereQuery.Append(" AND Studies.ModalityId in ("+param+")");
                //if (i != 0)
                //{
                //    param = "@ModalityId" + i.ToString();
                //    whereQuery.Append(" OR Studies.ModalityId=" + param);
                //    command.Parameters.AddWithValue(param, modalityIds[i]);
                //}
                //else
                //{
                //    param = "@ModalityId" + i.ToString();
                //    whereQuery.Append(" AND Studies.ModalityId=" + param);
                //    command.Parameters.AddWithValue(param, modalityIds[i]);
                //}
        }*/
        if (this.studyStatusId > 0)
        {
            whereQuery.Append(" AND Studies.StudyStatusId = @StatusId ");
            command.Parameters.Add(new SqlParameter("@StatusId", studyStatusId));
        }
        /*if(this.statusIds.Length>0)
        {
            
            string param = "";
            int i;
            for (i = 0; i < statusIds.Length; i++)
            {
                if (statusIds[i] == 0)
                    continue;
                if (param != "")
                    param += ",";
                param += statusIds[i].ToString();
            }
            if(param != "")
                whereQuery.Append(" AND Studies.StudyStatusId in ("+param+")");
                //if (i != 0)
                //{
                //    param = "@StatusId" + i.ToString();
                //    whereQuery.Append(" OR Studies.StudyStatusId=" + param);
                //    command.Parameters.AddWithValue(param, statusIds[i]);
                //}
                //else
                //{
                //    param = "@StatusId" + i.ToString();
                //    whereQuery.Append(" AND Studies.StudyStatusId=" + param);
                //    command.Parameters.AddWithValue(param, statusIds[i]);
                //}
        }*/
        if (this.procedure.Length > 0)
        {
            StringBuilder procedure = new StringBuilder();
            procedure.Append("%").Append(this.procedure).Append("%");
            whereQuery.Append(" AND UPPER(Procedures.Name) LIKE @ProcedureName ");
            command.Parameters.Add(new SqlParameter("@ProcedureName", procedure.ToString()));
        }
        if (this.radiologist.Length > 0)
        {
            StringBuilder radiologist = new StringBuilder();
            radiologist.Append("%").Append(this.radiologist).Append("%");
            whereQuery.Append(" AND UPPER(Radiologists.Name) LIKE @Radiologist ");
            command.Parameters.Add(new SqlParameter("@Radiologist", radiologist.ToString()));
        }
        if (this.physician.Length > 0)
        {
            StringBuilder physician = new StringBuilder();
            physician.Append("%").Append(this.physician).Append("%");
            whereQuery.Append(" AND UPPER(Users.Name) LIKE @Physician ");
            command.Parameters.Add(new SqlParameter("@Physician", physician.ToString()));
        }
        if (this.examDate >= 0)
        {
            if (examDate == 0 || examDate == 1)
            {
                whereQuery.Append(" AND DATEDIFF(day,Studies.StudyDate,getdate()) = @ExamDate ");
            }
            else
            {
                whereQuery.Append(" AND DATEDIFF(day,Studies.StudyDate,getdate()) >= 0 AND DATEDIFF(day,Studies.StudyDate,getdate())<= @ExamDate ");
            }
            command.Parameters.Add(new SqlParameter("@ExamDate", this.examDate));
        }
        //if (DateTime.Compare(this.fromDate,Default) > 0 && DateTime.Compare(this.toDate, Default) > 0)
        //{
        //    whereQuery.Append(" AND Studies.StudyDate>=@fromDate AND Studies.StudyDate<=@toDate ");
        //    command.Parameters.Add(new SqlParameter("@fromDate",this.fromDate));
        //    command.Parameters.Add(new SqlParameter("@toDate",this.toDate));
        //}
        
        return whereQuery.ToString();
    }

    private string GetJoinQuery(SqlCommand command)
    {
        StringBuilder joinQuery = new StringBuilder();
        //Commeting this code as this is causing perfomance issue. Replacing by EXISTS in WHERE 

        if (loggedInUserRoleId == Constants.Roles.Radiologist)
        {
            joinQuery.Append(" INNER JOIN StudyUsers On Studies.StudyId = StudyUsers.StudyId ");
            command.Parameters.Add(new SqlParameter("@UserId", loggedInUserId));
        }
        else if (loggedInUserRoleId == Constants.Roles.ClientAdmin)
        {
            joinQuery.Append(" INNER JOIN UserClients on Studies.ClientId = UserClients.ClientId ");
            command.Parameters.Add(new SqlParameter("@UserId", loggedInUserId));
        }
        else if (loggedInUserRoleId == Constants.Roles.ClientTechnologist || loggedInUserRoleId == Constants.Roles.HospitalAdmin
            || loggedInUserRoleId == Constants.Roles.HospitalStaff || loggedInUserRoleId == Constants.Roles.ReferringPhysician)
        {
            joinQuery.Append(" INNER JOIN UserHospitals on Studies.HospitalId = UserHospitals.HospitalId ");
            command.Parameters.Add(new SqlParameter("@UserId", loggedInUserId));
        }

        joinQuery.Append(" INNER JOIN ");
        joinQuery.Append(" (SELECT COUNT(0) AS PatRecCount,ExternalPatientId from Studies ");
        
        if(loggedInUserRoleId == Constants.Roles.Radiologist)
        {
            joinQuery.Append(" INNER JOIN StudyUsers on Studies.StudyId = StudyUsers.StudyId ");
            joinQuery.Append(" WHERE StudyUsers.UserId = @UserId ");
            joinQuery.Append(" AND IsLatest = 1 ");
        }
        else if (loggedInUserRoleId == Constants.Roles.ClientAdmin)
        {
            joinQuery.Append(" INNER JOIN UserClients on Studies.ClientId = UserClients.ClientId ");
            joinQuery.Append(" WHERE UserClients.UserId = @UserId ");
            joinQuery.Append(" AND IsLatest = 1 ");
        }
        else if (loggedInUserRoleId == Constants.Roles.ClientTechnologist || loggedInUserRoleId == Constants.Roles.HospitalAdmin
            || loggedInUserRoleId == Constants.Roles.HospitalStaff || loggedInUserRoleId == Constants.Roles.ReferringPhysician)
        {
            joinQuery.Append(" INNER JOIN UserHospitals on Studies.HospitalId = UserHospitals.HospitalId ");
            joinQuery.Append(" WHERE UserHospitals.UserId = @UserId ");
            joinQuery.Append(" AND IsLatest = 1 ");
        }
        else if (loggedInUserRoleId == Constants.Roles.Admin)
        {
            joinQuery.Append(" WHERE IsLatest = 1 ");
        }
        
        joinQuery.Append(" GROUP BY ExternalPatientId) tPatients ON tPatients.ExternalPatientId=Studies.ExternalPatientId ");
        joinQuery.Append(" INNER JOIN StudyStatusTypes ON StudyStatusTypes.StudyStatusTypeId = Studies.StudyStatusId ");
        joinQuery.Append(" LEFT OUTER JOIN Modalities ON Studies.ModalityId = Modalities.ModalityId ");
        joinQuery.Append(" LEFT OUTER JOIN Procedures ON Procedures.ProcedureId = Studies.ProcedureId ");
        joinQuery.Append(" LEFT OUTER JOIN Users AS Radiologists ON Studies.RadiologistId = Radiologists.UserId ");
        joinQuery.Append(" LEFT OUTER JOIN Users ON Studies.ReferringPhysicianId = Users.UserId ");
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
        /*if (reader.IsDBNull(reader.GetOrdinal("FindingId")) == false)
        {
            studyList.FindingId = (int)reader["FindingId"];
        }*/
        if (reader.IsDBNull(reader.GetOrdinal("AccessionNumber")) == false)
        {
            studyList.AccessionNumber = (string)reader["AccessionNumber"];
        }
        else
        {
            studyList.AccessionNumber = null;
        }
        studyList.PatientId = reader["ExternalPatientId"].ToString();
        studyList.PatientName = reader["PatientName"].ToString();
        studyList.OriginalPatientId = reader["OriginalPatientId"].ToString();
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
        if (reader.IsDBNull(reader.GetOrdinal("TechComments")) || reader["TechComments"].ToString().Trim().Length == 0)
        {
            studyList.TechComments = "[N/A]";
        }
        else
        {
            studyList.TechComments = (string)reader["TechComments"];
        }
        return studyList;
    } 
}
