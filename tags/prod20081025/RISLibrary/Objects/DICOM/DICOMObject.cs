using System;
using System.Collections.Generic;
using System.Text;

using RIS.RISLibrary.Database;

namespace RIS.RISLibrary.Objects.DICOM
{
    public abstract class DICOMObject : GenericObject
    {
        public override DatabaseAccessLayer GetDatabaseAccessLayer()
        {
            return new DICOMDatabaseAccessLayer();
        }
    }
}
