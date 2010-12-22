using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace RIS.RISLibrary.Database
{
    public class DatabaseParameter
    {
        String paramName;
        Object paramValue;
        DatabaseType paramType;

        public DatabaseParameter(String paramName, Object paramValue, DatabaseType type)
        {
            this.paramName = paramName;
            this.paramValue = paramValue;
            this.paramType = type;
        }

        public String ParameterName
        {
            get
            {
                return this.paramName;
            }
        }

        public Object ParameterValue
        {
            get
            {
                return this.paramValue;
            }
        }
        public DatabaseType ParameterType
        {
            get
            {
                return paramType;
            }
        }
    }
}
