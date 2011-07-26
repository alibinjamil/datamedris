using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using RIS.RISLibrary.Database;
namespace RIS.RISLibrary.Fields
{
    public class PrimaryKeyField : Field
    {
        private static DatabaseType type = new DatabaseType(SqlDbType.Int, DbType.String);
        public PrimaryKeyField()
        {
            this.ColumnName = null;
            this.Value = null;
            this.IsAutoIncreement = true;
            this.Type = null;
        }
        public PrimaryKeyField(String columnName,Object value,bool isAuto)
        {
            this.ColumnName = columnName;
            this.Value = value;
            this.IsAutoIncreement = isAuto;
            this.Type = DatabaseTypeFactory.GetType(DatabaseTypeFactory.FieldTypes.INT);
        }
        bool m_isAuto;
        public bool IsAutoIncreement
        {
            get
            {
                return m_isAuto;
            }
            set
            {
                m_isAuto = value;
            }
        }
    }
}
