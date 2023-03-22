using Pixygon.NFT.Wax;
using Pixygon.NFT.Eth;
using Pixygon.NFT.Tez;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Pixygon.NFT {
    public class NFT : MonoBehaviour {
        public delegate void OnFinish(waxAsset[] assets);
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
        public static async Task<waxAsset[]> FetchAssets(NFTTemplateInfo info) {
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

        public static async Task<bool> ValidateTemplate(NFTTemplateInfo info, OnSuccessString success = null, OnFail failed = null) {
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
                success?.Invoke(info.collection + info.schema + info.template);
            else
                failed?.Invoke();

            return owned;

        }

        public static async Task<waxAssetData> GetTemplate(int template) {
            return await WaxNFT.GetTemplate(template);
        }
        public static async void GetBalance(string symbol = "WAX", OnBalanceGet success = null, string code = "eosio.token") {
            success?.Invoke(await WaxNFT.GetBalance(symbol, code));
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
    public class NFTAsset {
        public string collection;
        public int template;
        public NFTData Data;
        public waxAsset WaxData;
        public AssetData Asset;
    }
    [Serializable]
    public class waxAsset {
        public string name;
        public string collectionName;
        public string ipfs;
        public string description;
        //public string chain;
        //public int templateID;
        //public string schemaID;
        public NFTTemplateInfo templateInfo;
        public AssetData[] assets;
        
        public string DisplayInfo() {
            var nftDataText = new StringBuilder();
            //nftDataText.Append("Type: ");
            //nftDataText.Append(type);
            //nftDataText.Append("\nAction: ");
            //nftDataText.Append(nftAction);
            nftDataText.Append("\nTemplateID: ");
            nftDataText.Append(templateInfo.template);
            return nftDataText.ToString();
        }

        /*
        public NFTTemplateInfo TemplateInfo() {
            NFTTemplateInfo info = new NFTTemplateInfo();
            info.chain = chain;
            info.collection = collectionName;
            info.schema = schemaID;
            info.template = templateID;
            return info;
        }
        */
        public NFTType type;
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
    public class waxAssetData
    {
        public string contract;
        public string asset_id;
        public string owner;
        public string name;
        public bool is_transferable;
        public bool is_burnable;
        public collection collection;
        public schema schema;
        public template template;
        //public backed_tokens[] backed_tokens;
        public mutable_data mutable_data;
        public immutable_data immutable_data;
        public int template_mint;

        public string burned_by_account;
        public string burned_at_block;
        public string burned_at_time;
        //public string updated_at_block;
        //public string updated_at_time;
        //public string transferred_at_block;
        //public string transferred_at_time;
        //public string minted_at_block;
        //public string minted_at_time;
        public nftdata data;
        public override string ToString() {
            return string.Format(
                "Contract: {0}\nAssetID: {1}\nOwner: {2}\nName: {3}\nTemplate: {4}",
                contract, asset_id, owner, name, template.template_id);
        }
    }
    [Serializable]
    public class immutable_data {
    }
    [Serializable]
    public class mutable_data {

    }

    [Serializable]
    public class nftdata {
        public string img;
        public string Info;
        public string name;
        public string Description;
    }
    [Serializable]
    public class collection
    {
        public string collection_name;
        public string name;
        public string author;
        public bool allow_notify;
        public string[] authorized_accounts;
        public string[] notify_accounts;
        public int market_fee;
        public string created_at_block;
        public string created_at_time;
    }
    [Serializable]
    public class schema
    {
        public string schema_name;
        
        /*
        "format": [
          {
            "name": "string",
            "type": "string"
          }
        ],
        "created_at_block": "string",
        "created_at_time": "string"
        */
    }
    [Serializable]
    public class template
    {
        public int template_id;
        public int max_supply;
        public bool is_transferable;
        public bool is_burnable;
        public int issued_supply;
        public mutable_data mutable_data;
        public immutable_data immutable_data;
        public string created_at_time;
        public string created_at_block;
    }
    public class backed_tokens
    {
        /*
        "token_contract": "string",
          "token_symbol": "string",
          "token_precision": 0,
          "amount": "string"
        */
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