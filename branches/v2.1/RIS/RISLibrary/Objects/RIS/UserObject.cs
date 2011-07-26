// Generated by CodeGenerator
// DO NOT MODIFY!
using RIS.RISLibrary.Fields;
using System;
namespace RIS.RISLibrary.Objects.RIS
{
	public class UserObject : RISObject
	{
		override public string GetTableName()
		{
			return "Users";
		}
		override public bool HasAccessColumns()
		{
			return true;
		}
		PrimaryKeyField m_UserId = new PrimaryKeyField("UserId",null,true);
		override public PrimaryKeyField GetPrimaryKey()
		{
			return m_UserId;
		}
		public PrimaryKeyField UserId
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
		TextField m_LoginName = new TextField("LoginName",null);
		public TextField LoginName
		{
			get
			{
				return this.m_LoginName;
			}
			set
			{
				this.m_LoginName = value;
			}
		}
		TextField m_Password = new TextField("Password",null);
		public TextField Password
		{
			get
			{
				return this.m_Password;
			}
			set
			{
				this.m_Password = value;
			}
		}
		TextField m_Name = new TextField("Name",null);
		public TextField Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}
		TextField m_IsActive = new TextField("IsActive",null);
		public TextField IsActive
		{
			get
			{
				return this.m_IsActive;
			}
			set
			{
				this.m_IsActive = value;
			}
		}
		DateTimeField m_LastLoginDate = new DateTimeField("LastLoginDate",null);
		public DateTimeField LastLoginDate
		{
			get
			{
				return this.m_LastLoginDate;
			}
			set
			{
				this.m_LastLoginDate = value;
			}
		}
		IntField m_SecretQuestionId = new IntField("SecretQuestionId",null);
		public IntField SecretQuestionId
		{
			get
			{
				return this.m_SecretQuestionId;
			}
			set
			{
				this.m_SecretQuestionId = value;
			}
		}
		TextField m_Answer = new TextField("Answer",null);
		public TextField Answer
		{
			get
			{
				return this.m_Answer;
			}
			set
			{
				this.m_Answer = value;
			}
		}
		TextField m_ResetPassword = new TextField("ResetPassword",null);
		public TextField ResetPassword
		{
			get
			{
				return this.m_ResetPassword;
			}
			set
			{
				this.m_ResetPassword = value;
			}
		}
		TextField m_Mobile = new TextField("Mobile",null);
		public TextField Mobile
		{
			get
			{
				return this.m_Mobile;
			}
			set
			{
				this.m_Mobile = value;
			}
		}
		IntField m_CarrierId = new IntField("CarrierId",null);
		public IntField CarrierId
		{
			get
			{
				return this.m_CarrierId;
			}
			set
			{
				this.m_CarrierId = value;
			}
		}
		TextField m_SendSMS = new TextField("SendSMS",null);
		public TextField SendSMS
		{
			get
			{
				return this.m_SendSMS;
			}
			set
			{
				this.m_SendSMS = value;
			}
		}
		override public Field[] GetFields()
		{
			Field[] fields = new Field[11];
			fields[0] = m_LoginName;
			fields[1] = m_Password;
			fields[2] = m_Name;
			fields[3] = m_IsActive;
			fields[4] = m_LastLoginDate;
			fields[5] = m_SecretQuestionId;
			fields[6] = m_Answer;
			fields[7] = m_ResetPassword;
			fields[8] = m_Mobile;
			fields[9] = m_CarrierId;
			fields[10] = m_SendSMS;
			return fields;
		}
		override public Field[] GetAllFields()
		{
			Field[] fields = new Field[12];
			fields[0] = m_UserId;
			fields[1] = m_LoginName;
			fields[2] = m_Password;
			fields[3] = m_Name;
			fields[4] = m_IsActive;
			fields[5] = m_LastLoginDate;
			fields[6] = m_SecretQuestionId;
			fields[7] = m_Answer;
			fields[8] = m_ResetPassword;
			fields[9] = m_Mobile;
			fields[10] = m_CarrierId;
			fields[11] = m_SendSMS;
			return fields;
		}
	}
}
