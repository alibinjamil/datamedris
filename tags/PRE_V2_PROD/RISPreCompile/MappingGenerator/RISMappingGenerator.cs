using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.IO;

namespace RIS.RISPreCompile.MappingGenerator
{
    public class RISMappingGenerator
    {
        private Hashtable mappingTable = new Hashtable();
        private Hashtable tablesWithNoAccessColumns = new Hashtable();
        public RISMappingGenerator()
        {
            mappingTable.Add("tSeries", "Series");
            mappingTable.Add("tLog", "Log");
            tablesWithNoAccessColumns.Add("tLog", null);
            tablesWithNoAccessColumns.Add("tUserHospitals", null);
            tablesWithNoAccessColumns.Add("tUserClients", null);
            tablesWithNoAccessColumns.Add("tTemplates", null);
            tablesWithNoAccessColumns.Add("tTemplateUsers", null);
            tablesWithNoAccessColumns.Add("tCarriers", null);
            tablesWithNoAccessColumns.Add("tAttachments", null);
        }
        
        
        public void Generate()
        {
            string filePath = @"E:\MyProjects\RIS\trunk\RISPreCompile\Resources\RISDatabaseObjectMapping.xml";
            if (File.Exists(filePath))
                File.Delete(filePath);
            StreamWriter file = new StreamWriter(filePath);
            file.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            file.WriteLine("<entities>");
            ArrayList tables = new ArrayList();
            SqlConnection connection = new SqlConnection(@"Data Source=.\SQL2005;Initial Catalog=RIS;User Id=sa;Password=123;");
            connection.Open();
            SqlCommand command = new SqlCommand("select TABLE_NAME from INFORMATION_SCHEMA.TABLES", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tables.Add(reader.GetString(0));
            }
            reader.Close();

            foreach (string tableName in tables)
            {
                string subTableName = tableName.Substring(1, tableName.Length - 2);
                string tableObjectName = (string)mappingTable[tableName];
                if (tableObjectName == null)
                {
                    if (tableName.EndsWith("ies"))
                        tableObjectName = tableName.Substring(1, tableName.Length - 4) + "y";
                    else
                        tableObjectName = subTableName;
                }
                if(tablesWithNoAccessColumns.ContainsKey(tableName))
                    file.WriteLine("<entity tableName=\"" + tableName + "\" objectName=\"" + tableObjectName + "Object\" HasAccessColumns=\"False\"  >");
                else
                    file.WriteLine("<entity tableName=\"" + tableName + "\" objectName=\"" + tableObjectName + "Object\" >");
                command = new SqlCommand("select COLUMN_NAME,DATA_TYPE from information_schema.columns WHERE TABLE_NAME = @tableName", connection);
                command.Parameters.Add(new SqlParameter("@tableName",tableName));
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string columnName = reader.GetString(0);
                    if (IgnoreColumn(columnName) == false)
                    {
                        string type = reader.GetString(1);
                        string fieldType = "";
                        if (columnName.ToUpper().Equals(tableObjectName.ToUpper() + "ID"))
                        {
                            fieldType = "PrimaryKeyField";
                        }
                        else if (type.Equals("datetime"))
                        {
                            fieldType = "DateTimeField";
                        }
                        else if (type.Equals("int"))
                        {
                            fieldType = "IntField";
                        }
                        else if (type.Equals("varbinary"))
                        {
                            fieldType = "BlobField";
                        }
                        else
                        {
                            fieldType = "TextField";
                        }
                        file.WriteLine("<field type=\"" + fieldType + "\" columnName=\"" + columnName + "\" />");
                    }
                }
                file.WriteLine("</entity>");
                reader.Close();
            }
            file.WriteLine("</entities>");
            file.Close();
            connection.Close();
        }

        private bool IgnoreColumn(string columnName)
        {
            string[] columns = {"CREATEDBY","CREATIONDATE","LASTUPDATEDBY","LASTUPDATEDATE"};
            foreach (string column in columns)
            {
                if (column.Equals(columnName.ToUpper()))
                    return true;
            }
            return false;
        }
    }
}
