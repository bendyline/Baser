using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public class SerializableTypeAttribute : Attribute
    {
        private String tagName;

        public String TagName
        {
            get { return this.tagName; }
            set { this.tagName = value; }
        }
    }
}
