using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace RIS.RISLibrary.Database
{
    public class DatabaseType
    {
        private SqlDbType sqlType;
        private DbType dbType;
        private DatabaseType() { }
        public DatabaseType(SqlDbType sqlType, DbType dbType)
        {
            this.sqlType = sqlType;
            this.dbType = dbType;
        }
        public SqlDbType SqlType
        {
            get
            {
                return sqlType;
            }
        }
        public DbType Type
        {
            get
            {
                return dbType;
            }
        }
    }
}
