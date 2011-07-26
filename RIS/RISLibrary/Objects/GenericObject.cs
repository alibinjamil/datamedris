using System;
using System.Collections.Generic;
using System.Text;
using RIS.RISLibrary.Fields;
using RIS.RISLibrary.Database;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;

using RIS.RISLibrary.Utilities;
namespace RIS.RISLibrary.Objects
{
    public abstract class GenericObject
    {
        public abstract Field[] GetFields();
        public abstract PrimaryKeyField GetPrimaryKey();
        public abstract String GetTableName();
        public abstract Field[] GetAllFields();
        public abstract bool HasAccessColumns();
        public abstract DatabaseAccessLayer GetDatabaseAccessLayer();

        /*private ArrayList innerJoinObjects = new ArrayList();

        public void AddInnerJoin(GenericObject genericObject)
        {
            this.InnerJoins.Add(genericObject);
        }*/
        public bool IsLoaded
        {
            get
            {
                if (this.GetPrimaryKey().Value == null)
                    return false;
                else
                    return true;
            }
        }

        public void Insert(int tranUserId)
        {
            DatabaseAccessLayer dataAccessLayer = GetDatabaseAccessLayer();
            QueryBuilder query = new QueryBuilder();
            query.AddText("INSERT INTO ");
            query.AddTableName(this.GetTableName());
            query.AddText("(");
            Field[] fields = null;
            if (this.GetPrimaryKey().IsAutoIncreement == false)
                fields = GetAllFields();
            else
                fields = GetFields();
            bool addComma = false;
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].Value != null)
                {
                    if (addComma == true)
                        query.AddText(",");
                    query.AddColumn(fields[i].ColumnName);
                    addComma = true;
                }
            }
            if (this.HasAccessColumns() == true)
            {
                query.AddText(",");
                query.AddAccessColumns();
            }
            query.AddText(") VALUES (");
            addComma = false;
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].Value != null)
                {
                    if (addComma == true)
                        query.AddText(",");
                    query.AddParam(fields[i]);
                    addComma = true;
                }
            }
            if (this.HasAccessColumns())
            {
                query.AddText(",");
                query.AddAccessParams(tranUserId);
            }
            query.AddText(")");
            if (this.GetPrimaryKey().IsAutoIncreement == false)
                dataAccessLayer.ExecuteUpdate(query);
            else
                this.GetPrimaryKey().Value = dataAccessLayer.ExecuteInsert(query);
        }
        public void Update(int tranUserId)
        {
            DatabaseAccessLayer dataAccessLayer = GetDatabaseAccessLayer();
            QueryBuilder query = new QueryBuilder();
            query.AddText("UPDATE ");
            query.AddTableName(this.GetTableName());
            query.AddText("SET ");
            Field[] fields = GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                if (i > 0) query.AddText(",");
                query.AddColumn(fields[i].ColumnName);
                query.AddText("=");
                query.AddParam(fields[i]);                
            }
            if (this.HasAccessColumns() == true)
            {
                query.AddText(",");
                query.AddLastUpdate(tranUserId);
            }
            query.AddEqualsFilter(this.GetPrimaryKey());
            int rows = dataAccessLayer.ExecuteUpdate(query);
        }

        public void Save(int tranUserId)
        {
            if (this.GetPrimaryKey().Value == null)
            {
                Insert(tranUserId);
            }
            else
            {
                Update(tranUserId);
            }
            
            
        }

        public void Remove(int tranUserId)
        {
            DatabaseAccessLayer dataAccessLayer = GetDatabaseAccessLayer();
            QueryBuilder query = new QueryBuilder();
            query.AddText("DELETE FROM");
            query.AddTableName(this.GetTableName());
            query.AddEqualsFilter(this.GetPrimaryKey());
            dataAccessLayer.ExecuteDelete(query);            
        }

        public QueryBuilder GetSelectQuery()
        {
            Console.WriteLine("GetSelectQuery()");
            QueryBuilder query = new QueryBuilder();
            query.AddText("SELECT ");
            Field[] fields = this.GetAllFields();
            for (int i = 0; i < fields.Length; i++)
            {
                if (i > 0)
                    query.AddText(",");
                query.AddColumn(fields[i].ColumnName);
            }
            query.AddText(" FROM ");
            query.AddTableName(this.GetTableName());
            return query;
        }

        public void Load(int tranUserId)
        {
            QueryBuilder query = GetSelectQuery();
            if (this.GetPrimaryKey().Value != null)
            {
                query.AddEqualsFilter(this.GetPrimaryKey());
            }
            else
            {
                Field []fields = GetFields();
                foreach(Field field in fields)
                {
                    if(field.Value != null)
                    {
                        query.AddEqualsFilter(field);
                    }
                }
                
            }
            DatabaseAccessLayer dataAccessLayer = GetDatabaseAccessLayer();
            DbDataReader reader = dataAccessLayer.ExecuteQuery(query);
            if (reader.Read())
            {
                Load(reader);
                if (tranUserId >= 0)
                {
                    //AddLogTransaction(tranUserId);
                }
            }
            else
            {
                this.GetPrimaryKey().Value = null;
            }
            reader.Close();
            dataAccessLayer.CloseConnection();
        }

        public void Load()
        {
            Load(Constants.Database.NullUserId);
        }
        public void Save()
        {
            Save(Constants.Database.NullUserId);
        }

        public void Load(DbDataReader reader)
        {
            Field[] fields = GetAllFields();
            for(int i=0;i<fields.Length;i++)
            {
                if (reader.IsDBNull(i))
                {
                    fields[i].Value = null;
                }
                /*else if(fields[i] is TextField)
                {                    
                    fields[i].Value = reader.GetString(i);
                    Console.WriteLine(fields[i].Value);
                }
                else if(fields[i] is IntField)
                {
                    fields[i].Value = reader.GetInt32(i);
                }
                else if(fields[i] is DateTimeField)
                {
                    fields[i].Value = reader.GetDateTime(i);
                }*/
                else
                {
                    fields[i].Value = reader.GetValue(i);
                }
            }

        }

        /*public void AddLogTransaction(int tranUserId)
        {

        }*/
    }
}
