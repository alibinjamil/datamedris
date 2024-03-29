using System;
using System.Collections.Generic;
using System.Text;

using RIS.RISLibrary.Objects.DICOM;
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;

namespace RIS.RISService.DataMigrators
{
    class PatientsDataMigrator : GenericDataMigrator
    {
        public PatientsDataMigrator()
        {
        }
        protected override DICOMObject GetDICOMObject()
        {
            return new DICOMPatientObject();
        }

        protected override RISObject GetRISObject()
        {
            return new PatientObject();
        }
        protected override string GetRISWhereClause()
        {
            return "";
        }
        protected override void UpdateDICOMObject(DICOMObject dicomObject)
        {
            DICOMPatientObject patient = (DICOMPatientObject)dicomObject;
            patient.SyncTime.Value = DateTime.Now;
            patient.Update(Constants.Database.SystemUserId);
        }

        protected override RISObject GetRISObject(DICOMObject dicomObject)
        {
            PatientObject risPatient = new PatientObject();
            DICOMPatientObject dicomPatient = (DICOMPatientObject)dicomObject;
            Console.WriteLine("Patient Synced:" + dicomPatient.PatientID);
            risPatient.ExternalPatientId.Value = dicomPatient.PatientID.Value;
            risPatient.Name.Value = dicomPatient.Name.Value;
            risPatient.DateOfBirth.Value = dicomPatient.DateOfBirth.Value;
            //risPatient.DateOfBirth.Value = DateTime.Now.ToString();
            risPatient.Gender.Value = dicomPatient.Gender.Value;
            return risPatient;            
        }
        protected override bool AreEqual(DICOMObject dicomObject, RISObject risObject)
        {
            DICOMPatientObject dicomPatient = (DICOMPatientObject)dicomObject;
            PatientObject risPatient = (PatientObject)risObject;
            return dicomPatient.PatientID.Value.Equals(risPatient.ExternalPatientId.Value);
        }
        protected override void PerformPostSaveTasks(RISObject risObject)
        {

        }
    }
}
