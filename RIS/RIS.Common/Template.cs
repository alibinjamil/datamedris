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
    public partial class Template : EntityObject
    {
        public string ModalityName
        { 
            get { return this.BodyPart.Modality.Name; } 
        }

        public string BodyPartName
        {
            get { return this.BodyPart.Name; }
        }
    }
}
