using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace Bendyline.Base
{
    public partial class SerializableObject 
    {
        public String Xml
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                return sb.ToString();
            }

            set
            {
            }
        }

        public SerializableObject()
        {
        }

        protected virtual void OnPostSerialization()
        {

        }
    }
}
