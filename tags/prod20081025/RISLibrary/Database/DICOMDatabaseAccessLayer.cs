using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;

namespace RIS.RISLibrary.Database
{
    public class DICOMDatabaseAccessLayer : DatabaseAccessLayer
    {
        protected override DbCommand GetCommand()
        {
            return new OleDbCommand();
        }

        public override DbConnection GetConnection()
        {
            //return new OleDbConnection("Provider=MS Remote; Remote Server=http://10.10.10.134; Remote Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\\Inetpub\\wwwroot\\conquestpacs_s.mdb");
            //return new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=D:\\RIS\\RISWebSite\\DICOM\\DicomServer\\data\\dbase\\conquestpacs_s.mdb;");
            return new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=E:\\RIS\\trunk\\RISWebSite\\conquestpacs_s.mdb;");
            //return new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=F:\\MyProjects\\RIS\\RISWebSite\\DICOM\\conquestpacs_s.mdb;");
            //return new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=D:\\Projects\\RIS\\RISWebSite\\DICOM\\conquestpacs_s.mdb;");
        }
        protected override DbParameter GetParameter(DatabaseParameter parameter)
        {
            return new OleDbParameter(parameter.ParameterName, parameter.ParameterValue);
        }
    }
}
