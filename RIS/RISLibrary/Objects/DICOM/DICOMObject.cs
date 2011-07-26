using System;
using System.Collections.Generic;
using System.Text;

using RIS.RISLibrary.Database;
using RIS.RISLibrary.Fields;

namespace RIS.RISLibrary.Objects.DICOM
{
    public abstract class DICOMObject : GenericObject
    {
        public override DatabaseAccessLayer GetDatabaseAccessLayer()
        {
            return new DICOMDatabaseAccessLayer();
        }
       
        TextField m_Synced = new TextField("synced", null);
        public TextField Synced
        {
            get
            {
                return this.m_Synced;
            }
            set
            {
                this.m_Synced = value;
            }
        }

    }
}
