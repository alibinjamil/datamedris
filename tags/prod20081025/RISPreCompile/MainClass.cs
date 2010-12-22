using System;
using System.Collections.Generic;
using System.Text;

using RIS.RISPreCompile.CodeGenerators;
using RIS.RISPreCompile.MappingGenerator;

namespace RIS.RISPreCompile
{
    class MainClass
    {
        static void Main(string[] args)
        {
            RISMappingGenerator generator1 = new RISMappingGenerator();
            generator1.Generate();
            GenericGenerator [] generators = new GenericGenerator[2];
            generators[0] = new RISObjectsGenerator();
            generators[1] = new DICOMObjectsGenerator();
            foreach(GenericGenerator generator in generators)
            {
                generator.Generate();
            }           
        }
    }
}
