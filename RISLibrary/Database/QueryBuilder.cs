using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.Common;
using System.Data;

using RIS.RISLibrary.Fields;

namespace RIS.RISLibrary.Database
{    
    public class QueryBuilder
    {        
        StringBuilder query = null;
        ArrayList columns = null;
        ArrayList parameters = null;
        ArrayList filters = null;

        public QueryBuilder()
        {
            query = new StringBuilder();
            columns = new ArrayList();
            parameters = new ArrayList();
            filters = new ArrayList();
        }
        
        public void AddText(String text)
        {
            query.Append(text);
        }

        public void AddColumn(String columnName)
        {
            query.Append("[").Append(columnName).Append("]");
            columns.Add(columnName);
        }
        
        public void AddTableName(String tableName)
        {
            query.Append(" ").Append(tableName).Append(" ");
        }

        /*public void AddParams()
        {
            for (int i = 0; i < columns.Count; i++)
            {
                if (i > 0)
                    query.Append(",");
                AddParam();
            }
        }*/
            
        public void AddParam(Field field)
        {
            AddParam(field.ColumnName, field.Value,field.Type);
        }

        public void AddAccessParams(int id)
        {
            String dateTime = DateTime.Now.ToString();
            AddParam("CreatedBy",id,DatabaseTypeFactory.GetType(DatabaseTypeFactory.FieldTypes.INT));
            this.AddText(",");
            AddParam("CreationDate", dateTime, DatabaseTypeFactory.GetType(DatabaseTypeFactory.FieldTypes.DATETIME));
            this.AddText(",");
            AddParam("LastUpdatedBy", id, DatabaseTypeFactory.GetType(DatabaseTypeFactory.FieldTypes.INT));
            this.AddText(",");
            AddParam("LastUpdateDate", dateTime, DatabaseTypeFactory.GetType(DatabaseTypeFactory.FieldTypes.DATETIME));
        }

        public void AddParam(String paramName, Object paramValue,DatabaseType type)
        {
            String name = MakeParam(paramName);
            query.Append(name);
            parameters.Add(new DatabaseParameter(name, paramValue,type));
        }

        public void AddAccessColumns()
        {
            this.AddColumn("CreatedBy");
            this.AddText(",");
            this.AddColumn("CreationDate");
            this.AddText(",");
            this.AddColumn("LastUpdatedBy");
            this.AddText(",");
            this.AddColumn("LastUpdateDate");
        }

        public void AddLastUpdate(int id)
        {
            this.AddColumn("LastUpdatedBy");
            this.AddText("=");
            this.AddParam("LastUpdatedBy", id, DatabaseTypeFactory.GetType(DatabaseTypeFactory.FieldTypes.INT));
            this.AddText(",");
            this.AddColumn("LastUpdateDate");
            this.AddText("=");
            this.AddParam("LastUpdateDate", DateTime.Now.ToString(), DatabaseTypeFactory.GetType(DatabaseTypeFactory.FieldTypes.DATETIME));
        }

        public override String ToString()
        {
            return query.ToString();
        }

        public ArrayList Parameters
        {
            get
            {
                return parameters;
            }
        }

        private String MakeParam(String param)
        {
            return (new StringBuilder("@").Append(param)).ToString();
        }

        public void AddEqualsFilter(Field field)
        {
            if (filters.Count == 0)
            {
                AddText(" WHERE ");
            }
            else
            {
                AddText(" AND ");
            }
            AddColumn(field.ColumnName);
            AddText(" = ");
            AddParam(field);
            filters.Add(field);
        }
    }
}
