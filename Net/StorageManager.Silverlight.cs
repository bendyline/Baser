using System;
using System.IO.IsolatedStorage;

namespace Bendyline.Base
{
    public class StorageManager
    {
        private IsolatedStorageFile rootIsoFile;
        private static StorageManager current;
        private StorageGroup rootGroup;

        public static StorageManager Current
        {
            get
            {
                if (current == null)
                {
                    current = new StorageManager();
                }

                return current;
            }
        }


        public StorageManager()
        {
            rootIsoFile = IsolatedStorageFile.GetUserStoreForApplication();

            this.rootGroup = new StorageGroup(rootIsoFile);

            this.rootGroup.Name = string.Empty;
        }

        public StorageFile GetFileFromPath(String path)
        {
            return this.rootGroup.GetFileFromPath(path);
        }

        public StorageGroup EnsureGroup(String name)
        {
            return this.rootGroup.EnsureGroup(name);
        }

        public StorageFile EnsureFile(String name)
        {
            return this.rootGroup.EnsureFile(name);
        }
        public static String EnsurePathEnded(String path)
        {
            if (path == String.Empty)
            {
                return String.Empty;
            }

            if (!path.EndsWith("/"))
            {
                path += "/";
            }

            return path;
        }

        public static String EnsurePathEndsWithSlash(String path)
        {
            if (!path.EndsWith("/"))
            {
                path += "/";
            }

            return path;
        }
    }
}
