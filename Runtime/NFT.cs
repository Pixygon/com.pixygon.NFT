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
        public delegate void OnFinishCollection(collection collection);
        public delegate void OnFinishCollections(collection[] collection);
        public delegate void OnSuccess();
        public delegate void OnSuccessString(string s);
        public delegate void OnFail();
        public delegate void OnBalanceGet(string s);

        private static string _account = string.Empty;
        
        //ASSETS
        public static async void FetchAllAssets(Chain chain, OnFinish finish, string collectionFilter = "", string wallet = "", int page = 1, int limit = 250, bool descOrder = true, AssetSort assetSort = AssetSort.AssetId) {
            switch(chain) {
                case Chain.Wax:
                    finish?.Invoke(await WaxNFT.FetchAllAssets(collectionFilter, wallet, page, limit, descOrder, assetSort));
                    break;
                case Chain.Ethereum:
                    finish?.Invoke(await EthNFT.FetchAllAssets(collectionFilter, wallet));
                    break;
                case Chain.Tezos:
                    finish?.Invoke(await TezNFT.FetchAllAssets(collectionFilter, wallet));
                    break;
            }
        }
        public static async void FetchAssets(NFTTemplateInfo info, OnFinish finish) {
            switch(info.chain) {
                case Chain.Wax:
                finish?.Invoke(await WaxNFT.FetchAssets(info));
                break;
                case Chain.Ethereum:
                finish?.Invoke(await EthNFT.FetchAssets(info));
                break;
                case Chain.Tezos:
                finish?.Invoke(await TezNFT.FetchAssets(info));
                break;
            }
        }
        public static async Task<NftTemplateObject[]> FetchAssets(NFTTemplateInfo info) {
            return info.chain switch {
                Chain.Wax => await WaxNFT.FetchAssets(info),
                Chain.Ethereum => await EthNFT.FetchAssets(info),
                Chain.Tezos => await TezNFT.FetchAssets(info),
                _ => null
            };
        }

        public static async Task<int> GetTotalAssets(Chain chain, string wallet) {
            return chain switch {
                Chain.Wax => await WaxNFT.GetTotalAssets(wallet),
                _ => 0
            };
        }
        
        //TEMPLATES
        public static async void FetchAllTemplates(Chain chain, OnFinish finish, string collectionFilter = "", string wallet = "", int page = 1, int limit = 250) {
            switch(chain) {
                case Chain.Wax:
                    finish?.Invoke(await WaxNFT.FetchAllTemplates(collectionFilter, wallet, page, limit));
                    break;
                case Chain.EOS:
                    break;
                case Chain.Ethereum:
                    //finish?.Invoke(await EthNFT.FetchAllTemplates(collectionFilter, wallet));
                    break;
                case Chain.Tezos:
                    //finish?.Invoke(await TezNFT.FetchAllTemplates(collectionFilter, wallet));
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
        public static async void FetchAllCollectionTemplates(Chain chain, OnFinish finish, string collectionFilter, int page = 1, int limit = 250) {
            switch(chain) {
                case Chain.Wax:
                    finish?.Invoke(await WaxNFT.FetchAllCollectionTemplates(collectionFilter, page, limit));
                    break;
                case Chain.EOS:
                    break;
                case Chain.Ethereum:
                    //finish?.Invoke(await EthNFT.FetchAllTemplates(collectionFilter, wallet));
                    break;
                case Chain.Tezos:
                    //finish?.Invoke(await TezNFT.FetchAllTemplates(collectionFilter, wallet));
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
        
        //COLLECTIONS
        public static async void GetCollection(Chain chain, OnFinishCollection finish, string collection) {
            switch(chain) {
                case Chain.Wax:
                    finish?.Invoke(await WaxNFT.GetCollection(collection));
                    break;
                case Chain.EOS:
                    break;
                case Chain.Ethereum:
                    //finish?.Invoke(await EthNFT.GetCollection(collection));
                    break;
                case Chain.Tezos:
                    //finish?.Invoke(await TezNFT.GetCollection(collection));
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
        public static async void SearchCollection(Chain chain, OnFinishCollections finish, string collection) {
            switch(chain) {
                case Chain.Wax:
                    finish?.Invoke(await WaxNFT.SearchCollections(collection));
                    break;
                case Chain.EOS:
                    break;
                case Chain.Ethereum:
                    //finish?.Invoke(await EthNFT.GetCollection(collection));
                    break;
                case Chain.Tezos:
                    //finish?.Invoke(await TezNFT.GetCollection(collection));
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
            var owned = info.chain switch {
                Chain.Wax => await WaxNFT.ValidateTemplate(info),
                Chain.Ethereum => await EthNFT.ValidateTemplate(info),
                Chain.Tezos => await TezNFT.ValidateTemplate(info),
                _ => false
            };
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
            //https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids=matic-network
            var chainName = chain.ToString().ToLower();
            if (chain == Chain.Polygon)
                chainName = "matic-network";
            var www = UnityWebRequest.Get($"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids={chainName}");
            www.SetRequestHeader("Content-Type", "application/json");
            www.SendWebRequest();
            while (!www.isDone)
                await Task.Yield();
            if (www.error != null) {
                Debug.Log(www.error);
                www.Dispose();
                return 0f;
            }
            var trimmedString = www.downloadHandler.text.Substring(1, www.downloadHandler.text.Length - 2);
            var price = JsonUtility.FromJson<CoinGeckoCoinValue>(trimmedString);
            www.Dispose();
            return (float)price.current_price;
        }


        public static async Task<NftTemplateObject> GetTemplate(int template) {
            return await WaxNFT.GetTemplate(template);
        }
        public static async Task<float> GetBalance(Chain chain, string wallet) {
            switch (chain) {
                case Chain.Ethereum:
                    return await EthNFT.GetBalance(wallet);
                    break;
                case Chain.Wax:
                    return await WaxNFT.GetBalance(wallet);
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
    public class response {
        public bool success;
        public waxAssetData[] data;
        public long query_time;
    }
    [Serializable]
    public class collectionResponse {
        public bool success;
        public collection data;
        public long query_time;
    }
    [Serializable]
    public class collectionsResponse {
        public bool success;
        public collection[] data;
        public long query_time;
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


    public enum AssetSort
    {
        AssetId,
        Minted,
        Updated,
        Transferred,
        TemplateMint,
        Name
    }
    public enum CollectionSort
    {
        Created,
        CollectionName,
    }
}