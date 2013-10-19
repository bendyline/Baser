using System;
using System.Xml;

namespace Bendyline.Base
{
    public interface ISerializableObject
    {
        String Xml { get; set; }

        void ReadXml(XmlReader reader);
        void WriteXml(XmlWriter writer);
        void WriteXmlWithTagName(XmlWriter writer, String tagName);
    }
}
