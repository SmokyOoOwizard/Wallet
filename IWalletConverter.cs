using System.Collections.Generic;
using System.IO;

namespace Wallet
{
    public interface IWalletConverter
    {
        void Serialize(Dictionary<string, int> currency, Stream storage);
        void Deserialize(Dictionary<string, int> currency, Stream storage);
    }
}
