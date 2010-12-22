using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using RIS.RISLibrary.Objects.DICOM;
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;
using RIS.RISLibrary.Utilities;

namespace RIS.RISService.DataMigrators
{
    class StudiesDataMigrator : GenericDataMigrator
    {
        public StudiesDataMigrator()
        {
        }
        protected override DICOMObject GetDICOMObject()
        {
            return new DICOMStudyObject();
        }
        protected override string GetDICOMWhereClause()
        {
            return "";
        }
        protected override RISObject GetRISObject()
        {
            return new StudyObject();
        }
        protected override string GetRISWhereClause()
        {
            return "";
        }
        protected override RISObject GetRISObject(DICOMObject dicomObject)
        {
            StudyObject risStudy = new StudyObject();
            DICOMStudyObject dicomStudy = (DICOMStudyObject)dicomObject;
            risStudy.StudyStatusId.Value = Constants.StudyStatusTypes.New;
            risStudy.StudyInstance.Value = dicomStudy.StudyInstance.Value;
            if (((string)dicomStudy.StudyInstance.Value).Equals("1.3.12.2.1107.5.1.4.29126.30000007122815364739000000004"))
            {
                int debug = 0;
            }
            risStudy.StudyDate.Value = DatabaseUtility.GetDateTime(dicomStudy.StudyDate.Value,dicomStudy.StudyTime.Value);
            
            //risStudy.Description.Value = dicomStudy.StudyDescription.Value;
            SetReferringPhysician(dicomStudy, risStudy);
            
            risStudy.PatientWeight.Value = dicomStudy.PatientsWeight.Value;
            
            ModalityObject modality = new ModalityObject();
            modality.Name.Value = dicomStudy.StudyModal.Value;
            modality.Load();
            if (!modality.IsLoaded)
            {
                modality.Save();
            }
            risStudy.ModalityId.Value = modality.ModalityId.Value;

            if (dicomStudy.StationName.Value != null)
            {
                StationObject station = new StationObject();
                station.ModalityId.Value = modality.GetPrimaryKey().Value;
                station.StationName.Value = dicomStudy.StationName.Value;
                station.Instituition.Value = dicomStudy.Instituition.Value;
                station.Load();
                if (!station.IsLoaded)
                {
                    station.Save();
                }
                risStudy.StationId.Value = station.GetPrimaryKey().Value;
            }
            
            if (dicomStudy.StudyDescription.Value != null)
            {
                ProcedureObject procedure = new ProcedureObject();
                procedure.Name.Value = dicomStudy.StudyDescription.Value;
                procedure.ModalityId.Value = modality.ModalityId.Value;
                procedure.Load();
                if (procedure.IsLoaded == false)
                {
                    procedure.Save();
                }
                risStudy.ProcedureId.Value = procedure.ProcedureId.Value;
            }
                        
            PatientObject patient = new PatientObject();
            patient.ExternalPatientId.Value = dicomStudy.PatientID.Value;
            patient.Load();
            if (patient.IsLoaded)
            {
                risStudy.PatientId.Value = patient.GetPrimaryKey().Value;
            }
            else
            {
                patient.Name.Value = dicomStudy.PatientName.Value;
                patient.DateOfBirth.Value = dicomStudy.PatientDateOfBirth.Value;
                patient.Gender.Value = dicomStudy.PatientSex.Value;
                patient.Save();
                risStudy.PatientId.Value = patient.GetPrimaryKey().Value;
            }
            return risStudy;            
        }
        protected override bool AreEqual(DICOMObject dicomObject, RISObject risObject)
        {
            DICOMStudyObject dicomStudy = (DICOMStudyObject)dicomObject;
            StudyObject risStudy = (StudyObject)risObject;
            return dicomStudy.StudyInstance.Value.Equals(risStudy.StudyInstance.Value);
        }
        protected override void PerformPostSaveTasks(RISObject risObject)
        {
            StudyObject risStudy = (StudyObject)risObject;

                RISDatabaseAccessLayer databaseAccessLayer = new RISDatabaseAccessLayer();
                SqlConnection connection = (SqlConnection)databaseAccessLayer.GetConnection();
                connection.Open();
                SqlCommand command = new SqlCommand("sp_insert_study_group", connection);
                command.Parameters.AddWithValue("@studyId", risStudy.GetPrimaryKey().Value);
                if(risStudy.ReferringPhysicianId.Value != null)
                    command.Parameters.AddWithValue("@userId", risStudy.ReferringPhysicianId.Value);
                else
                    command.Parameters.AddWithValue("@userId", DBNull.Value);
                command.Parameters.AddWithValue("@adminUserId", GenericDataMigrator.AdminUserId);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                connection.Close();
            
        }

        private bool SetReferringPhysician(DICOMStudyObject dicomStudy,StudyObject risStudy)
        {
            //As our system is not being used to entrer work lists we can not pick up Referrring Physicians from our own worklist
            //therrefore the worklist code has been commented for now and we just create a user
            bool isSet = false;
            /*WorkListObject workList = new WorkListObject();
            workList.WorkListId.Value = dicomStudy.AccessionNumber.Value;
            workList.Load(GenericDataMigrator.AdminUserId);
            if (workList.IsLoaded)
            {
                risStudy.ReferringPhysicianId.Value = workList.RequestingPhysicianId.Value;
                isSet = true;
            }
            else*/ 
            if(dicomStudy.ReferringPhysician.Value != null)
            {
                UserObject user = new UserObject();
                user.Name.Value = dicomStudy.ReferringPhysician.Value;
                user.Load();
                if (!user.IsLoaded)
                {
                    user = DatabaseUtility.CreateUser((string)dicomStudy.ReferringPhysician.Value);
                    UserRoleObject userRole = new UserRoleObject();
                    userRole.UserId.Value = user.GetPrimaryKey().Value;
                    userRole.RoleId.Value = Constants.Roles.ReferringPhysician;
                    userRole.Save();                    
                }
                risStudy.ReferringPhysicianId.Value = user.UserId.Value;
                isSet = true;
            }
            return isSet;
        }
    }
}

