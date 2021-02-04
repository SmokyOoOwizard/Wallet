using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

namespace Wallet
{

    public sealed class PlayerPrefsWalletStorage : IWalletStorage
    {
        public const string DEFAULT_NAME = "Wallet";

        public string StorageName { get; private set; }

        private IWalletConverter converter;

        private PlayerPrefsWalletStorage()
        {

        }

        public static PlayerPrefsWalletStorage CreateStorage(string name, IWalletConverter converter)
        {
            if (string.IsNullOrEmpty(name) || converter == null)
            {
                return null;
            }

            var storage = new PlayerPrefsWalletStorage();
            storage.StorageName = name;
            storage.converter = converter;

            return storage;
        }

        public void Load(Dictionary<string, int> currency)
        {
            if (currency != null)
            {
                if (PlayerPrefs.HasKey(StorageName))
                {
                    var serializedCurrency = PlayerPrefs.GetString(StorageName);
                    if (string.IsNullOrEmpty(serializedCurrency))
                    {
                        return;
                    }

                    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(serializedCurrency)))
                    {
                        ms.Seek(0, SeekOrigin.Begin);

                        converter.Deserialize(currency, ms);
                    }
                }
            }
        }

        public void Save(Dictionary<string, int> currency)
        {
            if (currency != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    converter.Serialize(currency, ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    using (TextReader tr = new StreamReader(ms, Encoding.UTF8))
                    {
                        PlayerPrefs.SetString(StorageName, tr.ReadToEnd());
                        PlayerPrefs.Save();
                    }
                }
            }
        }
    }
}
