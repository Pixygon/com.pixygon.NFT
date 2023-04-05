using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pixygon.DebugTool;
using Pixygon.Saving;
using UnityEngine;
using UnityEngine.Networking;

namespace Pixygon.NFT.Eth {
    public class EthNFT : MonoBehaviour {
        private static string account = string.Empty;

        private static string Account {
            get {
                if (string.IsNullOrWhiteSpace(account) && !string.IsNullOrWhiteSpace(SaveManager.SettingsSave._user.ethWallet))
                    account = SaveManager.SettingsSave._user.ethWallet;
                return account;
            }
        }
        private static async Task<UnityWebRequest> GetRequest(string url, int page = 1, int limit = 250) {
            var www = UnityWebRequest.Get($"https://api.rarible.org/v0.1/{url}");
            www.timeout = 60;
            www.SendWebRequest();
            while (!www.isDone) 
                await Task.Yield();
            if (www.error == null) 
                return www;
            Log.DebugMessage(DebugGroup.Nft, $"Something went wrong while fetching Ethereum NFT-data from Rarible: {www.error}\nURL: {www.url}");
            return null;
        }
        //Must implement
        private static NftAssetContainer[] GetList(response response) {
            /*
            var wax = new List<waxAsset>();
            foreach (var data in response.data) {
                var found = false;
                foreach (var waxasset in wax) {
                    var listAssetData = waxasset.assets.ToList();
                    if (waxasset.templateInfo.template != data.template.template_id) continue;
                    listAssetData.Add(new AssetData(data.asset_id, data.owner, data.template_mint, data.template.issued_supply, data.template.max_supply));
                    found = true;
                    waxasset.assets = listAssetData.ToArray();
                }

                if (found) continue;
                {
                    var waxasset = new waxAsset {
                        name = data.name,
                        templateInfo = new NFTTemplateInfo(data.template.template_id, data.schema.schema_name, data.collection.collection_name, Chain.Wax),
                        //waxasset.templateID = data.template.template_id;
                        ipfs = data.data.img,
                        description = data.data.Description,
                        collectionName = data.collection.collection_name
                    };
                    var listAssetData2 = new List<AssetData> { //waxasset.assets = new List<AssetData>();
                        new AssetData(data.asset_id, data.owner, data.template_mint, data.template.issued_supply, data.template.max_supply) };
                    waxasset.assets = listAssetData2.ToArray();
                    //waxasset.assets.Add(new AssetData(data.asset_id, data.owner, data.template_mint, data.template.issued_supply, data.template.max_supply));
                    wax.Add(waxasset);
                }
            }
            return wax.ToArray();
            */
            return null;
        }
        /// <summary>
        /// Invokes 'finish' method and returns templates found
        /// </summary>
        /// <param name="info"></param>
        public static async Task<NftAssetContainer[]> FetchAssets(NFTTemplateInfo info) {
            var url = "assets?owner=" + Account;
            if (info.collection != string.Empty)
                url += "&collection_name=" + info.collection;
            if (info.schema != string.Empty)
                url += "&schema_name=" + info.schema;
            if (info.template != -1)
                url += "&template_id=" + info.template;
            var www = await GetRequest(url);
            var a = GetList(JsonUtility.FromJson<response>(www.downloadHandler.text));
            www.Dispose();
            return a;
        }
        public static async Task<NftAssetContainer[]> FetchAllAssets(string collectionFilter = "") {
            /*
            var allAssets = new List<waxAsset>();
            var page = 1;
            var isComplete = false;
            var url = "assets?owner=" + Account;
            if (!string.IsNullOrEmpty(collectionFilter))
                url += "&collection_whitelist=" + collectionFilter;


            while (!isComplete) {
                var www = await GetRequest(url, page);

                page++;

                var r = JsonUtility.FromJson<response>(www.downloadHandler.text);

                if (r.success == false || r.data.Length == 0) {
                    isComplete = true;
                    break;
                }

                var a = GetList(JsonUtility.FromJson<response>(www.downloadHandler.text));
                allAssets.AddRange(a);
                www.Dispose();
            }

            return allAssets.ToArray();
            */
            return null;
        }
        
        /// <summary>
        /// Returns true if the account owns the template, and invokes success or failed actions, if supplied
        /// </summary>
        /// <param name="info">The required template</param>
        /// <returns></returns>
        public static async Task<bool> ValidateTemplate(NFTTemplateInfo info) {
            if (info.template == -1) return false;
            if (string.IsNullOrWhiteSpace(Account)) {
                Debug.Log("No ETH account to fetch NFT from!");
                return false;
            }
            var www = await GetRequest($"ownerships/ETHEREUM:{info.collection}:{info.schema}:{Account}");
            if(www == null)
                return false;
            else {
                Debug.Log($"This was the URL: ownerships/ETHEREUM:{info.collection}:{info.schema}:{Account}" +
                          "\nThis is the ETH-response!! " + www.downloadHandler.text);
                return true;
            }
        }
        public static async Task<NftAssetContainer> GetTemplate(int template) {
            /*
            var www = await GetRequest($"assets?template_id={template}&limit=100&order=desc&sort=asset_id");
            var d = JsonUtility.FromJson<response>(www.downloadHandler.text).data[0];
            www.Dispose();
            return d;
            */
            return null;
        }
        public static async Task<string> GetBalance(string symbol = "WAX", string code = "eosio.token") {
            var data = new PostData {
                json = true,
                code = code,
                scope = Account,
                table = "accounts",
                lower_bound = symbol,
                upper_bound = symbol,
                limit = 1
            };
            var www = UnityWebRequest.Put("https://wax.greymass.com/v1/chain/get_table_rows",
                JsonUtility.ToJson(data));
            www.SetRequestHeader("Content-Type", "application/json");
            www.SendWebRequest();
            while (!www.isDone)
                await Task.Yield();
            if (www.error != null) {
                Debug.Log(www.error);
                www.Dispose();
                return string.Empty;
            }

            var accountResult = JsonUtility.FromJson<AccountResult>(www.downloadHandler.text);
            www.Dispose();
            return accountResult.rows.Length == 0 ? "0" : accountResult.rows[0].balance;
        }
        public static async Task<NftAssetContainer[]> FetchAllAssetsInWallet(string wallet) {
            /*
            var allAssets = new List<waxAsset>();
            var page = 1;
            var isComplete = false;
            var url = "assets?owner=" + Account;
            while (!isComplete) {
                var www = await GetRequest(url, page);

                page++;

                var r = JsonUtility.FromJson<response>(www.downloadHandler.text);

                if (r.success == false || r.data.Length == 0) {
                    isComplete = true;
                    break;
                }

                var a = GetList(JsonUtility.FromJson<response>(www.downloadHandler.text));
                allAssets.AddRange(a);
                www.Dispose();
            }

            return allAssets.ToArray();
            */
            return null;
        }
    }
}