using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Wallet
{
    public sealed class Wallet
    {
        /// <summary>
        /// key - currency name
        /// value - currency value
        /// </summary>
        private readonly Dictionary<string, int> currency = new Dictionary<string, int>();

        private IWalletStorage defaultStorage;

        public string[] GetCurrencyNames()
        {
            return currency.Keys.ToArray();
        }
        public KeyValuePair<string, int>[] GetAllCurrency()
        {
            return currency.ToArray();
        }

        public bool ExistsCurrency(string name)
        {
            return currency.ContainsKey(name);
        }
        public int GetCurrency(string name)
        {
            int value = 0;
            currency.TryGetValue(name, out value);
            return value;
        }
        public void SetCurrency(string name, int value)
        {
            currency[name] = value;
        }

        public void SaveCurrency()
        {
            if (defaultStorage != null)
            {
                defaultStorage.Save(currency);
            }
        }
        public void SaveCurrency(IWalletStorage storage)
        {
            storage.Save(currency);
        }

        public void ResetCurrency()
        {
            currency.Clear();
            if (defaultStorage != null)
            {
                defaultStorage.Load(currency);
            }
        }
        public void ResetCurrency(IWalletStorage storage)
        {
            currency.Clear();
            storage.Load(currency);
        }

        public static Wallet CreateWallet(IWalletStorage defaultStorage)
        {
            var wallet = new Wallet();
            wallet.defaultStorage = defaultStorage;

            wallet.ResetCurrency();

            return wallet;
        }
    }
}
