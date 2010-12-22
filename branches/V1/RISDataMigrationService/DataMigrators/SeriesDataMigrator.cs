using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using RIS.RISLibrary.Objects.DICOM;
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;

namespace RIS.RISService.DataMigrators
{
    class SeriesDataMigrator : GenericDataMigrator
    {
        StudyObject risStudy = null;
        public SeriesDataMigrator()
        {
        }
        protected override DICOMObject GetDICOMObject()
        {
            return new DICOMSeriesObject();
        }
        protected override string GetDICOMWhereClause()
        {
            return "";
        }
        protected override RISObject GetRISObject()
        {
            return new SeriesObject();
        }
        protected override string GetRISWhereClause()
        {
            return "";
        }
        protected override RISObject GetRISObject(DICOMObject dicomObject)
        {
            SeriesObject risSeries = new SeriesObject();
            DICOMSeriesObject dicomSeries = (DICOMSeriesObject)dicomObject;
            risSeries.SeriesInstance.Value = dicomSeries.SeriesInstance.Value;
            risSeries.SeriesNumber.Value = dicomSeries.SeriesNumber.Value;
            risSeries.SeriesDate.Value = DatabaseUtility.GetDateTime(dicomSeries.SeriesDate.Value, dicomSeries.SeriesTime.Value);
            risSeries.Description.Value = dicomSeries.Description.Value;
            risSeries.PatientPosition.Value = dicomSeries.PatientPosition.Value;
            risSeries.Contrast.Value = dicomSeries.ContrastBo.Value;
            risSeries.ProtocolName.Value = dicomSeries.ProtocolNa.Value;
            risSeries.FrameOfReference.Value = dicomSeries.FrameOfRef.Value;
            risSeries.BodyPartExamined.Value = dicomSeries.BodyPartEx.Value;

            ModalityObject risModality = new ModalityObject();
            risModality.Name.Value = dicomSeries.Modality.Value;
            risModality.Load();
            if (!risModality.IsLoaded)
            {
                risModality.Save();
            }
            ModalityDetailObject modalityDetail = new ModalityDetailObject();
            modalityDetail.ModalityId.Value = risModality.GetPrimaryKey().Value;
            modalityDetail.Manufacturer.Value = dicomSeries.Manufactur.Value;
            modalityDetail.ModelName.Value = dicomSeries.ModelName.Value;
            modalityDetail.Load();
            if (!modalityDetail.IsLoaded)
            {
                modalityDetail.Save();
            }


            risSeries.ModalityDetailId.Value = modalityDetail.GetPrimaryKey().Value;
            
            risStudy = new StudyObject();
            risStudy.StudyInstance.Value = dicomSeries.StudyInsta.Value;
            risStudy.Load();
            if (risStudy.IsLoaded)
            {
                risSeries.StudyId.Value = risStudy.StudyId.Value;
                if (dicomSeries.StationName.Value != null)
                {
                    StationObject station = new StationObject();
                    station.ModalityId.Value = risModality.GetPrimaryKey().Value;
                    station.StationName.Value = dicomSeries.StationName.Value;
                    station.Instituition.Value = dicomSeries.Instituition.Value;
                    station.Load();
                    if (!station.IsLoaded)
                    {
                        station.Save();
                    }
                    risStudy.StationId.Value = station.GetPrimaryKey().Value;
                    risStudy.Save();
                }
            }
            else
            {
                risSeries = null;                
            }
            return risSeries;            
        }
        protected override bool AreEqual(DICOMObject dicomObject, RISObject risObject)
        {
            DICOMSeriesObject dicomSeries = (DICOMSeriesObject)dicomObject;
            SeriesObject risSeries = (SeriesObject)risObject;
            return dicomSeries.SeriesInstance.Value.Equals(risSeries.SeriesInstance.Value);
        }

        protected override void PerformPostSaveTasks(RISObject risObject)
        {
            SeriesObject risSeries = (SeriesObject)risObject;
            risStudy.LatestSeriesId.Value = risSeries.GetPrimaryKey().Value;
            risStudy.Save();
        }
    }
}

