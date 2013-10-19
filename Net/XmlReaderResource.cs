using System;

using System.IO;
using System.Xml;

namespace Bendyline.Base
{
    public class XmlReaderResource : IDisposable
    {
        private Stream resourceHost;
        private XmlReader reader;

        public Stream ResourceHost
        {
            get
            {
                return this.resourceHost;
            }

            set
            {
                this.resourceHost = value;
            }
        }

        public XmlReader Reader
        {
            get
            {
                return this.reader;
            }

            set
            {
                this.reader = value;
            }
        }

        public void Dispose()
        {
           // this.reader.Dispose(); 
            this.resourceHost.Dispose();
        }

        public XmlReaderResource(Stream s, XmlReader reader)
        {
            this.reader = reader;
            this.resourceHost = s;
        }

    }
}
