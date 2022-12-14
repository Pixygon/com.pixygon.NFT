using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                if (account == string.Empty)
                    account = SaveManager.SettingsSave._waxWallet; // PlayerPrefs.GetString("WaxWallet");
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
                        "api.wax-aa.bountyblok.io",
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
                var www = UnityWebRequest.Get($"https://{endPoint}/atomicassets/v1/{url}&page={page}&limit={limit}");
                www.timeout = 60;
                www.SendWebRequest();
                while (!www.isDone)
                    await Task.Yield();
                if (www.error == null)
                    return www;


                Log.DebugMessage(DebugGroup.Nft, $"Something went wrong while fetching NFT-data on EndPoint {endPoint}: {www.error}\nURL: {www.url}\nRetry: {retry}");
                retry += 1;
            }

            Log.DebugMessage(DebugGroup.Nft, $"Retried NFT Fetch {maxTries} times, and still couldn't get it :(");
            return null;
        }
        private static waxAsset[] GetList(response response) {
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
        }
        /// <summary>
        /// Invokes 'finish' method and returns templates found
        /// </summary>
        /// <param name="info"></param>
        public static async Task<waxAsset[]> FetchAssets(NFTTemplateInfo info) {
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
        public static async Task<waxAsset[]> FetchAllAssets(string collectionFilter = "") {
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
        }
        /// <summary>
        /// Returns true if the account owns the template, and invokes success or failed actions, if supplied
        /// </summary>
        /// <param name="info">The required template</param>
        /// <returns></returns>
        public static async Task<bool> ValidateTemplate(NFTTemplateInfo info) {
            if (info.template == -1) return false;
            if (Account == string.Empty) {
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
        public static async Task<waxAssetData> GetTemplate(int template) {
            var www = await GetRequest($"assets?template_id={template}&limit=100&order=desc&sort=asset_id");
            var d = JsonUtility.FromJson<response>(www.downloadHandler.text).data[0];
            www.Dispose();
            return d;
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
        public static async Task<waxAsset[]> FetchAllAssetsInWallet(string wallet) {
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
        }
    }

    [System.Serializable]
    public class endpoints {
        public string[] nodeEndpoints;
        private int currentIndex = 0;

        public string GetEndpoint {
            get {
                currentIndex++;
                if (currentIndex >= nodeEndpoints.Length)
                    currentIndex = 0;
                return nodeEndpoints[currentIndex];
            }
        }
    }
}