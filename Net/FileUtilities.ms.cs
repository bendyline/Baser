using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bendyline.Base
{
    public static class FileUtilities
    {
        public static String GetDirectoryPathFromFilePath(String filePath)
        {
            int lastSlash = filePath.LastIndexOf("\\");

            if (lastSlash >= 0)
            {
                return filePath.Substring(0, lastSlash + 1);
            }

            return filePath;
        }

        public static String GetTextFromFile(String filePath)
        {
            String result = null;

            return result;
        }

        public static void SetTextToFile(String filePath, String text)
        {
            
        }

        public static String EnsurePathDoesNotEndWithBackSlash(String path)
        {
            if (path.EndsWith("\\"))
            {
                path = path.Substring(0, path.Length - 1);
            }

            return path;
        }

        public static String GetCanonicalPath(String path)
        {
            return path.ToLower();
        }

        public static String EnsurePathEndsWithBackSlash(String path)
        {
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }

            return path;
        }


        public static String EnsurePathDoesNotStartWithBackSlash(String path)
        {
            if (path.StartsWith("\\"))
            {
                path = path.Substring(1);
            }

            return path;
        }

        public static String EnsurePathStartsWithBackSlash(String path)
        {
            if (!path.StartsWith("\\"))
            {
                path = "\\" + path;
            }

            return path;
        }

    }
}
