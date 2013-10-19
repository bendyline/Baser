using System;
using System.IO.IsolatedStorage;
using System.IO;

namespace Bendyline.Base
{
    public class StorageFile
    {
        private StorageGroup parentGroup;
        private String name;
        private FileInfo file;

        public String Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        public String Path
        {
            get
            {
                return FileUtilities.EnsurePathEndsWithBackSlash(this.parentGroup.Name) + this.name;
            }
        }

        public StorageGroup ParentGroup
        {
            get
            {
                return this.parentGroup;
            }

            set
            {
                this.parentGroup = value;
            }
        }

        public StorageFile(FileInfo file, StorageGroup parentGroup)
        {
            this.file = file;
            this.parentGroup = parentGroup;
        }

        public void SetText(String text)
        {
            if (!this.file.Exists)
            {
                using (FileStream s = this.file.Create())
                {
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write(text);
                    }
                }
            }
            else
            {
                using (FileStream s = this.file.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write(text);
                    }
                }
            }
        }


        public String GetText()
        {
            if (!this.file.Exists)
            {
                return null;
            }

            using (FileStream s = this.file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
