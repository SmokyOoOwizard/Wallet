using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Wallet
{
    public sealed class BinaryWalletConverter : IWalletConverter
    {
        public void Deserialize(Dictionary<string, int> currency, Stream storage)
        {
            if (currency != null)
            {
                BinaryFormatter bf = new BinaryFormatter();

                var deserializedCurrency = bf.Deserialize(storage) as Dictionary<string, int>;

                if (deserializedCurrency != null)
                {
                    foreach (var c in deserializedCurrency)
                    {
                        currency[c.Key] = c.Value;
                    }
                }
            }
        }

        public void Serialize(Dictionary<string, int> currency, Stream storage)
        {
            if (currency != null)
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(storage, currency);
            }
        }
    }
}
