using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

namespace RIS.RISPreCompile.CodeGenerators
{
    abstract public class ObjectsGenerator : GenericGenerator
    {
        override public void Generate()
        {
            XmlDocument document = GetDocument();
            XmlElement rootElement = document.DocumentElement;
            foreach (XmlElement currentElement in rootElement.ChildNodes)
            {
                if (currentElement.Name.Equals("entity"))
                {
                    ArrayList varNames = new ArrayList();
                    string pkVarName = null;
                    OpenFile(currentElement.GetAttribute("objectName"));
                    WriteLine("override public string GetTableName()");
                    OpenBlock();
                    WriteLine("return \"" + currentElement.GetAttribute("tableName") + "\";");
                    CloseBlock();
                    WriteLine("override public bool HasAccessColumns()");
                    OpenBlock();
                    if(currentElement.HasAttribute("HasAccessColumns") && currentElement.GetAttribute("HasAccessColumns").Equals("False"))
                        WriteLine("return false;");
                    else
                        WriteLine("return true;");
                    CloseBlock();
                    foreach (XmlElement currentField in currentElement.ChildNodes)
                    {
                        string columnName = currentField.GetAttribute("columnName");
                        string fieldName;
                        if (currentField.HasAttribute("name"))
                            fieldName = currentField.GetAttribute("name");
                        else
                            fieldName = columnName;
                        string type = currentField.GetAttribute("type");
                        string varName = "m_" + fieldName;                        
                        //WriteLine(varName + ".ColumnName = \"" + columnName + "\"");
                        if (type.Equals("PrimaryKeyField"))
                        {
                            string auto = "true";
                            if (currentField.HasAttribute("Auto") && currentField.GetAttribute("Auto").Equals("False"))
                            {
                                auto = "false";
                            }
                            WriteLine(type + " " + varName + " = new " + type + "(\"" + columnName + "\",null," + auto + ");");
                            WriteLine("override public PrimaryKeyField GetPrimaryKey()");
                            OpenBlock();
                            WriteLine("return " + varName + ";");
                            CloseBlock();
                            pkVarName = varName;
                        }
                        else
                        {
                            WriteLine(type + " " + varName + " = new " + type + "(\"" + columnName + "\",null);");
                            varNames.Add(varName);
                        }
                        AddProperty(type, fieldName, varName);
                    }
                    WriteLine("override public Field[] GetFields()");
                    OpenBlock();
                    WriteLine("Field[] fields = new Field[" + varNames.Count + "];");
                    for (int i = 0; i < varNames.Count; i++)
                    {
                        WriteLine("fields[" + i + "] = " + (string)varNames[i] + ";");
                    }
                    WriteLine("return fields;");
                    CloseBlock();

                    WriteLine("override public Field[] GetAllFields()");
                    OpenBlock();
                    WriteLine("Field[] fields = new Field[" + (varNames.Count+1) + "];");
                    WriteLine("fields[0] = " + pkVarName + ";");
                    for (int i = 1; i <= varNames.Count; i++)
                    {
                        WriteLine("fields[" + i + "] = " + (string)varNames[i-1] + ";");
                    }
                    WriteLine("return fields;");
                    CloseBlock();

                    CloseFile();
                }
            }
        }
    }
}
