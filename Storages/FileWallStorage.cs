using System.Collections.Generic;
using System;
using System.IO;

namespace Wallet
{
    public sealed class FileWallStorage : IWalletStorage
    {

        public string FilePath { get; private set; }

        private IWalletConverter converter;

        private FileWallStorage()
        {

        }

        public static FileWallStorage CreateStorage(string path, IWalletConverter converter)
        {
            if (string.IsNullOrEmpty(path) || converter == null)
            {
                return null;
            }

            var storage = new FileWallStorage();

            storage.converter = converter;
            storage.FilePath = path;

            return storage;
        }

        public void Load(Dictionary<string, int> currency)
        {
            if (currency != null)
            {
                if (File.Exists(FilePath))
                {
                    using (var fileAccess = File.OpenRead(FilePath))
                    {
                        converter.Deserialize(currency, fileAccess);
                    }
                }
            }
        }

        public void Save(Dictionary<string, int> currency)
        {
            if (currency != null)
            {
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }

                var dirPath = Path.GetDirectoryName(FilePath);
                if (!string.IsNullOrEmpty(dirPath))
                {
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                }

                using (var fileAccess = File.Open(FilePath, FileMode.Create, FileAccess.Write))
                {
                    converter.Serialize(currency, fileAccess);
                }
            }
        }
    }
}
