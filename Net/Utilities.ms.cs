using System;
using System.Xml;
using System.IO;
using System.Reflection;

namespace Bendyline.Base
{
    public static partial class Utilities
    {
        public static String AssemblyDetails
        {
            get
            {
                return ", Version=1.0.0.0, Culture=neutral, PublicKeyToken=7791b3b09e7c4049";
            }
        }

        public static bool IsPhone
        {
            get
            {
                return false;
            }
        }

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

        public static String AssemblyName
        {
            get
            {
                return "Bendyline.Base" + Utilities.AssemblyDetails;
            }
        }

        public static String TypeSuffix
        {
            get
            {
                return ", " + AssemblyName;
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
