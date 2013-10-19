using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace Bendyline.Base
{
    public class StorageManager
    {
        private static DirectoryInfo bendylineDirectory;
        private static DirectoryInfo isoStoreRootDirectory;
        private static StorageManager current;
        private static String isoStoreRootPath;
        private static String bendylinePath;

        private StorageGroup rootGroup;

        
        public static String BendylinePath
        {
            get
            {
                if (bendylinePath == null)
                {
                    bendylinePath = FileUtilities.EnsurePathEndsWithBackSlash(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + "Bendyline\\";
                }

                return bendylinePath;
            }
        }

        public static String IsoStoreRootPath
        {
            get
            {
                if (isoStoreRootPath == null)
                {
                    isoStoreRootPath = BendylinePath + "IsoStore\\";
                }

                return isoStoreRootPath;
            }
        }

        public static DirectoryInfo BendylineDirectory
        {
            get
            {
                if (bendylineDirectory == null)
                {
                    EnsureBendylineDirectory();
                }

                return bendylineDirectory;
            }
        }

        public static DirectoryInfo IsoStoreRootDirectory
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

        private static void EnsureBendylineDirectory()
        {
            bendylineDirectory = new DirectoryInfo(BendylinePath);

            if (!bendylineDirectory.Exists)
            {
                bendylineDirectory.Create();
            }
        }

        private static void EnsureRootDirectory()
        {
            EnsureBendylineDirectory();

            isoStoreRootDirectory = new DirectoryInfo(IsoStoreRootPath);

            if (!isoStoreRootDirectory.Exists)
            {
                isoStoreRootDirectory.Create();
            }
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
