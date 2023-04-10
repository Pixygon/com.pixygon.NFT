using Pixygon.NFT.Wax;
using Pixygon.NFT.Eth;
using Pixygon.NFT.Tez;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Pixygon.NFT {
    public class NFT : MonoBehaviour {
        public delegate void OnFinish(NftTemplateObject[] assets);
        public delegate void OnSuccess();
        public delegate void OnSuccessString(string s);
        public delegate void OnFail();
        public delegate void OnBalanceGet(string s);

        private static string _account = string.Empty;

        /*
        private static string Account { get {
                if(_account == string.Empty)
                    _account = PlayerPrefs.GetString("WaxWallet");
                return _account;
            }
        }
        */

        public static async void FetchAllAssetsInWallet(Chain chain, OnFinish finish, string wallet) {
            switch(chain) {
                case Chain.Wax:
                    finish?.Invoke(await WaxNFT.FetchAllAssetsInWallet(wallet));
                    break;
                case Chain.EOS:
                    break;
                case Chain.Ethereum:
                    finish?.Invoke(await EthNFT.FetchAllAssetsInWallet(wallet));
                    break;
                case Chain.Tezos:
                    finish?.Invoke(await TezNFT.FetchAllAssetsInWallet(wallet));
                    break;
                case Chain.Polygon:
                    break;
                case Chain.Polkadot:
                    break;
                case Chain.Elrond:
                    break;
                case Chain.BinanceChain:
                    break;
                case Chain.Cardano:
                    break;
                case Chain.Stellar:
                    break;
                case Chain.Neo:
                    break;
                case Chain.HyperledgerFabric:
                    break;
                case Chain.Waves:
                    break;
                case Chain.Cosmos:
                    break;
                case Chain.Ripple:
                    break;
                case Chain.Nem:
                    break;
                case Chain.Solana:
                    break;
                case Chain.Hive:
                    break;
                case Chain.Phantom:
                    break;
                case Chain.Flow:
                    break;
            }
        }
        public static async void FetchAllAssets(Chain chain, OnFinish finish, string collectionFilter = "") {
            switch(chain) {
                case Chain.Wax:
                    finish?.Invoke(await WaxNFT.FetchAllAssets(collectionFilter));
                    break;
                case Chain.EOS:
                    break;
                case Chain.Ethereum:
                    finish?.Invoke(await EthNFT.FetchAllAssets(collectionFilter));
                    break;
                case Chain.Tezos:
                    finish?.Invoke(await TezNFT.FetchAllAssets(collectionFilter));
                    break;
                case Chain.Polygon:
                    break;
                case Chain.Polkadot:
                    break;
                case Chain.Elrond:
                    break;
                case Chain.BinanceChain:
                    break;
                case Chain.Cardano:
                    break;
                case Chain.Stellar:
                    break;
                case Chain.Neo:
                    break;
                case Chain.HyperledgerFabric:
                    break;
                case Chain.Waves:
                    break;
                case Chain.Cosmos:
                    break;
                case Chain.Ripple:
                    break;
                case Chain.Nem:
                    break;
                case Chain.Solana:
                    break;
                case Chain.Hive:
                    break;
                case Chain.Phantom:
                    break;
                case Chain.Flow:
                    break;
            }
        }
        
        
        /// <summary>
        /// Invokes 'finish' method and returns templates found
        /// </summary>
        /// <param name="info"></param>
        /// <param name="finish"></param>
        public static async void FetchAssets(NFTTemplateInfo info, OnFinish finish) {
            switch(info.chain) {
                case Chain.Wax:
                finish?.Invoke(await WaxNFT.FetchAssets(info));
                break;
                case Chain.EOS:
                finish?.Invoke(await OtherNFT.FetchAssets(info));
                break;
                case Chain.Ethereum:
                finish?.Invoke(await EthNFT.FetchAssets(info));
                break;
                case Chain.Tezos:
                finish?.Invoke(await TezNFT.FetchAssets(info));
                break;
                case Chain.Polygon:
                finish?.Invoke(await OtherNFT.FetchAssets(info));
                break;
                case Chain.Polkadot:
                break;
                case Chain.Elrond:
                break;
                case Chain.BinanceChain:
                break;
                case Chain.Cardano:
                break;
                case Chain.Stellar:
                break;
                case Chain.Neo:
                break;
                case Chain.HyperledgerFabric:
                break;
                case Chain.Waves:
                break;
                case Chain.Cosmos:
                break;
                case Chain.Ripple:
                break;
                case Chain.Nem:
                break;
                case Chain.Solana:
                finish?.Invoke(await OtherNFT.FetchAssets(info));
                break;
                case Chain.Flow:
                finish?.Invoke(await OtherNFT.FetchAssets(info));
                break;
                case Chain.Hive:
                    break;
                case Chain.Phantom:
                    break;
            }
        }
        public static async Task<NftTemplateObject[]> FetchAssets(NFTTemplateInfo info) {
            switch(info.chain) {
                case Chain.Wax:
                return await WaxNFT.FetchAssets(info);
                case Chain.EOS:
                break;
                case Chain.Ethereum:
                    return await EthNFT.FetchAssets(info);
                break;
                case Chain.Tezos:
                    return await TezNFT.FetchAssets(info);
                break;
                case Chain.Polygon:
                break;
                case Chain.Polkadot:
                break;
                case Chain.Elrond:
                break;
                case Chain.BinanceChain:
                break;
                case Chain.Cardano:
                break;
                case Chain.Stellar:
                break;
                case Chain.Neo:
                break;
                case Chain.HyperledgerFabric:
                break;
                case Chain.Waves:
                break;
                case Chain.Cosmos:
                break;
                case Chain.Ripple:
                break;
                case Chain.Nem:
                break;
            }
            return null;
        }
        /// <summary>
        /// Returns true if the account owns the template, and invokes success or failed actions, if supplied
        /// </summary>
        /// <param name="info">The required template</param>
        /// <param name="success">Action if successful</param>
        /// <param name="failed">Action if failed</param>
        /// <returns></returns>
        public static async Task<bool> ValidateTemplate(NFTTemplateInfo info, OnSuccess success = null, OnFail failed = null) {
            var owned = false;
            switch(info.chain) {
                case Chain.Wax:
                owned = await WaxNFT.ValidateTemplate(info);
                break;
                case Chain.EOS:
                break;
                case Chain.Ethereum:
                    owned = await EthNFT.ValidateTemplate(info);
                break;
                case Chain.Tezos:
                    owned = await TezNFT.ValidateTemplate(info);
                break;
                case Chain.Polygon:
                break;
                case Chain.Polkadot:
                break;
                case Chain.Elrond:
                break;
                case Chain.BinanceChain:
                break;
                case Chain.Cardano:
                break;
                case Chain.Stellar:
                break;
                case Chain.Neo:
                break;
                case Chain.HyperledgerFabric:
                break;
                case Chain.Waves:
                break;
                case Chain.Cosmos:
                break;
                case Chain.Ripple:
                break;
                case Chain.Nem:
                break;
                case Chain.Solana:
                    break;
                case Chain.Hive:
                    break;
                case Chain.Phantom:
                    break;
                case Chain.Flow:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if(owned)
                success?.Invoke();
            else
                failed?.Invoke();

            return owned;

        }

        public static async Task<bool> ValidateTemplate(NFTTemplateInfo info, OnSuccessString success = null, OnFail failed = null, string verification = "") {
            var owned = false;
            switch(info.chain) {
                case Chain.Wax:
                    owned = await WaxNFT.ValidateTemplate(info);
                    break;
                case Chain.EOS:
                    break;
                case Chain.Ethereum:
                    owned = await EthNFT.ValidateTemplate(info);
                    break;
                case Chain.Tezos:
                    owned = await TezNFT.ValidateTemplate(info);
                    break;
                case Chain.Polygon:
                    break;
                case Chain.Polkadot:
                    break;
                case Chain.Elrond:
                    break;
                case Chain.BinanceChain:
                    break;
                case Chain.Cardano:
                    break;
                case Chain.Stellar:
                    break;
                case Chain.Neo:
                    break;
                case Chain.HyperledgerFabric:
                    break;
                case Chain.Waves:
                    break;
                case Chain.Cosmos:
                    break;
                case Chain.Ripple:
                    break;
                case Chain.Nem:
                    break;
                case Chain.Solana:
                    break;
                case Chain.Hive:
                    break;
                case Chain.Phantom:
                    break;
                case Chain.Flow:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if(owned)
                success?.Invoke(verification);
            else
                failed?.Invoke();

            return owned;

        }

        public static async Task<float> FetchCoinPrice(Chain chain) {
            //https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids=wax
            //https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids=ethereum
            //https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids=tezos
            var www = UnityWebRequest.Get($"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids={chain.ToString().ToLower()}");
            www.SetRequestHeader("Content-Type", "application/json");
            www.SendWebRequest();
            while (!www.isDone)
                await Task.Yield();
            if (www.error != null) {
                Debug.Log(www.error);
                www.Dispose();
                return 0f;
            }
            Debug.Log(www.downloadHandler.text);
            var price = JsonUtility.FromJson<CoinGeckoCoinValue[]>(www.downloadHandler.text);
            www.Dispose();
            return (float)price[0].current_price;
        }


        public static async Task<NftTemplateObject> GetTemplate(int template) {
            return await WaxNFT.GetTemplate(template);
        }
        public static async Task<float> GetBalance(Chain chain) {
            switch (chain) {
                case Chain.Ethereum:
                    return await EthNFT.GetBalance();
                    break;
                case Chain.Wax:
                    return await WaxNFT.GetBalance();
                    break;
                case Chain.Tezos:
                    break;
            }

            return 0f;
        }
        public static void OpenOnAtomic(string collectionTitle, int template) {
            Application.OpenURL($"https://wax.atomichub.io/explorer/template/{collectionTitle}/{template}");
        }
        public static void OpenAssetOnAtomic(string assetID) {
            Application.OpenURL($"https://wax.atomichub.io/explorer/asset/{assetID}");
        }
        public static void OpenCollectionOnAtomic(string collectionTitle) {
            Application.OpenURL($"https://wax.atomichub.io/explorer/collection/{collectionTitle}");
        }
        public static void OpenOnAtomicMarket(string collectionTitle, int template) {
            Application.OpenURL(
                $"https://wax.atomichub.io/market?collection_name={collectionTitle}&template_id={template}");
        }

        public static async Task<Sprite> GetImageFromIpfs(string hash) {
            var www = UnityWebRequestTexture.GetTexture($"https://ipfs.atomichub.io/ipfs/{hash}");
            www.SendWebRequest();
            while(!www.isDone)
                await Task.Yield();
            if (www.error != null) return null;
            var t = DownloadHandlerTexture.GetContent(www);
            return Sprite.Create(t, new Rect(0f, 0f, t.width, t.height), new Vector2(.5f, .5f));
        }

    }
    [Serializable]
    public class AssetData
    {
        public string assetID;
        public string owner;
        public int mint;
        public int issuedSupply;
        public int maxSupply;

        public AssetData(string assetID, string owner, int mint, int issuedSupply, int maxSupply) {
            this.assetID = assetID;
            this.owner = owner;
            this.mint = mint;
            this.issuedSupply = issuedSupply;
            this.maxSupply = maxSupply;
        }
    }
    [Serializable]
    public class response
    {
        public bool success;
        public waxAssetData[] data;
        public int query_time;
    }
    [Serializable]
    public class ErrorResponse
    {
        public bool success;
        public string message;
    }

    [Serializable]
    public class PostData {
        public bool json;
        public string code;
        public string table;
        public string scope;
        //public string index_position;
        //public string key_type;
        //public string encode_type;
        public string lower_bound;
        public string upper_bound;
        public int limit;
        //public bool reverse;
        //public bool show_payer;
    }

    [Serializable]
    public class AccountResult {
        public rows[] rows;
        public bool more;
        public string next_key;
    }

    [Serializable]
    public class rows {
        public string balance;
    }
}

/*
    {
    "id": 6456092,
    "steam_id": null,
    "username": "",
    "display_name": "User 6456092",
    "email": "user@mail.com",
    "first_name": "WAX",
    "last_name": "User",
    "social_accounts": [],
    "tos_accepted": true,
    "age_accepted": true,
    "is_banned": false,
    "additional_authentication": {
        "enabled": true,
        "type": "Google 2FA",
        "type_id": 1,
        "enabled_at": "2019-09-23 15:55:39"
    },
    "address": {
        "address1": "",
        "address2": "",
        "address3": "",
        "city": "",
        "locality": "",
        "postal_code": "",
        "country": ""
    },
    "wcw": {
        "account_name": "3m1q4.wam",
        "public_keys": [
            "EOS6rjGKGYPBmVGsDDFAbM6UT5wQ9szB9m2fEcqHFMMcPge983xz9",
            "EOS7wTCoctybwrQWuE2tWYGwdLEGRXE9rrzALeBLUhWfbHXysFr9W"
        ]
    }
    */