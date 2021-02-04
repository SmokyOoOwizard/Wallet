using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Wallet
{
    public sealed class JsonWalletConverter : IWalletConverter
    {
        public void Deserialize(Dictionary<string, int> currency, Stream storage)
        {
            if (currency != null)
            {
                var sr = new StreamReader(storage);

                var jr = new JsonTextReader(sr);

                bool success = true;
                var settings = new JsonSerializerSettings
                {
                    Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
                    MissingMemberHandling = MissingMemberHandling.Error
                };

                var serializer = JsonSerializer.Create(settings);

                var deserializedCurrency = serializer.Deserialize<Dictionary<string, int>>(jr);

                if (success && deserializedCurrency != null)
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
                var sw = new StreamWriter(storage);

                var jw = new JsonTextWriter(sw);

                var serializer = new JsonSerializer();

                serializer.Serialize(jw, currency);

                jw.Flush();
            }
        }
    }
}
