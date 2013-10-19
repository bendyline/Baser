using System;
using System.IO;
using Windows.Storage;
using Windows.Storage.BulkAccess;

namespace Bendyline.Base
{
    public class StorageManager
    {
        private static StorageFolder bendylineDirectory;
        private static StorageFolder isoStoreRootDirectory;
        private static StorageManager current;
        private static String isoStoreRootPath;
        private static String bendylinePath;

        private StorageGroup rootGroup;


        public static StorageFolder IsoStoreRootDirectory
        {
            get
            {
                if (isoStoreRootDirectory == null)
                {
                    EnsureRootDirectory();
                }

                return isoStoreRootDirectory;
            }
        }

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
            this.rootGroup = new StorageGroup(IsoStoreRootDirectory);

            this.rootGroup.Name = string.Empty;
        }

        private static void EnsureRootDirectory()
        {
            isoStoreRootDirectory = Windows.Storage.ApplicationData.Current.LocalFolder;
            /*
            if (!isoStoreRootDirectory.Exists)
            {
                isoStoreRootDirectory.Create();
            }*/
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
    }
}
