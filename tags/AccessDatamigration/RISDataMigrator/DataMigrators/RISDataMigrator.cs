using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections;

using RIS.RISLibrary.Database;
using RIS.RISLibrary.Objects.DICOM;
using RIS.RISLibrary.Objects.RIS;


namespace RIS.RISService.DataMigrators
{
    public class RISDataMigrator
    {
        public RISDataMigrator()
        {
        }
        
        public void MigrateData()
        {
            GenericDataMigrator[] dataMigrators = new GenericDataMigrator[4];
            dataMigrators[0] = new PatientsDataMigrator();
            dataMigrators[1] = new StudiesDataMigrator();
            dataMigrators[2] = new SeriesDataMigrator();
            dataMigrators[3] = new ImagesDataMigrator();
            foreach (GenericDataMigrator dataMigrator in dataMigrators)
            {
                dataMigrator.Migrate();
            }            
        }
    }
}
