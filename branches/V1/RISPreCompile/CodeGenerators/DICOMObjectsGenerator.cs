using System;
using System.Collections.Generic;
using System.Text;

namespace RIS.RISPreCompile.CodeGenerators
{
    public class DICOMObjectsGenerator : ObjectsGenerator
    {
        public DICOMObjectsGenerator()
        {
        }
        protected override string GetExtends()
        {
            return "DICOMObject";
        }
        protected override string GetXMLFilePath()
        {
            return @"E:\MyProjects\RIS\trunk\RISPreCompile\Resources\DICOMDatabaseObjectMapping.xml";
        }
        protected override string[] GetIncludes()
        {
            string[] includes = new string[2];
            includes[0] = "RIS.RISLibrary.Fields";
            includes[1] = "System";
            return includes;
        }
        protected override string GetNameSpace()
        {
            return "RIS.RISLibrary.Objects.DICOM";
        }
        protected override string GetOutputPath()
        {
            return @"E:\MyProjects\RIS\trunk\RISLibrary\Objects\DICOM\"; 
        }
    }
}
