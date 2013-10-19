using System;
using System.IO.IsolatedStorage;
using System.IO;

namespace Bendyline.Base
{
    public class StorageFile
    {
        private StorageGroup parentGroup;
        private String name;
        private IsolatedStorageFile isoFile;

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
                return StorageManager.EnsurePathEnded(this.parentGroup.Name) + this.name;
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

        public StorageFile(IsolatedStorageFile isoFile, StorageGroup parentGroup)
        {
            this.isoFile = isoFile;
            this.parentGroup = parentGroup;
        }

        public void SetText(String text)
        {
            if (!this.isoFile.FileExists(this.Path))
            {
                using (IsolatedStorageFileStream s = this.isoFile.CreateFile(this.Path))
                {
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write(text);
                    }
                }

            }
            else
            {
                using (IsolatedStorageFileStream s = this.isoFile.OpenFile(this.Path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
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
            if (!this.isoFile.FileExists(this.Path))
            {
                return null;
            }
            using (IsolatedStorageFileStream s = this.isoFile.OpenFile(this.Path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
