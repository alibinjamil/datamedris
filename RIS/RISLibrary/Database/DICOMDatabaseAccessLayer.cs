using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;

namespace RIS.RISLibrary.Database
{
    public class DICOMDatabaseAccessLayer : DatabaseAccessLayer
    {
        protected override DbCommand GetCommand()
        {
            return new SqlCommand();
        }

        public override DbConnection GetConnection()
        {
            //return new SqlConnection(@"Data Source=.\SQL2005;database=conquest;Persist Security Info=True;User ID=sa;Password=123;");
            return new SqlConnection(@"Data Source=.;database=conquest;Persist Security Info=True;User ID=sa;Password=datamed;");
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
