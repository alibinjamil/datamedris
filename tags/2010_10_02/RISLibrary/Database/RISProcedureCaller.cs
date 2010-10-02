using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace RIS.RISLibrary.Database
{
    public static class RISProcedureCaller
    {
        public static int GetRoleCount(int userId)
        {
            int numOfRoles = 0;
            RISDatabaseAccessLayer databaseAccess = new RISDatabaseAccessLayer();
            SqlConnection connection = (SqlConnection)databaseAccess.GetConnection();
            connection.Open();
            SqlCommand command = new SqlCommand("sp_get_num_user_roles", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId", userId);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                numOfRoles = reader.GetInt32(0);
            }
            connection.Close();
            return numOfRoles;
        }
        public static void InsertStudyGroup(object studyId, object referringPhysicianId, object userId)
        {
            RISDatabaseAccessLayer databaseAccess = new RISDatabaseAccessLayer();
            SqlConnection connection = (SqlConnection)databaseAccess.GetConnection();
            connection.Open();
            SqlCommand command = new SqlCommand("sp_insert_study_group", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@studyId", studyId);
            command.Parameters.AddWithValue("@userId", referringPhysicianId);
            command.Parameters.AddWithValue("@adminUserId", userId);
            command.ExecuteNonQuery();
        }
        public static DataTable GetAllUsers()
        {

            RISDatabaseAccessLayer databaseAccess = new RISDatabaseAccessLayer();
            SqlConnection connection = (SqlConnection)databaseAccess.GetConnection();
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT [UserId], [LoginName], [Password], [Name], [IsActive] FROM [tUsers]", connection);
            command.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dtResults = new DataTable();
            da.Fill(dtResults);
            connection.Close();
            return dtResults;
        }
        public static DataTable GetPatientInfo(string whereClause)
        {
            return new DataTable();
        }
        public static DataTable GetUserGroupsWithDefaults(int userid)
        {
            RISDatabaseAccessLayer databaseAccess = new RISDatabaseAccessLayer();
            SqlConnection connection = (SqlConnection)databaseAccess.GetConnection();
            connection.Open();
            SqlCommand command = new SqlCommand("sp_get_groups_for_user_defaults", connection);
            command.Parameters.AddWithValue("@userid", userid);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dtResults = new DataTable();
            da.Fill(dtResults);
            connection.Close();
            return dtResults;

        }
    }
}
