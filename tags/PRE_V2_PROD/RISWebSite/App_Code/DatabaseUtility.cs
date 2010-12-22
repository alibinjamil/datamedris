using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

using RIS.RISLibrary.Database;

/// <summary>
/// Summary description for DatabaseUtility
/// </summary>
public static class DatabaseUtility
{
    public static void BindUserDDL(int roleId, string addText, DropDownList ddl)
    {
        RISDatabaseAccessLayer dataAccess = new RISDatabaseAccessLayer();
        SqlConnection connection = (SqlConnection)dataAccess.GetConnection();
        connection.Open();
        SqlCommand command = new SqlCommand("sp_get_users_for_role", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@roleId", roleId);
        command.Parameters.AddWithValue("@addText", addText);
        SqlDataReader reader = command.ExecuteReader();
        ddl.DataSource = reader;
        ddl.DataMember = "Name";
        ddl.DataTextField = "Name";
        ddl.DataValueField = "UserId";
        ddl.DataBind();
        reader.Close();
        connection.Close();        
    }

    public static void BindModalitiesDDL(string addText, CheckBoxList ddl)
    {
        RISDatabaseAccessLayer dataAccess = new RISDatabaseAccessLayer();
        SqlConnection connection = (SqlConnection)dataAccess.GetConnection();
        connection.Open();
        SqlCommand command = new SqlCommand("sp_get_modalities", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@addText", addText);
        SqlDataReader reader = command.ExecuteReader();
        ddl.DataSource = reader;
        ddl.DataMember = "Name";
        ddl.DataTextField = "Name";
        ddl.DataValueField = "ModalityId";
        ddl.DataBind();
        reader.Close();
        connection.Close();
    }
    public static void BindModalitiesDDL(string addText, DropDownList ddl)
    {
        RISDatabaseAccessLayer dataAccess = new RISDatabaseAccessLayer();
        SqlConnection connection = (SqlConnection)dataAccess.GetConnection();
        connection.Open();
        SqlCommand command = new SqlCommand("sp_get_modalities", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@addText", addText);
        SqlDataReader reader = command.ExecuteReader();
        ddl.DataSource = reader;
        ddl.DataMember = "Name";
        ddl.DataTextField = "Name";
        ddl.DataValueField = "ModalityId";
        ddl.DataBind();
        reader.Close();
        connection.Close();
    }
    public static void BindStudyStatusTypesDDL(string addText, DropDownList ddl)
    {
        RISDatabaseAccessLayer dataAccess = new RISDatabaseAccessLayer();
        SqlConnection connection = (SqlConnection)dataAccess.GetConnection();
        connection.Open();
        SqlCommand command = new SqlCommand("sp_get_study_status_types", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@addText", addText);
        SqlDataReader reader = command.ExecuteReader();
        ddl.DataSource = reader;
        ddl.DataMember = "Status";
        ddl.DataTextField = "Status";
        ddl.DataValueField = "StudyStatusTypeId";
        ddl.DataBind();
        reader.Close();
        connection.Close();
    }

    public static void BindProceduresDDL(string addText, int modalityId, DropDownList ddl)
    {
        RISDatabaseAccessLayer dataAccess = new RISDatabaseAccessLayer();
        SqlConnection connection = (SqlConnection)dataAccess.GetConnection();
        connection.Open();
        SqlCommand command = new SqlCommand("sp_get_procedures_for_modality", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@addText", addText);
        command.Parameters.AddWithValue("@modalityId", modalityId);
        SqlDataReader reader = command.ExecuteReader();
        ddl.DataSource = reader;
        ddl.DataMember = "Name";
        ddl.DataTextField = "Name";
        ddl.DataValueField = "ProcedureId";
        ddl.DataBind();
        reader.Close();
        connection.Close();
    }

    public static void BindStationsDDL(string addText, int modalityId, DropDownList ddl)
    {
        RISDatabaseAccessLayer dataAccess = new RISDatabaseAccessLayer();
        SqlConnection connection = (SqlConnection)dataAccess.GetConnection();
        connection.Open();
        SqlCommand command = new SqlCommand("sp_get_stations_for_modality", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@addText", addText);
        command.Parameters.AddWithValue("@modalityId", modalityId);
        SqlDataReader reader = command.ExecuteReader();
        ddl.DataSource = reader;
        ddl.DataMember = "StationName";
        ddl.DataTextField = "StationName";
        ddl.DataValueField = "StationId";
        ddl.DataBind();
        reader.Close();
        connection.Close();
    }

    public static void BindUserDDLForGroup(int roleId, int groupId,string addText, DropDownList ddl)
    {
        RISDatabaseAccessLayer dataAccess = new RISDatabaseAccessLayer();
        SqlConnection connection = (SqlConnection)dataAccess.GetConnection();
        connection.Open();
        SqlCommand command = new SqlCommand("sp_get_users_for_role_in_group", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@roleId", roleId);
        command.Parameters.AddWithValue("@groupId", groupId);
        command.Parameters.AddWithValue("@addText", addText);
        SqlDataReader reader = command.ExecuteReader();
        ddl.DataSource = reader;
        ddl.DataMember = "Name";
        ddl.DataTextField = "Name";
        ddl.DataValueField = "UserId";
        ddl.DataBind();
        reader.Close();
        connection.Close();
    }
}
