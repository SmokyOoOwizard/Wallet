using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Wallet
{
    public interface IWalletStorage
    {
        void Save(Dictionary<string, int> currency);
        void Load(Dictionary<string, int> currency);
    }
}
