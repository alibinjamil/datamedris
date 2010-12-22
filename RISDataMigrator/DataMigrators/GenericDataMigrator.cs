using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.OleDb;
using System.Data.SqlClient;

using RIS.RISLibrary.Objects.DICOM;
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;
using RIS.RISLibrary.Utilities;

namespace RIS.RISService.DataMigrators
{
    abstract class GenericDataMigrator
    {
        abstract protected DICOMObject GetDICOMObject();
        abstract protected RISObject GetRISObject();
        abstract protected string GetDICOMWhereClause();
        abstract protected string GetRISWhereClause();
        abstract protected bool AreEqual(DICOMObject dicomObject,RISObject risObject);
        abstract protected RISObject GetRISObject(DICOMObject dicomObject);
        abstract protected void PerformPostSaveTasks(RISObject risObject);

        public static int AdminUserId = 0;

        public void Migrate()
        {
            ArrayList dicomObjects = GetDICOMList();
            ArrayList risObjects = GetRISList();
            foreach (DICOMObject dicomObject in dicomObjects)
            {
                try
                {
                    bool IsPresent = false;
                    foreach (RISObject risObject in risObjects)
                    {
                        if (AreEqual(dicomObject, risObject))
                        {
                            IsPresent = true;
                            break;
                        }
                    }
                    if (IsPresent == false)
                    {
                        RISObject risObject = GetRISObject(dicomObject);
                        if (risObject != null)
                        {
                            risObject.Save(Constants.Database.SystemUserId);
                            PerformPostSaveTasks(risObject);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Instance.Log(ex.StackTrace);
                    //log excetpion
                }
            }
        }

        private ArrayList GetDICOMList()
        {
            DICOMObject dicomObject = GetDICOMObject();
            QueryBuilder query = dicomObject.GetSelectQuery();
            query.AddText(GetDICOMWhereClause());
            DICOMDatabaseAccessLayer dicomDataAccessLayer = new DICOMDatabaseAccessLayer();
            OleDbDataReader reader = (OleDbDataReader)dicomDataAccessLayer.ExecuteQuery(query);
            ArrayList dicomObjects = new ArrayList();
            while (reader.Read())
            {
                dicomObject.Load(reader);
                dicomObjects.Add(dicomObject);
                dicomObject = GetDICOMObject();
            }
            reader.Close();
            dicomDataAccessLayer.CloseConnection();
            return dicomObjects;
        }

        private ArrayList GetRISList()
        {
            RISObject risObject = GetRISObject();
            QueryBuilder query = risObject.GetSelectQuery();
            query.AddText(GetRISWhereClause());
            RISDatabaseAccessLayer dataAccessLayer = new RISDatabaseAccessLayer();
            SqlDataReader reader = (SqlDataReader)dataAccessLayer.ExecuteQuery(query);
            ArrayList risObjects = new ArrayList();
            while (reader.Read())
            {
                risObject.Load(reader);
                risObjects.Add(risObject);
                risObject = GetRISObject();
            }
            reader.Close();
            dataAccessLayer.CloseConnection();
            return risObjects;
        }

    }
}
