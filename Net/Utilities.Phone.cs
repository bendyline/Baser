using System;
using System.Reflection;
using System.IO;
using System.Xml;

namespace Bendyline.Base
{
    public static partial class Utilities
    {

        public static String SharePointClientAssemblyName
        {
            get
            {
                return "Bendyline.Base.SharePointClient" + Utilities.AssemblyDetails;
            }
        }

        public static String SharePointClientTypeSuffix
        {
            get
            {
                return ", " + SharePointClientAssemblyName;
            }
        }


        public static String AssemblyDetails
        {
            get
            {
                return "";
            }
        }

        public static bool IsPhone
        {
            get
            {
                return true;
            }
        }

        public static XmlReaderResource LoadFromResource(Assembly assembly, String filePath)
        {
            Stream s = assembly.GetManifestResourceStream(filePath);

            if (s == null)
            {
                throw new FileNotFoundException("Could not find assembly resource " + filePath + "");
            }

            XmlReader xr = XmlReader.Create(s);
            XmlUtilities.ReadToInterior(0, xr);

            XmlReaderResource xrr = new XmlReaderResource(s, xr);

            return xrr;
        }

    }
}
