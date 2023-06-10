using System.Threading.Tasks;
using Pixygon.DebugTool;
using Pixygon.Saving;
using UnityEngine;
using UnityEngine.Networking;

namespace Pixygon.NFT
{
    public class MaticNFT : MonoBehaviour
    {
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
            Log.DebugMessage(DebugGroup.Nft, $"Something went wrong while fetching Matic NFT-data from Rarible: {www.error}\nURL: {www.url}");
            return null;
        }
        //Must implement
        private static NftTemplateObject[] GetList(response response) {
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
        public static async Task<NftTemplateObject[]> FetchAssets(NFTTemplateInfo info) {
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
        public static async Task<NftTemplateObject[]> FetchAllAssets(string collectionFilter = "") {
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
                Debug.Log("No Matic account to fetch NFT from!");
                return false;
            }
            var www = await GetRequest($"ownerships/MATIC:{info.collection}:{info.schema}:{Account}");
            if(www == null)
                return false;
            else {
                Debug.Log($"This was the URL: ownerships/MATIC:{info.collection}:{info.schema}:{Account}" +
                          "\nThis is the Matic-response!! " + www.downloadHandler.text);
                return true;
            }
        }
        public static async Task<NftTemplateObject> GetTemplate(int template) {
            /*
            var www = await GetRequest($"assets?template_id={template}&limit=100&order=desc&sort=asset_id");
            var d = JsonUtility.FromJson<response>(www.downloadHandler.text).data[0];
            www.Dispose();
            return d;
            */
            return null;
        }
        public static async Task<float> GetBalance() {
            var www = UnityWebRequest.Get(
                $"https://api.etherscan.io/api?module=account&action=balance" +
                $"&address={Account}" +
                $"&tag=latest" +
                $"&apikey=7VR9XJZM17W2P6NEXEJDPKUHB21FAHDEQ9");
            www.SetRequestHeader("Content-Type", "application/json");
            www.SendWebRequest();
            while (!www.isDone)
                await Task.Yield();
            if (www.error != null) {
                Debug.Log(www.error);
                www.Dispose();
                return 0f;
            }
            Debug.Log("Eth balance: " + www.downloadHandler.text);
            var balance = JsonUtility.FromJson<MaticBalanceResponse>(www.downloadHandler.text);
            www.Dispose();
            //Balance is returned in WEI, must be converted to Eth
            return (balance.result/1000000000000000000f);
        }
        public static async Task<NftTemplateObject[]> FetchAllAssetsInWallet(string wallet) {
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
    public class MaticBalanceResponse {
        public int status;
        public string message;
        public int result;
    }
}
