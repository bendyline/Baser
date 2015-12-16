using System;
using System.Text;

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
