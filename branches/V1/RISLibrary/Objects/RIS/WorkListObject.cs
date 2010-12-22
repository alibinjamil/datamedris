// Generated by CodeGenerator
// DO NOT MODIFY!
using RIS.RISLibrary.Fields;
using System;
namespace RIS.RISLibrary.Objects.RIS
{
	public class WorkListObject : RISObject
	{
		override public string GetTableName()
		{
			return "tWorkLists";
		}
		override public bool HasAccessColumns()
		{
			return true;
		}
		PrimaryKeyField m_WorkListId = new PrimaryKeyField("WorkListId",null,true);
		override public PrimaryKeyField GetPrimaryKey()
		{
			return m_WorkListId;
		}
		public PrimaryKeyField WorkListId
		{
			get
			{
				return this.m_WorkListId;
			}
			set
			{
				this.m_WorkListId = value;
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
		IntField m_RequestingPhysicianId = new IntField("RequestingPhysicianId",null);
		public IntField RequestingPhysicianId
		{
			get
			{
				return this.m_RequestingPhysicianId;
			}
			set
			{
				this.m_RequestingPhysicianId = value;
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
		IntField m_PerformingPhysicianId = new IntField("PerformingPhysicianId",null);
		public IntField PerformingPhysicianId
		{
			get
			{
				return this.m_PerformingPhysicianId;
			}
			set
			{
				this.m_PerformingPhysicianId = value;
			}
		}
		TextField m_ExternalPatientId = new TextField("ExternalPatientId",null);
		public TextField ExternalPatientId
		{
			get
			{
				return this.m_ExternalPatientId;
			}
			set
			{
				this.m_ExternalPatientId = value;
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
		override public Field[] GetFields()
		{
			Field[] fields = new Field[8];
			fields[0] = m_PatientId;
			fields[1] = m_RequestingPhysicianId;
			fields[2] = m_ModalityId;
			fields[3] = m_ProcedureId;
			fields[4] = m_StationId;
			fields[5] = m_PerformingPhysicianId;
			fields[6] = m_ExternalPatientId;
			fields[7] = m_StudyInstance;
			return fields;
		}
		override public Field[] GetAllFields()
		{
			Field[] fields = new Field[9];
			fields[0] = m_WorkListId;
			fields[1] = m_PatientId;
			fields[2] = m_RequestingPhysicianId;
			fields[3] = m_ModalityId;
			fields[4] = m_ProcedureId;
			fields[5] = m_StationId;
			fields[6] = m_PerformingPhysicianId;
			fields[7] = m_ExternalPatientId;
			fields[8] = m_StudyInstance;
			return fields;
		}
	}
}
