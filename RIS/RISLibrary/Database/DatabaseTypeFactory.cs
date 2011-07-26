using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using RIS.RISLibrary.Database;
namespace RIS.RISLibrary.Database
{
    public class DatabaseTypeFactory
    {
        public enum FieldTypes
        {
            BLOB = 1,
            DATETIME = 2,
            INT = 3,
            TEXT = 4
        }

        public static DatabaseType GetType(FieldTypes type)
        {
            DatabaseType databastType = null;
            switch (type)
            {
                case FieldTypes.BLOB:
                    databastType = new DatabaseType(SqlDbType.VarBinary, DbType.Binary);
                    break;
                case FieldTypes.DATETIME:
                    databastType = new DatabaseType(SqlDbType.DateTime, DbType.DateTime);
                    break;
                case FieldTypes.INT:
                    databastType = new DatabaseType(SqlDbType.Int, DbType.Int32);
                    break;
                case FieldTypes.TEXT:
                    databastType = new DatabaseType(SqlDbType.VarChar, DbType.String);
                    break;
            }
            return databastType;
        }
    }
}
