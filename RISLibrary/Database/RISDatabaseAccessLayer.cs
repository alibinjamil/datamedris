using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace RIS.RISLibrary.Database
{
    public class RISDatabaseAccessLayer : DatabaseAccessLayer
    {
        public override DbConnection GetConnection()
        {
            return new SqlConnection("Data Source=.\\SQLEXPRESS;database=RIS;Trusted_Connection=yes");
            //return new SqlConnection("Data Source=cdocs-pak-d620\\SQLEXPRESS;User ID=sa;Password=123;database=RIS");
            //return new SqlConnection("Data Source=lahore\\SQLEXPRESS;User ID=sa;Password=123;database=RIS");
            //return new SqlConnection("Data Source=.\\SQLEXPRESS;database=RIS;Persist Security Info=True;User ID=sa;Password=new_password;Connect Timeout=30;User Instance=False");
        }

        protected override DbCommand GetCommand()
        {
            return new SqlCommand();
        }

        protected override DbParameter GetParameter(DatabaseParameter parameter)
        {
     
            SqlParameter sqlParameter = null;
            if (parameter.ParameterValue == null)
            {
                if (parameter.ParameterType.SqlType.Equals(SqlDbType.VarBinary))
                {
                    sqlParameter = new SqlParameter(parameter.ParameterName, SqlDbType.VarBinary, -1);
                    sqlParameter.Value = DBNull.Value;
                }
                else
                {
                    sqlParameter = new SqlParameter(parameter.ParameterName, DBNull.Value);
                }
            }
            else
            {
                sqlParameter = new SqlParameter(parameter.ParameterName, parameter.ParameterValue);
            }
            return sqlParameter;
        }
    }
}
