using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using RIS.RISLibrary.Objects.DICOM;
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;
using RIS.RISLibrary.Utilities;

namespace RIS.RISService.DataMigrators
{
    class ImagesDataMigrator : GenericDataMigrator
    {
        public ImagesDataMigrator()
        {
        }
        protected override DICOMObject GetDICOMObject()
        {
            return new DICOMImageObject();
        }
        protected override RISObject GetRISObject()
        {
            return new ImageObject();
        }
        protected override string GetRISWhereClause()
        {
            return "";
        }
        protected override void UpdateDICOMObject(DICOMObject dicomObject)
        {
            DICOMImageObject image = (DICOMImageObject)dicomObject;
            image.SyncTime.Value = DateTime.Now;
            image.Update(Constants.Database.SystemUserId);
        }

        protected override RISObject GetRISObject(DICOMObject dicomObject)
        {
            ImageObject risImage = new ImageObject();
           
            DICOMImageObject dicomImage = (DICOMImageObject)dicomObject;
            Console.WriteLine("Syncing Image:" + dicomImage.ImageInstance.Value);
            risImage.ImageInstance.Value = dicomImage.ImageInstance.Value;

            risImage.ImageClassUI.Value = dicomImage.ImageClassUI.Value;
            risImage.ImageNumber.Value = dicomImage.ImageNumber.Value;
            risImage.ImageDate.Value = DatabaseUtility.GetDateTime(dicomImage.ImageDate.Value, dicomImage.ImageTime.Value);
            risImage.EchoNumber.Value = dicomImage.EchoNumber.Value;
            risImage.NumberOfFrames.Value = dicomImage.NumberOfFrames.Value;
            risImage.AcquiredDate.Value = DatabaseUtility.GetDateTime(dicomImage.AcqDate.Value, dicomImage.AcqTime.Value);
            risImage.SliceLocation.Value = dicomImage.SliceLocation.Value;
            risImage.NumberOfSamples.Value = dicomImage.NumberOfSamples.Value;
            risImage.PhotoMetric.Value = dicomImage.PhotoMetric.Value;
            risImage.Rows.Value = dicomImage.Rows.Value;
            risImage.Columns.Value = dicomImage.Columns.Value;
            risImage.BitsStored.Value = dicomImage.BitsStored.Value;
            risImage.Path.Value = dicomImage.ObjectFile.Value;
            risImage.DeviceName.Value = dicomImage.DeviceName.Value;

            SeriesObject risSeries = new SeriesObject();
            risSeries.SeriesInstance.Value = dicomImage.SeriesInst.Value;
            risSeries.Load();
            if (risSeries.IsLoaded)
            {
                risImage.SeriesId.Value = risSeries.GetPrimaryKey().Value;
            }
            else
            {
                risImage = null;
            }
            return risImage;            
        }
        protected override bool AreEqual(DICOMObject dicomObject, RISObject risObject)
        {
            DICOMImageObject dicomImage = (DICOMImageObject)dicomObject;
            ImageObject risImage = (ImageObject)risObject;
            return dicomImage.ImageInstance.Value.Equals(risImage.ImageInstance.Value);                
        }
        protected override void PerformPostSaveTasks(RISObject risObject)
        {
            
        }
    }
}

