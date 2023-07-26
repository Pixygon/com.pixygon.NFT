using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pixygon.DebugTool;
using Pixygon.Saving;
using UnityEngine;
using UnityEngine.Networking;

namespace Pixygon.NFT.Wax {
    public class WaxNFT : MonoBehaviour {
        //EndpointList: https://validate.eosnation.io/wax/reports/endpoints.html

        private static float _lastNodeUpdate;
        private static endpoints _nodes;
        private static string account = string.Empty;

        private static string Account {
            get {
                if (string.IsNullOrWhiteSpace(account) && !string.IsNullOrWhiteSpace(SaveManager.SettingsSave._user.waxWallet))
                    account = SaveManager.SettingsSave._user.waxWallet;
                return account;
            }
        }
        private static async Task CheckNodes() {
            if (_nodes == null || (Time.realtimeSinceStartup - _lastNodeUpdate) > 600f) {
                //string json = await PixygonAPI.GetEndpointNodesAsync();
                //nodes = JsonUtility.FromJson<endpoints>(json);
                _nodes = new endpoints {
                    nodeEndpoints = new[] {
                        "atomic-api.wax.cryptolions.io",
                        "wax-aa.eosdac.io",
                        "wax.api.atomicassets.io",
                        //"wax.greymass.com",             //Did not work!
                        "wax-aa.eu.eosamsterdam.net",
                        "wax.blokcrafters.io",
                        //"apiwax.3dkrender.com",         //Did not work!
                        //"query.3dkrender.com",          //Did not work!
                        "aa-wax-public1.neftyblocks.com",
                        //"aa-wax-public2.neftyblocks.com",       //Did not work!
                        //"api.wax.greeneosio.com",       //Did not work!
                        //"api.waxsweden.org",       //Did not work!
                        //"wax.api.eosnation.io",       //Did not work!
                        //"wax.pink.gg",       //Did not work!
                        //"api.wax-aa.bountyblok.io",
                        "atomicassets.ledgerwise.io",
                        "wax-atomic-api.eosphere.io",
                        "atomic.wax.eosrio.io",
                        "aa-api-wax.eosauthority.com",
                        "atomic.sentnl.io",
                        "atomic.wax.tgg.gg"
                        //"api-wax-aa.eosarabia.net"
                        
                    }
                };
                _lastNodeUpdate = Time.realtimeSinceStartup;
            }
        }
        private static async Task<UnityWebRequest> GetRequest(string url, int page = 1, int limit = 250) {
            await CheckNodes();
            var retry = 0;
            var maxTries = _nodes.nodeEndpoints.Length;
            while (retry < maxTries) {
                var endPoint = _nodes.GetEndpoint;
                var urlString = $"https://{endPoint}/atomicassets/v1/{url}";
                if(page != -1)
                    urlString += $"&page={page}&limit={limit}";
                var www = UnityWebRequest.Get(urlString);
                www.timeout = 60;
                www.SendWebRequest();
                while (!www.isDone) await Task.Yield();
                if (www.error == null) return www;
                Log.DebugMessage(DebugGroup.Nft, $"Something went wrong while fetching NFT-data on EndPoint {endPoint}: {www.error}\nURL: {www.url}\nRetry: {retry}");
                retry += 1;
            }
            Log.DebugMessage(DebugGroup.Nft, $"Retried NFT Fetch {maxTries} times, and still couldn't get it :(");
            return null;
        }
        private static NftTemplateObject[] GetList(response response) {
            var wax = new List<NftTemplateObject>();
            foreach (var data in response.data) {
                var found = false;
                foreach (var waxasset in wax) {
                    var listAssetData = waxasset.assets.ToList();
                    if (waxasset.TemplateInfo.template != data.template.template_id) continue;
                    listAssetData.Add(new AssetData(data.asset_id, data.owner, data.template_mint, data.template.issued_supply, data.template.max_supply));
                    found = true;
                    waxasset.assets = listAssetData.ToArray();
                }
                if (found) continue; {
                    var waxasset = new NftTemplateObject(data);
                    var listAssetData2 = new List<AssetData> { //waxasset.assets = new List<AssetData>();
                        new AssetData(data.asset_id, data.owner, data.template_mint, data.template.issued_supply, data.template.max_supply) };
                    waxasset.assets = listAssetData2.ToArray();
                    //waxasset.assets.Add(new AssetData(data.asset_id, data.owner, data.template_mint, data.template.issued_supply, data.template.max_supply));
                    wax.Add(waxasset);
                }
            }
            return wax.ToArray();
        }
        public static async Task<int> GetTotalAssets(string wallet) {
            var www = UnityWebRequest.Get($"https://wax.api.atomicassets.io/atomicassets/v1/accounts?owner={wallet}");
            www.SetRequestHeader("Content-Type", "application/json");
            www.SendWebRequest();
            while (!www.isDone)
                await Task.Yield();
            if (www.error != null)
            {
                Debug.Log(www.error);
                www.Dispose();
                return 0;
            }
            var accountResult = JsonUtility.FromJson<AssetCount>(www.downloadHandler.text);
            www.Dispose();
            Debug.Log("Amount: " + accountResult.data[0].assets);
            return accountResult.data[0].assets;
        }

