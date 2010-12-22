using System;
using System.Collections.Generic;
using System.Text;

using RIS.RISLibrary.Database;
namespace RIS.RISLibrary.Objects.RIS
{
    public abstract class RISObject : GenericObject
    {
        public override DatabaseAccessLayer GetDatabaseAccessLayer()
        {
            return new RISDatabaseAccessLayer();
        }
    }
}
