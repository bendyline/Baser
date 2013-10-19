using System;
using System.Net;

namespace Bendyline.Base
{
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

    public class PropertyChangedEventArgs : EventArgs
    {
        private String propertyName;

        public String PropertyName
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        public PropertyChangedEventArgs(String propertyName)
        {
            this.propertyName = propertyName;
        }       
    }
}
