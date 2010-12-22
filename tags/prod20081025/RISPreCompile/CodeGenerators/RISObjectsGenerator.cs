using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;

namespace RIS.RISPreCompile.CodeGenerators
{
    public class RISObjectsGenerator : ObjectsGenerator
    {       
        public RISObjectsGenerator()
        {
        }
        protected override string GetExtends()
        {
            return "RISObject";
        }

        protected override string GetXMLFilePath()
        {
            return @"E:\MyProjects\RIS\tags\prod20081025\RISPreCompile\Resources\RISDatabaseObjectMapping.xml";
        }
        protected override string[] GetIncludes()
        {
            string [] includes = new string[2];
            includes[0] = "RIS.RISLibrary.Fields";
            includes[1] = "System";
            return includes;
        }
        protected override string GetNameSpace()
        {
            return "RIS.RISLibrary.Objects.RIS";
        }
        protected override string GetOutputPath()
        {
            return @"E:\MyProjects\RIS\tags\prod20081025\RISLibrary\Objects\RIS\"; 
        }
    }
}
