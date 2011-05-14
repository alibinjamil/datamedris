using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Xml.Linq;

using RIS.Common;
using RIS.RISLibrary.Utilities;


namespace RIS.RISService.DataMigrators
{
    public class RISDataMigrator
    {
        private RISEntities risDB;
        private ConquestEntities conquestDB;
        public static int AdminUserId = 0;
        public RISDataMigrator()
        {
            risDB = new RISEntities();
            conquestDB = new ConquestEntities();
        }
        
        /*
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
         * */

        public void MigrateData()
        {
            MigrateStudies();
            MigrateSeries();
            MigrateImages();
        }

        private void MigrateStudies()
        {
            List<DICOMStudy> dicomStudyList = (from s in conquestDB.DICOMStudies
                                               where s.SyncTime == null
                                               select s).ToList();
            foreach (DICOMStudy dicomStudy in dicomStudyList)
            {
                try
                {
                    Study study = new Study();
                    study.ExternalPatientId = dicomStudy.PatientID;
                    study.OriginalPatientId = dicomStudy.PatientID;
                    study.PatientName = ParseName(dicomStudy.PatientNam);
                    study.PatientDOB = ParseDateTime(dicomStudy.PatientBir, null);
                    study.PatientGender = dicomStudy.PatientSex;
                    study.PatientWeight = dicomStudy.PatientsWe;
                    study.IsLatest = true;
                    //study.IsManual = "N";

                    study.StudyStatusId = Constants.StudyStatusTypes.PreRelease;
                    study.StudyInstance = dicomStudy.StudyInsta;
                    study.AccessionNumber = dicomStudy.AccessionN;
                    study.StudyDate = ParseDateTime(dicomStudy.StudyDate, dicomStudy.StudyTime);
                    study = SetHospitalAndRefPhy(dicomStudy, study);

                    study = SetModality(dicomStudy, study);

                    if (dicomStudy.StudyDescr != null)
                    {
                        Procedure procedure = (from p in risDB.Procedures
                                               where p.Name == dicomStudy.StudyDescr
                                                  && p.ModalityId == study.ModalityId
                                               select p).FirstOrDefault();
                        if (procedure == null)
                        {
                            procedure = new Procedure();
                            procedure.Name = dicomStudy.StudyDescr;
                            procedure.ModalityId = study.ModalityId.Value;
                            procedure.CreatedBy = AdminUserId;
                            procedure.CreationDate = DateTime.Now;
                            procedure.LastUpdatedBy = AdminUserId;
                            procedure.LastUpdateDate = DateTime.Now;
                        }
                        study.Procedure = procedure;
                    }
                    dicomStudy.SyncTime = DateTime.Now;
                    //study = SetAllSeries(dicomStudy, study);
                    study.CreatedBy = AdminUserId;
                    study.LastUpdatedBy = AdminUserId;
                    study.CreationDate = DateTime.Now;
                    study.LastUpdateDate = DateTime.Now;
                    risDB.AddToStudies(study);
                    risDB.SaveChanges();
                    conquestDB.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.StackTrace);
                    Console.WriteLine("-----Inner Exception------");
                    Console.Write(ex.InnerException.StackTrace);
                    risDB.Refresh(System.Data.Objects.RefreshMode.StoreWins, risDB);
                    conquestDB.Refresh(System.Data.Objects.RefreshMode.StoreWins, conquestDB);
                }
            }
        }

        private Study SetHospitalAndRefPhy(DICOMStudy dicomStudy, Study risStudy)
        {
            if (dicomStudy.ReferPhysi != null)
            {
                string referringPhysician = "";
                string hospitalCode = "";
                foreach (char ch in dicomStudy.ReferPhysi.Replace("^", " ").ToCharArray())
                {
                    if (Char.IsDigit(ch))
                    {
                        hospitalCode += ch;
                    }
                    else
                    {
                        referringPhysician += ch;
                    }
                }
                if (hospitalCode.Length > 0)
                {
                    Hospital hospital = (from h in risDB.Hospitals where h.Code == hospitalCode select h).FirstOrDefault();
                    if (hospital != null)
                    {
                        risStudy.HospitalId = hospital.HospitalId;
                        risStudy.ClientId = hospital.ClientId;
                    }
                } 
                if (referringPhysician.Length > 0)
                {
                    User refPhy = (from u in risDB.Users where u.Name == referringPhysician select u).FirstOrDefault();
                    if (refPhy != null)
                    {
                        risStudy.ReferringPhysician = refPhy;
                    }
                    else
                    {
                        risStudy.ReferringPhysician = CreateUser(referringPhysician);
                    }
                }

            }
            return risStudy;
        }

