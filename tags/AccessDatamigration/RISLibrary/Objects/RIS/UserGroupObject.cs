// Generated by CodeGenerator
// DO NOT MODIFY!
using RIS.RISLibrary.Fields;
using System;
namespace RIS.RISLibrary.Objects.RIS
{
	public class UserGroupObject : RISObject
	{
		override public string GetTableName()
		{
			return "tUserGroups";
		}
		override public bool HasAccessColumns()
		{
			return true;
		}
		PrimaryKeyField m_UserGroupId = new PrimaryKeyField("UserGroupId",null,true);
		override public PrimaryKeyField GetPrimaryKey()
		{
			return m_UserGroupId;
		}
		public PrimaryKeyField UserGroupId
		{
			get
			{
				return this.m_UserGroupId;
			}
			set
			{
				this.m_UserGroupId = value;
			}
		}
		IntField m_UserId = new IntField("UserId",null);
		public IntField UserId
		{
			get
			{
				return this.m_UserId;
			}
			set
			{
				this.m_UserId = value;
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
			fields[0] = m_UserId;
			fields[1] = m_GroupId;
			return fields;
		}
		override public Field[] GetAllFields()
		{
			Field[] fields = new Field[3];
			fields[0] = m_UserGroupId;
			fields[1] = m_UserId;
			fields[2] = m_GroupId;
			return fields;
		}
	}
}
