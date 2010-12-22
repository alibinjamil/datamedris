using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using RIS.RISLibrary.Database;
namespace RIS.RISLibrary.Fields
{
    public class IntField : Field
    {
        public IntField()
        {
            this.ColumnName = null;
            this.Value = null;
            this.Type = null;
        }
        public IntField(String columnName, Object value)
        {
            this.ColumnName = columnName;
            this.Value = value;
            this.Type = DatabaseTypeFactory.GetType(DatabaseTypeFactory.FieldTypes.INT);
        }

    }
}
