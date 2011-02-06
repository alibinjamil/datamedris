using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace RIS.Common
{
    public partial class Study : EntityObject
    {
        public List<Study> GetParents()
        {
            List<Study> parentList = new List<Study>();
            Study currentStudy = this;
            while (currentStudy.ParentStudy != null)
            {
                currentStudy = currentStudy.ParentStudy;
                parentList.Add(currentStudy);
            }
            return parentList;
        }
    }
}
