using System;
using System.Net;
using System.Xml;
using System.Text;

namespace Bendyline.Base
{
    public static class XmlUtilities
    {
        public static String StripWhiteSpace(ref String contentSource)
        {
            StringBuilder sb = new StringBuilder();

            int currentWhiteSpaceStart = 0;
            int nextLessThan = contentSource.IndexOf("<");

            while (nextLessThan >= 0)
            {
                int nextGreaterThan = contentSource.IndexOf(">", nextLessThan);

                if (nextGreaterThan > nextLessThan)
                {
                    nextGreaterThan++;

                    String whiteSpace = contentSource.Substring(currentWhiteSpaceStart, nextLessThan - currentWhiteSpaceStart);

                    if (!IsWhiteSpace(whiteSpace))
                    {
                        sb.Append(whiteSpace.Trim());
                    }

                    sb.Append(contentSource.Substring(nextLessThan, nextGreaterThan - nextLessThan));
                    currentWhiteSpaceStart = nextGreaterThan;
                    nextLessThan = contentSource.IndexOf("<", nextGreaterThan);
                }
                else
                {
                    nextLessThan = -1;
                }
            }

            String endWhiteSpace = contentSource.Substring(currentWhiteSpaceStart, contentSource.Length - currentWhiteSpaceStart);

            if (!IsWhiteSpace(endWhiteSpace))
            {
                sb.Append(endWhiteSpace.Trim());
            }

            return sb.ToString();
        }

        public static bool IsWhiteSpace(String content)
        {
            foreach (char c in content)
            {
                if (!Char.IsWhiteSpace(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static String GetTagInterior(ref String content, String tagName)
        {
            return GetTagInterior(ref content, tagName, 0);
        }

        public static String GetTagInterior(ref String content, String tagName, int index)
        {
            int indexSpace = content.IndexOf(String.Format("<{0} ", tagName), index, StringComparison.InvariantCultureIgnoreCase);
            int indexSign = content.IndexOf(String.Format("<{0}>", tagName), index, StringComparison.InvariantCultureIgnoreCase);

            // is a simple tag the first instance of this tag?  If so, use that as the start of our search.
            if (indexSign >= 0 && (indexSign < indexSpace || indexSpace < 0))
            {
                indexSpace = indexSign;
            }

            if (indexSpace < 0)
            {
                return null;
            }

            // find the start of the interior.
            int startOfInterior = content.IndexOf(">", indexSpace);

            if (startOfInterior < 0)
            {
                return null;
            }

            // if this is a singleton, e.g., <foo/> then our content is empty
            if (content[startOfInterior - 1] == '/')
            {
                return String.Empty;
            }

            // bump the index one to get us to the interior
            startOfInterior++;

            int nextStart = startOfInterior;

            // find the next intersting thing: either <foo or <foo> or </foo>
            indexSpace = content.IndexOf(String.Format("<{0} ", tagName), nextStart, StringComparison.InvariantCultureIgnoreCase);
            indexSign = content.IndexOf(String.Format("<{0}>", tagName), nextStart, StringComparison.InvariantCultureIgnoreCase);
            int endTag = content.IndexOf(String.Format("</{0}>", tagName), nextStart, StringComparison.InvariantCultureIgnoreCase);

            // keep a count of 
            int starts = 0;

            while (starts >= 0)
            {
                if (endTag < 0)
                {
                    return null;
                }

                if (indexSpace > 0 && indexSpace < endTag)
                {
                    starts++;
                    nextStart = indexSpace + 1;
                    indexSpace = content.IndexOf("<" + tagName + " ", nextStart, StringComparison.InvariantCultureIgnoreCase);
                }
                else if (indexSign > 0 && indexSign < endTag)
                {
                    starts++;
                    nextStart = indexSpace + 1;
                    indexSign = content.IndexOf("<" + tagName + ">", nextStart, StringComparison.InvariantCultureIgnoreCase);
                }
                else
                {
                    starts--;
                    nextStart = endTag + 1;
                    endTag = content.IndexOf("</" + tagName + ">", nextStart, StringComparison.InvariantCultureIgnoreCase);
                }
            }

            return content.Substring(startOfInterior, nextStart - (startOfInterior + 1));
        }

        public static void ReadToInterior(int initialLevel, XmlReader reader)
        {
            reader.Read();

            while (reader.NodeType == XmlNodeType.Comment || reader.NodeType == XmlNodeType.DocumentFragment
            || reader.NodeType == XmlNodeType.Document || reader.NodeType == XmlNodeType.Whitespace
            || reader.NodeType == XmlNodeType.XmlDeclaration || reader.NodeType == XmlNodeType.DocumentType
            || reader.NodeType == XmlNodeType.ProcessingInstruction || reader.NodeType == XmlNodeType.Notation
            || reader.NodeType == XmlNodeType.CDATA)
            {
                reader.Read();
            }
        }

        public static String DeEntitize(String xml)
        {
            xml = xml.Replace("&lt;", "<");
            xml = xml.Replace("&gt;", ">");
            xml = xml.Replace("&nbsp;", " ");
            xml = xml.Replace("&amp;", "&");

            return xml;
        }

        public static String Entitize(String xml)
        {
            xml = xml.Replace("&", "&amp;");
            xml = xml.Replace("<", "&lt;");
            xml = xml.Replace(">", "&gt;");

            return xml;
        }
    }
}
