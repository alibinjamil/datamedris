// Generated by CodeGenerator
// DO NOT MODIFY!
using RIS.RISLibrary.Fields;
using System;
namespace RIS.RISLibrary.Objects.RIS
{
	public class TemplateObject : RISObject
	{
		override public string GetTableName()
		{
			return "tTemplates";
		}
		override public bool HasAccessColumns()
		{
			return true;
		}
		PrimaryKeyField m_TemplateId = new PrimaryKeyField("TemplateId",null,true);
		override public PrimaryKeyField GetPrimaryKey()
		{
			return m_TemplateId;
		}
		public PrimaryKeyField TemplateId
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
		TextField m_Text = new TextField("Text",null);
		public TextField Text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.m_Text = value;
			}
		}
		override public Field[] GetFields()
		{
			Field[] fields = new Field[2];
			fields[0] = m_Name;
			fields[1] = m_Text;
			return fields;
		}
		override public Field[] GetAllFields()
		{
			Field[] fields = new Field[3];
			fields[0] = m_TemplateId;
			fields[1] = m_Name;
			fields[2] = m_Text;
			return fields;
		}
	}
}