        private Study SetModality(DICOMStudy dicomStudy, Study study)
        {
            study.Modality = (from m in risDB.Modalities where m.Name == dicomStudy.StudyModal select m).FirstOrDefault();
            return study;
        }

        public User CreateUser(string name)
        {
            string[] names = name.Split(',');
            StringBuilder userId = new StringBuilder();
            if (names.Length > 1)
            {
                if (names[1].Trim().Length > 0)
                    userId.Append(names[1].Trim().ToLower()[0]);
                userId.Append(names[0].Trim().ToLower());
            }
            else if (names.Length == 1)
            {
                userId.Append(names[0].Trim().ToLower());
            }
            else
            {
                userId.Append("risuser");
            }
            bool quit = false;
            int count = 1;
            User user = null;
            do
            {
                string userIdStr = userId.ToString();
                user = (from u in risDB.Users where u.LoginName == userIdStr select u).FirstOrDefault();
                if (user == null)
                {
                    quit = true;
                    
                }
                else
                {
                    userId.Append(count.ToString());
                    count++;
                }
            }
            while (!quit);
            user = new User();
            user.Name = name;
            user.LoginName = userId.ToString();
            user.Password = "password";
            user.IsActive = true;
            user.CreatedBy = AdminUserId;
            user.CreationDate = DateTime.Now;
            user.LastUpdatedBy = AdminUserId;
            user.LastUpdateDate = DateTime.Now;
            return user;
        }

        private void MigrateSeries()
        {
            List<DICOMSeries> dicomSeriesList = (from s in conquestDB.DICOMSeries
                                                 where s.SyncTime == null select s).ToList();
            foreach (DICOMSeries dicomSeries in dicomSeriesList)
            {                
                try 
	            {	
                    Study study = (from s in risDB.Studies where s.StudyInstance == dicomSeries.StudyInsta select s).FirstOrDefault();
                    if (study != null)
                    {
                        Series series = new Series();
                        series.StudyId = study.StudyId;
                        series.SeriesInstance = dicomSeries.SeriesInst;
                        series.SeriesNumber = dicomSeries.SeriesNumb;
                        series.SeriesDate = ParseDateTime(dicomSeries.SeriesDate, dicomSeries.SeriesTime);
                        series.Description = dicomSeries.SeriesDesc;
                        series.PatientPosition = dicomSeries.PatientPos;
                        series.Contrast = dicomSeries.ContrastBo;
                        series.ProtocolName = dicomSeries.ProtocolNa;
                        series.FrameOfReference = dicomSeries.FrameOfRef;
                        series.BodyPartExamined = dicomSeries.BodyPartEx;
                        series = SetModalityDetail(dicomSeries, study, series);                      

                        series.CreatedBy = AdminUserId;
                        series.LastUpdateDate = DateTime.Now;
                        series.LastUpdatedBy = AdminUserId;
                        series.CreationDate = DateTime.Now;
                        
                        dicomSeries.SyncTime = DateTime.Now;
                        //study.Series.Add(series);
                        risDB.AddToSeries(series);
                        study = SetStationAndDetail(dicomSeries, study);
                        risDB.SaveChanges();
                        conquestDB.SaveChanges();
                    }
	            }
	            catch (Exception ex)
	            {
		            Console.Write(ex.StackTrace);
                    Console.WriteLine("-----Inner Exception------");
                    Console.Write(ex.InnerException.StackTrace);
                    risDB.Refresh(System.Data.Objects.RefreshMode.StoreWins, risDB);
                    conquestDB.Refresh(System.Data.Objects.RefreshMode.StoreWins, conquestDB);
	            }
            }            
        }

