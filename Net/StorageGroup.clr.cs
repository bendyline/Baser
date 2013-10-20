/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;

namespace Bendyline.Base
{
    public class StorageGroup
    {
        private readonly Dictionary<String, StorageFile> filesByName;
        private readonly List<StorageFile> files;
        private readonly Dictionary<String, StorageGroup> groupsByName;
        private readonly List<StorageGroup> groups;
        private DirectoryInfo directory;
        private String name;
        private StorageGroup parentGroup;
        private bool loadedGroupNames;
        private bool loadedFileNames;

        public IEnumerable<StorageGroup> Groups
        {
            get
            {
                this.EnsureGroupsLoaded();

                return this.groups;
            }
        }

        public IEnumerable<StorageFile> Files
        {
            get
            {
                this.EnsureFilesLoaded();

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
                    return FileUtilities.EnsurePathEndsWithBackSlash(this.parentGroup.Path + this.Name);
                }

                return String.Empty;
            }
        }

        internal StorageGroup(DirectoryInfo directory)
        {
            this.filesByName = new Dictionary<string, StorageFile>();
            this.files = new List<StorageFile>();
            this.groupsByName = new Dictionary<string, StorageGroup>();
            this.groups = new List<StorageGroup>();
            this.directory = directory;
        }

        private void EnsureGroupsLoaded()
        {
            if (!loadedGroupNames)
            {
                DirectoryInfo[] directories = this.directory.GetDirectories();

                foreach (DirectoryInfo directory in directories)
                {
                    this.EnsureGroupByDirectoryInfo(directory);
                }

                this.loadedGroupNames = true;
            }
        }

        private void EnsureFilesLoaded()
        {
            if (!loadedFileNames)
            {
                FileInfo[] files = this.directory.GetFiles();

                foreach (FileInfo file in files)
                {
                    this.EnsureFileByFileInfo(file);
                }

                this.loadedFileNames = true;
            }
        }

        public StorageGroup GetGroup(String name)
        {
            this.EnsureGroupsLoaded();

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
            name = name.ToLower();

            foreach (StorageFile storageFile in this.Files)
            {
                String targetName = storageFile.Name.ToLower();

                if (targetName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public StorageFile GetFile(String name)
        {
            name = name.ToLower(); 
            
            foreach (StorageFile storageFile in this.Files)
            {
                String targetName = storageFile.Name.ToLower();

                if (targetName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
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

        public StorageFile EnsureFile(String fileName)
        {
            String fileNameLower = fileName.ToLower();

            this.EnsureFilesLoaded();

            if (this.filesByName.ContainsKey(fileNameLower))
            {
                return this.filesByName[fileNameLower];
            }

            FileInfo fi = new FileInfo(FileUtilities.EnsurePathEndsWithBackSlash(this.directory.FullName) + fileName);
            
            fi.Create();

            StorageFile sf = new StorageFile(fi, this);

            sf.Name = name;
            this.filesByName.Add(fileName, sf);
            this.files.Add(sf);

            return sf;
        }

        public StorageFile EnsureFileByFileInfo(FileInfo fileInfo)
        {
            String fileName = fileInfo.Name.ToLower();

            if (this.filesByName.ContainsKey(fileName))
            {
                return this.filesByName[fileName];
            }

            StorageFile sf = new StorageFile(fileInfo, this);
            sf.Name = fileName;

            this.filesByName.Add(fileName, sf);
            this.files.Add(sf);

            return sf;
        }

        public StorageGroup EnsureGroup(String name)
        {
            name = name.ToLower();

            this.EnsureGroupsLoaded();

            if (this.groupsByName.ContainsKey(name))
            {
                return this.groupsByName[name];
            }

            DirectoryInfo di = this.directory.CreateSubdirectory(name);

            StorageGroup sg = new StorageGroup(di);
            sg.Name = name;
            sg.ParentGroup = this;

            this.groupsByName.Add(name, sg);
            this.groups.Add(sg);

            return sg;
        }

        public StorageGroup EnsureGroupByDirectoryInfo(DirectoryInfo directoryInfo)
        {
            if (this.groupsByName.ContainsKey(directoryInfo.Name.ToLower()))
            {
                return this.groupsByName[directoryInfo.Name.ToLower()];
            }

            /*if (!this.isoFile.DirectoryExists(name))
            {
                this.isoFile.CreateDirectory(name);
            }*/

            String nameLower = directoryInfo.Name.ToLower();

            StorageGroup sg = new StorageGroup(directoryInfo);
            sg.Name = nameLower;            
            sg.ParentGroup = this;

            this.groupsByName.Add(nameLower, sg);
            this.groups.Add(sg);

            return sg;
        }
    }
}
