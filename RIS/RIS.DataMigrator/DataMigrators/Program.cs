using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace RIS.RISService.DataMigrators
{
    class Program
    {        
        static void Main(string[] args)
        {
            RISDataMigrator migrator = new RISDataMigrator();
            migrator.MigrateData();
        }
    }
}
