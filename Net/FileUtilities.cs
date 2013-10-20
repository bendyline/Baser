/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

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

        public static String EnsurePath(String folderPath, String fileName)
        {
            return FileUtilities.EnsurePathEndsWithBackSlash(folderPath) + fileName;
        }

        public static String GetBaseFileName(String fileName)
        {
            int lastPeriod = fileName.LastIndexOf(".");
            if (lastPeriod > 0)
            {
                fileName = fileName.Substring(0, lastPeriod);
            }

            int lastBackSlash = fileName.LastIndexOf("\\");
            if (lastBackSlash > 0)
            {
                fileName = fileName.Substring(lastBackSlash + 1);
            }

            return fileName;
        }

        public static String GetTextFromFile(String filePath)
        {
            String result = null;

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    result = sr.ReadToEnd();
                }
            }

            return result;
        }

        public static void SetTextToFile(String filePath, String text)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(text);
                }
            }
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
