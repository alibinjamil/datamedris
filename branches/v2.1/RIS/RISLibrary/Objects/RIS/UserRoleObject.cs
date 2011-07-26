// Generated by CodeGenerator
// DO NOT MODIFY!
using RIS.RISLibrary.Fields;
using System;
namespace RIS.RISLibrary.Objects.RIS
{
	public class UserRoleObject : RISObject
	{
		override public string GetTableName()
		{
			return "UserRoles";
		}
		override public bool HasAccessColumns()
		{
			return true;
		}
		PrimaryKeyField m_UserRoleId = new PrimaryKeyField("UserRoleId",null,true);
		override public PrimaryKeyField GetPrimaryKey()
		{
			return m_UserRoleId;
		}
		public PrimaryKeyField UserRoleId
		{
			get
			{
				return this.m_UserRoleId;
			}
			set
			{
				this.m_UserRoleId = value;
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
		IntField m_RoleId = new IntField("RoleId",null);
		public IntField RoleId
		{
			get
			{
				return this.m_RoleId;
			}
			set
			{
				this.m_RoleId = value;
			}
		}
		override public Field[] GetFields()
		{
			Field[] fields = new Field[2];
			fields[0] = m_UserId;
			fields[1] = m_RoleId;
			return fields;
		}
		override public Field[] GetAllFields()
		{
			Field[] fields = new Field[3];
			fields[0] = m_UserRoleId;
			fields[1] = m_UserId;
			fields[2] = m_RoleId;
			return fields;
		}
	}
}