        /// <summary>
        /// Invokes 'finish' method and returns templates found
        /// </summary>
        /// <param name="info"></param>
        public static async Task<NftTemplateObject[]> FetchAssets(NFTTemplateInfo info) {
            var url = "assets?owner=" + Account;
            if (info.collection != string.Empty)
                url += "&collection_name=" + info.collection;
            if (info.schema != string.Empty)
                url += "&schema_name=" + info.schema;
            if (info.template != -1)
                url += "&template_id=" + info.template;
            var www = await GetRequest(url);
            var a = GetList(JsonConvert.DeserializeObject<response>(www.downloadHandler.text));
            www.Dispose();
            return a;
        }
        public static async Task<NftTemplateObject[]> FetchAllAssets(string collectionFilter = "", string wallet = "", int page = 1, int limit = 250) {
            var url = $"assets?owner={(wallet == "" ? Account : wallet)}";
            if (Account == string.Empty)
                return null;
            if (!string.IsNullOrEmpty(collectionFilter))
                url += "&collection_whitelist=" + collectionFilter;
            var www = await GetRequest(url, page, limit);
            var r = JsonConvert.DeserializeObject<response>(www.downloadHandler.text);
            if (r.success == false || r.data.Length == 0) {
                Debug.Log("Something wrong, i guess?");
                //break;
            }
            var wax = JsonConvert.DeserializeObject<response>(www.downloadHandler.text).data.Select(data => new NftTemplateObject(data) {
                assets = new AssetData[] {
                    new(data.asset_id, data.owner, data.template_mint, data.template.issued_supply,
                        data.template.max_supply)
                }
            });
            www.Dispose();
            return wax.ToArray();
        }
        public static async Task<NftTemplateObject[]> FetchAllCollectionTemplates(string collectionFilter = "", int page = 1, int limit = 250) {
            var url = $"templates?collection_name={collectionFilter}";
            var www = await GetRequest(url, page, limit);
            Debug.Log("Templates: " + www.downloadHandler.text);
            var r = JsonConvert.DeserializeObject<response>(www.downloadHandler.text);
            if (r.success == false || r.data.Length == 0) Debug.Log("Something wrong, i guess?");
            var wax = new List<NftTemplateObject>();
            foreach (var data in r.data) {
                var d = new NftTemplateObject(data);
                wax.Add(d);
            }
            www.Dispose();
            return wax.ToArray();
        }
        public static async Task<collection> GetCollection(string collectionFilter = "") {
            var url = $"collections/{collectionFilter}";
            var www = await GetRequest(url, -1);
            var r = JsonConvert.DeserializeObject<collectionResponse>(www.downloadHandler.text);
            www.Dispose();
            return  r.data;
        }

        public static async Task<collection[]> SearchCollections(string search) {
            var url = $"collections?match={search}";
            var www = await GetRequest(url);
            var r = JsonConvert.DeserializeObject<collectionsResponse>(www.downloadHandler.text);
            www.Dispose();
            return  r.data;
        }
        
        //GET COLLECTION STATS: https://wax.api.atomicassets.io/atomicmarket/v1/stats/accounts?symbol=WAX&collection_name=pixygon&page=1&limit=100&order=desc&sort=buy_volume
        public static async Task<NftTemplateObject[]> FetchAllTemplates(string collectionFilter = "", string wallet = "", int page = 1, int limit = 250) {
            var allAssets = new List<NftTemplateObject>();
            var isComplete = false;
            var url = $"assets?owner={(wallet == "" ? Account : wallet)}";
            if (!string.IsNullOrEmpty(collectionFilter))
                url += "&collection_whitelist=" + collectionFilter;
            while (!isComplete) {
                var www = await GetRequest(url, page, limit);
                page++;
                var r = JsonConvert.DeserializeObject<response>(www.downloadHandler.text);
                if (r.success == false || r.data.Length == 0) {
                    isComplete = true;
                    break;
                }
                isComplete = true;
                var a = GetList(JsonUtility.FromJson<response>(www.downloadHandler.text));
                allAssets.AddRange(a);
                www.Dispose();
            }
            return allAssets.ToArray();
        }
        
        /// <summary>
        /// Returns true if the account owns the template, and invokes success or failed actions, if supplied
        /// </summary>
        /// <param name="info">The required template</param>
        /// <returns></returns>
        public static async Task<bool> ValidateTemplate(NFTTemplateInfo info) {
            if (info.template == -1) return false;
            if (string.IsNullOrWhiteSpace(Account)) {
                Debug.Log("No account to fetch NFT from!");
                return false;
            }
            UnityWebRequest www = null;
            www = await GetRequest($"assets?owner={Account}&template_id={info.template}");
            var wax = GetList(JsonUtility.FromJson<response>(www.downloadHandler.text));
            www.Dispose();
            bool owned;
            if (wax == null)
                owned = false;
            else if (wax.Length == 0)
                owned = false;
            else if (wax[0].assets == null)
                owned = false;
            else if (wax[0].assets.Length == 0)
                owned = false;
            else
                owned = true;
            return owned;
        }
        public static async Task<NftTemplateObject> GetTemplate(int template) {
            var www = await GetRequest($"assets?template_id={template}&limit=100&order=desc&sort=asset_id");
            var d = JsonUtility.FromJson<response>(www.downloadHandler.text).data[0];
            www.Dispose();
            return new NftTemplateObject(d);
        }
        public static async Task<float> GetBalance(string wallet) {
            var data = new PostData {
                json = true,
                code = "eosio.token",
                scope = wallet,
                table = "accounts",
                lower_bound = "WAX",
                upper_bound = "WAX",
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
                return 0f;
            }
            var accountResult = JsonUtility.FromJson<AccountResult>(www.downloadHandler.text);
            www.Dispose();
            if (accountResult.rows.Length == 0) return 0f;
            var s = accountResult.rows[0].balance;
            var trimmedString = s.Remove(s.Length - 4, 4);
            float.TryParse(trimmedString, NumberStyles.Currency, CultureInfo.InvariantCulture, out var f);
            Debug.Log("Amount: " + f);
            return f;
        }
        public static async Task<NftTemplateObject[]> FetchAllAssetsInWallet(string wallet) {
            var allAssets = new List<NftTemplateObject>();
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
        }
    }
}