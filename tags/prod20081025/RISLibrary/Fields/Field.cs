using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using RIS.RISLibrary.Database;

namespace RIS.RISLibrary.Fields
{
    public abstract class Field
    {
        /*private Field()
        {
        }
        private Field(String columnName, Object value)
        {
            this.columnName = columnName;
            this.value = value;
        }*/
        private String columnName = null;
        public String ColumnName
        {
            get
            {
                return this.columnName;
            }
            set
            {
                this.columnName = value;
            }
        }

        private Object value = null;
        public Object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        private DatabaseType type; 
        public DatabaseType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
    }
}
