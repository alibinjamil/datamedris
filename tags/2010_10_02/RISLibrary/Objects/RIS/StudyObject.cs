// Generated by CodeGenerator
// DO NOT MODIFY!
using RIS.RISLibrary.Fields;
using System;
namespace RIS.RISLibrary.Objects.RIS
{
	public class StudyObject : RISObject
	{
		override public string GetTableName()
		{
			return "tStudies";
		}
		override public bool HasAccessColumns()
		{
			return true;
		}
		PrimaryKeyField m_StudyId = new PrimaryKeyField("StudyId",null,true);
		override public PrimaryKeyField GetPrimaryKey()
		{
			return m_StudyId;
		}
		public PrimaryKeyField StudyId
		{
			get
			{
				return this.m_StudyId;
			}
			set
			{
				this.m_StudyId = value;
			}
		}
		TextField m_StudyInstance = new TextField("StudyInstance",null);
		public TextField StudyInstance
		{
			get
			{
				return this.m_StudyInstance;
			}
			set
			{
				this.m_StudyInstance = value;
			}
		}
		DateTimeField m_StudyDate = new DateTimeField("StudyDate",null);
		public DateTimeField StudyDate
		{
			get
			{
				return this.m_StudyDate;
			}
			set
			{
				this.m_StudyDate = value;
			}
		}
		IntField m_ProcedureId = new IntField("ProcedureId",null);
		public IntField ProcedureId
		{
			get
			{
				return this.m_ProcedureId;
			}
			set
			{
				this.m_ProcedureId = value;
			}
		}
		IntField m_ReferringPhysicianId = new IntField("ReferringPhysicianId",null);
		public IntField ReferringPhysicianId
		{
			get
			{
				return this.m_ReferringPhysicianId;
			}
			set
			{
				this.m_ReferringPhysicianId = value;
			}
		}
		IntField m_PatientId = new IntField("PatientId",null);
		public IntField PatientId
		{
			get
			{
				return this.m_PatientId;
			}
			set
			{
				this.m_PatientId = value;
			}
		}
		TextField m_PatientWeight = new TextField("PatientWeight",null);
		public TextField PatientWeight
		{
			get
			{
				return this.m_PatientWeight;
			}
			set
			{
				this.m_PatientWeight = value;
			}
		}
		IntField m_ModalityId = new IntField("ModalityId",null);
		public IntField ModalityId
		{
			get
			{
				return this.m_ModalityId;
			}
			set
			{
				this.m_ModalityId = value;
			}
		}
		IntField m_StationId = new IntField("StationId",null);
		public IntField StationId
		{
			get
			{
				return this.m_StationId;
			}
			set
			{
				this.m_StationId = value;
			}
		}
		IntField m_StudyStatusId = new IntField("StudyStatusId",null);
		public IntField StudyStatusId
		{
			get
			{
				return this.m_StudyStatusId;
			}
			set
			{
				this.m_StudyStatusId = value;
			}
		}
		IntField m_LatestSeriesId = new IntField("LatestSeriesId",null);
		public IntField LatestSeriesId
		{
			get
			{
				return this.m_LatestSeriesId;
			}
			set
			{
				this.m_LatestSeriesId = value;
			}
		}
		IntField m_LatestFindingId = new IntField("LatestFindingId",null);
		public IntField LatestFindingId
		{
			get
			{
				return this.m_LatestFindingId;
			}
			set
			{
				this.m_LatestFindingId = value;
			}
		}
		TextField m_IsManual = new TextField("IsManual",null);
		public TextField IsManual
		{
			get
			{
				return this.m_IsManual;
			}
			set
			{
				this.m_IsManual = value;
			}
		}
		TextField m_AccessionNumber = new TextField("AccessionNumber",null);
		public TextField AccessionNumber
		{
			get
			{
				return this.m_AccessionNumber;
			}
			set
			{
				this.m_AccessionNumber = value;
			}
		}
		IntField m_HospitalId = new IntField("HospitalId",null);
		public IntField HospitalId
		{
			get
			{
				return this.m_HospitalId;
			}
			set
			{
				this.m_HospitalId = value;
			}
		}
		TextField m_TechComments = new TextField("TechComments",null);
		public TextField TechComments
		{
			get
			{
				return this.m_TechComments;
			}
			set
			{
				this.m_TechComments = value;
			}
		}
		IntField m_ClientId = new IntField("ClientId",null);
		public IntField ClientId
		{
			get
			{
				return this.m_ClientId;
			}
			set
			{
				this.m_ClientId = value;
			}
		}
		TextField m_BodyPartExamined = new TextField("BodyPartExamined",null);
		public TextField BodyPartExamined
		{
			get
			{
				return this.m_BodyPartExamined;
			}
			set
			{
				this.m_BodyPartExamined = value;
			}
		}
		IntField m_TemplateId = new IntField("TemplateId",null);
		public IntField TemplateId
		{
			get
			{
				return this.m_TemplateId;
			}
			set
			{
				this.m_TemplateId = value;
			}
		}
		TextField m_RejectionReason = new TextField("RejectionReason",null);
		public TextField RejectionReason
		{
			get
			{
				return this.m_RejectionReason;
			}
			set
			{
				this.m_RejectionReason = value;
			}
		}
		override public Field[] GetFields()
		{
			Field[] fields = new Field[19];
			fields[0] = m_StudyInstance;
			fields[1] = m_StudyDate;
			fields[2] = m_ProcedureId;
			fields[3] = m_ReferringPhysicianId;
			fields[4] = m_PatientId;
			fields[5] = m_PatientWeight;
			fields[6] = m_ModalityId;
			fields[7] = m_StationId;
			fields[8] = m_StudyStatusId;
			fields[9] = m_LatestSeriesId;
			fields[10] = m_LatestFindingId;
			fields[11] = m_IsManual;
			fields[12] = m_AccessionNumber;
			fields[13] = m_HospitalId;
			fields[14] = m_TechComments;
			fields[15] = m_ClientId;
			fields[16] = m_BodyPartExamined;
			fields[17] = m_TemplateId;
			fields[18] = m_RejectionReason;
			return fields;
		}
		override public Field[] GetAllFields()
		{
			Field[] fields = new Field[20];
			fields[0] = m_StudyId;
			fields[1] = m_StudyInstance;
			fields[2] = m_StudyDate;
			fields[3] = m_ProcedureId;
			fields[4] = m_ReferringPhysicianId;
			fields[5] = m_PatientId;
			fields[6] = m_PatientWeight;
			fields[7] = m_ModalityId;
			fields[8] = m_StationId;
			fields[9] = m_StudyStatusId;
			fields[10] = m_LatestSeriesId;
			fields[11] = m_LatestFindingId;
			fields[12] = m_IsManual;
			fields[13] = m_AccessionNumber;
			fields[14] = m_HospitalId;
			fields[15] = m_TechComments;
			fields[16] = m_ClientId;
			fields[17] = m_BodyPartExamined;
			fields[18] = m_TemplateId;
			fields[19] = m_RejectionReason;
			return fields;
		}
	}
}
