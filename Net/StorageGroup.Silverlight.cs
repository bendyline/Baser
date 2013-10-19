using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace Bendyline.Base
{
    public class StorageGroup
    {
        private readonly Dictionary<String, StorageFile> filesByName;
        private readonly List<StorageFile> files;
        private readonly Dictionary<String, StorageGroup> groupsByName;
        private readonly List<StorageGroup> groups;
        private IsolatedStorageFile isoFile;
        private String name;
        private StorageGroup parentGroup;
        private bool loadedGroupNames;
        private bool loadedFileNames;

        public IEnumerable<StorageGroup> Groups
        {
            get
            {
                if (!loadedGroupNames)
                {
                    foreach (String directoryName in this.isoFile.GetDirectoryNames())
                    {
                        this.EnsureGroup(directoryName);
                    }

                    this.loadedGroupNames = true;
                }

                return this.groups;
            }
        }

        public IEnumerable<StorageFile> Files
        {
            get
            {
                if (!loadedFileNames)
                {
                    foreach (String fileName in this.isoFile.GetFileNames(this.Name + "/*"))
                    {
                        this.EnsureFile(fileName);
                    }

                    this.loadedFileNames = true;
                }

                return this.files;
            }
        }

        public int FileCount
        {
            get
            {
                return ((List<StorageFile>)this.Files).Count;
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
                if (this.parentGroup != null)
                {
                    return StorageManager.EnsurePathEnded(this.parentGroup.Path + this.Name);
                }

                return String.Empty;
            }
        }

        internal StorageGroup(IsolatedStorageFile file)
        {
            this.filesByName = new Dictionary<string, StorageFile>();
            this.files = new List<StorageFile>();
            this.groupsByName = new Dictionary<string, StorageGroup>();
            this.groups = new List<StorageGroup>();
            this.isoFile = file;
        }

        public StorageGroup GetGroup(String name)
        {
            foreach (StorageGroup storageGroup in this.Groups)
            {
                if (storageGroup.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return storageGroup;
                }
            }

            return null;
        }

        public bool ContainsFile(String name)
        {
            foreach (StorageFile storageFile in this.Files)
            {
                if (storageFile.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public StorageFile GetFile(String name)
        {
            foreach (StorageFile storageFile in this.Files)
            {
                if (storageFile.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return storageFile;
                }
            }

            return null;
        }

        public StorageFile GetFileFromPath(String path)
        {
            int nextSlash = path.IndexOf("/");

            if (nextSlash >= 0)
            {
                StorageGroup sg = this.GetGroup(path.Substring(0, nextSlash));

                if (sg == null)
                {
                    return null;
                }

                return sg.GetFileFromPath(path.Substring(nextSlash + 1));
            }

            return this.GetFile(path);
        }

        public StorageFile EnsureFile(String name)
        {
            if (this.filesByName.ContainsKey(name))
            {
                return this.filesByName[name];
            }

            StorageFile sf = new StorageFile(this.isoFile, this);
            sf.Name = name;
            this.filesByName.Add(name, sf);
            this.files.Add(sf);

            return sf;
        }

        public StorageGroup EnsureGroup(String name)
        {
            if (this.groupsByName.ContainsKey(name))
            {
                return this.groupsByName[name];
            }

            if (!this.isoFile.DirectoryExists(name))
            {
                this.isoFile.CreateDirectory(name);
            }

            StorageGroup sg = new StorageGroup(this.isoFile);
            sg.Name = name;
            sg.ParentGroup = this;

            this.groupsByName.Add(name, sg);
            this.groups.Add(sg);

            return sg;
        }
    }
}
