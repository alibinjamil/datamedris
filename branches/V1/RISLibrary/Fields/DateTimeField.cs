using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using RIS.RISLibrary.Database;
namespace RIS.RISLibrary.Fields
{
    public class DateTimeField : Field
    {
        public DateTimeField()
        {
            this.ColumnName = null;
            this.Value = null;
            this.Type = null;
        }

        public DateTimeField(String columnName, Object value)
        {
            //base(columnName, value);
            this.ColumnName = columnName;
            this.Value = value;
            this.Type = DatabaseTypeFactory.GetType(DatabaseTypeFactory.FieldTypes.DATETIME);
        }
    }
}