        private void MigrateImages()
        {
            List<DICOMImage> dicomImagesList = (from i in conquestDB.DICOMImages
                                                 where i.SyncTime == null
                                                 select i).ToList();

            foreach (DICOMImage dicomImage in dicomImagesList)
            {
                try
                {
                    Series series = (from s in risDB.Series where s.SeriesInstance == dicomImage.SeriesInst select s).FirstOrDefault();
                    if (series != null)
                    {
                        Image risImage = new Image();
                        risImage.ImageInstance = dicomImage.SOPInstanc;

                        risImage.ImageClassUI = dicomImage.SOPClassUI;
                        risImage.ImageNumber = dicomImage.ImageNumbe;
                        risImage.ImageDate = ParseDateTime(dicomImage.ImageDate, dicomImage.ImageTime);
                        risImage.EchoNumber = dicomImage.EchoNumber;
                        risImage.NumberOfFrames = dicomImage.NumberOfFr;
                        risImage.AcquiredDate = ParseDateTime(dicomImage.AcqDate, dicomImage.AcqTime);
                        risImage.SliceLocation = dicomImage.SliceLocat;
                        risImage.NumberOfSamples = dicomImage.SamplesPer;
                        risImage.PhotoMetric = dicomImage.PhotoMetri;
                        risImage.Rows = dicomImage.Rows;
                        risImage.Columns = dicomImage.Colums;
                        risImage.BitsStored = dicomImage.BitsStored;
                        risImage.Path = dicomImage.ObjectFile;
                        risImage.DeviceName = dicomImage.DeviceName;

                        risImage.CreatedBy = AdminUserId;
                        risImage.LastUpdateDate = DateTime.Now;
                        risImage.CreationDate = DateTime.Now;
                        risImage.LastUpdatedBy = AdminUserId;

                        risImage.SeriesId = series.SeriesId;
                        risDB.AddToImages(risImage);
                        dicomImage.SyncTime = DateTime.Now;
                        risDB.SaveChanges();
                        conquestDB.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.StackTrace);
                    Console.WriteLine("-----Inner Exception------");
                    Console.Write(ex.InnerException.StackTrace);
                    risDB.Refresh(System.Data.Objects.RefreshMode.StoreWins, risDB);
                    conquestDB.Refresh(System.Data.Objects.RefreshMode.StoreWins, conquestDB);
                }
            }                        
        }

        private Study SetStationAndDetail(DICOMSeries dicomSeries,Study study)
        {
            Station station = (from s in risDB.Stations
                               where s.ModalityId == study.ModalityId
                                  && s.StationName == dicomSeries.StationNam
                                  && s.Instituition == dicomSeries.Institutio
                               select s).FirstOrDefault();
            if (station != null)
            {
                study.StationId = station.StationId;
                study.HospitalId = station.HospitalId;
                study.ClientId = station.ClientId;
            }
            return study;
        }

        private Series SetModalityDetail(DICOMSeries dicomSeries, Study study, Series series)
        {
            ModalityDetail modalityDetail = (from md in risDB.ModalityDetails
                                             where md.Manufacturer == dicomSeries.Manufactur
                                             && md.ModelName == dicomSeries.ModelName
                                             && md.ModalityId == study.ModalityId 
                                             select md).FirstOrDefault();
            if(modalityDetail == null)
            {
                modalityDetail = new ModalityDetail();
                modalityDetail.CreatedBy = AdminUserId;
                modalityDetail.CreationDate = DateTime.Now;
                modalityDetail.LastUpdateDate = DateTime.Now;
                modalityDetail.LastUpdatedby = AdminUserId;
                modalityDetail.Manufacturer = dicomSeries.Manufactur;
                modalityDetail.ModalityId = study.ModalityId.Value;
                modalityDetail.ModelName = dicomSeries.Manufactur;                
            }
            series.ModalityDetail = modalityDetail;
            return series;
        }        

        private Nullable<DateTime> ParseDateTime(string dateStr,string timeStr)
        {
            if (dateStr != null && timeStr != null)
                return new DateTime(int.Parse(dateStr.Substring(0, 4)), int.Parse(dateStr.Substring(4, 2)), int.Parse(dateStr.Substring(6, 2)), int.Parse(timeStr.Substring(0, 2)), int.Parse(timeStr.Substring(2, 2)), int.Parse(timeStr.Substring(4, 2)));
            else if (dateStr != null)
                return new DateTime(int.Parse(dateStr.Substring(0, 4)), int.Parse(dateStr.Substring(4, 2)), int.Parse(dateStr.Substring(6, 2)));
            else
                return null;
        }

       
        private string ParseName(string name)
        {
            name = name.Replace("^", " ");
            char [] sep = new char[1];
            sep[0] = ' ';
            string[] names = name.Split(sep);
            for(int i=0;i<names.Length;i++)
            {
                names[i] = names[i].Trim();
            }
            if (names.Length == 2)
            {
                return names[1] + "," + names[0];
            }
            return names[0];
        }
    }
}
