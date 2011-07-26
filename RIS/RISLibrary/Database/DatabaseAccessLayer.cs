using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;

using RIS.RISLibrary.Objects;
using RIS.RISLibrary.Fields;

namespace RIS.RISLibrary.Database
{
    public abstract class DatabaseAccessLayer
    {
        private DbConnection currentConnection;
        private DbCommand currentCommand;

        public abstract DbConnection GetConnection();

        protected abstract DbCommand GetCommand();

        protected abstract DbParameter GetParameter(DatabaseParameter parameter);

        public int ExecuteUpdate(QueryBuilder query)
        {
            SetCommand(query);
            int rows = currentCommand.ExecuteNonQuery();
            currentConnection.Close();
            return rows;
        }

        public int ExecuteDelete(QueryBuilder query)
        {
            SetCommand(query);
            int rows = currentCommand.ExecuteNonQuery();
            currentConnection.Close();
            return rows;
        }

        public object ExecuteInsert(QueryBuilder query)
        {
            object id = null;
            query.AddText(";SELECT @@IDENTITY;");
            SetCommand(query);
            id = currentCommand.ExecuteScalar();
            currentConnection.Close();
            return id;
        }

        public DbDataReader ExecuteQuery(QueryBuilder query)
        {
            SetCommand(query);
            return currentCommand.ExecuteReader();
        }

        public void CloseConnection()
        {
            currentConnection.Close();
        }

        private void SetCommand(QueryBuilder query)
        {
            currentConnection = GetConnection();
            currentConnection.Open();
            currentCommand = GetCommand();
            currentCommand.Connection = currentConnection;
            currentCommand.CommandText = query.ToString();
            foreach (DatabaseParameter parmeter in query.Parameters)
            {
                currentCommand.Parameters.Add(GetParameter(parmeter));
            }
        }

        /*public void FillObject(QueryBuilder query,RISObject currentObject)
        {
            SetCommand(query);
            DbDataReader reader = currentCommand.ExecuteReader();
            Field [] fields = currentObject.GetFields();
            if (reader.Read())
            {
                reader.re
                for(int i=0;i<fields.Length;i++)
                {
                    if(fields[i] is TextField)
                    {
                        fields[i].Value = reader.GetString(i);
                    }
                    else if(fields[i] is IntField)
                    {
                        fields[i].Value = reader.GetInt32(i);
                    }
                    else if(fields[i] is DateTimeField)
                    {
                        fields[i].Value = reader.GetDateTime(i);
                    }
                }
            }
            reader.Close();
            currentConnection.Close();
        }*/
    }    
}
