// Generated by CodeGenerator
// DO NOT MODIFY!
using RIS.RISLibrary.Fields;
using System;
namespace RIS.RISLibrary.Objects.RIS
{
	public class HospitalGroupObject : RISObject
	{
		override public string GetTableName()
		{
			return "tHospitalGroups";
		}
		override public bool HasAccessColumns()
		{
			return true;
		}
		PrimaryKeyField m_HospitalGroupId = new PrimaryKeyField("HospitalGroupId",null,true);
		override public PrimaryKeyField GetPrimaryKey()
		{
			return m_HospitalGroupId;
		}
		public PrimaryKeyField HospitalGroupId
		{
			get
			{
				return this.m_HospitalGroupId;
			}
			set
			{
				this.m_HospitalGroupId = value;
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
		IntField m_GroupId = new IntField("GroupId",null);
		public IntField GroupId
		{
			get
			{
				return this.m_GroupId;
			}
			set
			{
				this.m_GroupId = value;
			}
		}
		override public Field[] GetFields()
		{
			Field[] fields = new Field[2];
			fields[0] = m_HospitalId;
			fields[1] = m_GroupId;
			return fields;
		}
		override public Field[] GetAllFields()
		{
			Field[] fields = new Field[3];
			fields[0] = m_HospitalGroupId;
			fields[1] = m_HospitalId;
			fields[2] = m_GroupId;
			return fields;
		}
	}
}