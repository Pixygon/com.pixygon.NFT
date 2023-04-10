using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Pixygon.NFT {
    public class OtherNFT : MonoBehaviour {
        private static string account = string.Empty;

        private static string GetWallet(Chain chain) {
            switch(chain) {
                case Chain.Wax:
                return PlayerPrefs.GetString("WAXWallet");
                case Chain.EOS:
                return PlayerPrefs.GetString("EOSWallet");
                case Chain.Ethereum:
                return PlayerPrefs.GetString("ETHWallet");
                case Chain.Tezos:
                return PlayerPrefs.GetString("TEZWallet");
                case Chain.Polygon:
                return PlayerPrefs.GetString("PolygonWallet");
                case Chain.Polkadot:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.Elrond:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.BinanceChain:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.Cardano:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.Stellar:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.Neo:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.HyperledgerFabric:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.Waves:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.Cosmos:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.Ripple:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.Nem:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.Solana:
                return PlayerPrefs.GetString("SolanaWallet");
                case Chain.Hive:
                return PlayerPrefs.GetString("WaxWallet");
                case Chain.Phantom:
                return PlayerPrefs.GetString("WaxWallet");
            }
            return "";
        }
        private static async Task<UnityWebRequest> GetRequest(string url, string json) {
            int retry = 0;
            int maxTries = 5;
            while (retry < maxTries) {
                WWWForm form = new WWWForm();
                form.AddField("data", json);
                UnityWebRequest www = UnityWebRequest.PostWwwForm(string.Format("http://18.225.33.231/{0}", url), json);

                www.timeout = 60;
                //www.method = "GET";
                www.SetRequestHeader("Content-Type", "application/json");
                www.SendWebRequest();
                while (!www.isDone)
                    await Task.Yield();
                if (www.error == null)
                    return www;


                Debug.Log("Something went wrong while fetching NFT-data" + ": " + www.error +
                          "\nURL: " + www.url +
                          "\nRetry: " + retry);
                retry += 1;
            }

            Debug.Log("Retried NFT Fetch " + maxTries + " times, and still couldn't get it :(");
            return null;
        }

        /*
        public static waxAsset[] GetList(response response) {
            List<waxAsset> wax = new List<waxAsset>();
            foreach (waxAssetData data in response.data) {
                bool found = false;
                foreach (waxAsset waxasset in wax) {
                    if (waxasset.templateID == data.template.template_id) {
                        waxasset.assets.Add(new AssetData(data.asset_id, data.owner, data.template_mint,
                            data.template.issued_supply, data.template.max_supply));
                        found = true;
                    }
                }

                if (!found) {
                    waxAsset waxasset = new waxAsset();
                    waxasset.name = data.name;
                    waxasset.templateID = data.template.template_id;
                    waxasset.assets = new List<AssetData>();
                    waxasset.assets.Add(new AssetData(data.asset_id, data.owner, data.template_mint,
                        data.template.issued_supply, data.template.max_supply));
                    wax.Add(waxasset);
                }
            }

            return wax.ToArray();
        }
        */

        /// <summary>
        /// Invokes 'finish' method and returns templates found
        /// </summary>
        /// <param name="info"></param>
        /// <param name="finish"></param>
        public static async Task<NftTemplateObject[]> FetchAssets(NFTTemplateInfo info) {
            var url = "FetchAsset";
            var json = JsonUtility.ToJson(new NFTRequest(info, GetWallet(info.chain)));
            Debug.Log(url + "\n" + json);
            var www = await GetRequest(url, json);
            Debug.Log("PLEASE COPYPASTE THIS TO ME, MAGNTA:\n" + www.downloadHandler.text);
            //var a = JsonUtility.FromJson<FetchedAssets>(www.downloadHandler.text).assets;
            //Debug.Log(a.Length);
            www.Dispose();
            //return a;
            return null;
        }
        public static async Task<NftTemplateObject[]> FetchAllAssets(string collectionFilter = "") {
            var allAssets = new List<NftTemplateObject>();
            var page = 1;
            var isComplete = false;
            var url = "assets?owner=";// + Account;
            if (!string.IsNullOrEmpty(collectionFilter))
                url += "&collection_whitelist=" + collectionFilter;
            while (!isComplete) {
                var www = await GetRequest(url, "");
                page++;
                var r = JsonUtility.FromJson<response>(www.downloadHandler.text);
                if (r.success == false || r.data.Length == 0) {
                    isComplete = true;
                    break;
                }
                NftTemplateObject[] a = null; // GetList(JsonUtility.FromJson<response>(www.downloadHandler.text));
                allAssets.AddRange(a);
                www.Dispose();
            }
            return allAssets.ToArray();
        }
        /// <summary>
        /// Returns true if the account owns the template, and invokes success or failed actions, if supplied
        /// </summary>
        /// <param name="info">The required template</param>
        /// <param name="success">Action if successful</param>
        /// <param name="failed">Action if failed</param>
        /// <returns></returns>
        public static async Task<bool> ValidateTemplate(NFTTemplateInfo info) {
            if (info.template == -1)
                return false;
            var json = JsonUtility.ToJson(new NFTRequest(info, GetWallet(info.chain)));
            var www = await GetRequest("ValidateTemplate", json);
            NftTemplateObject[] wax = null; //GetList(JsonUtility.FromJson<response>(www.downloadHandler.text));
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
            var www = await GetRequest("", "");
            //NftAssetContainer d = JsonUtility.FromJson<response>(www.downloadHandler.text).data[0];
            www.Dispose();
            //return d;
            return null;
        }
        public static async Task<string> GetBalance(string symbol = "WAX", string code = "eosio.token") {
            var data = new PostData {
                json = true,
                code = code,
                //data.scope = Account;
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
            if (accountResult.rows.Length == 0) {
                return "0";
            } else {
                var balance = accountResult.rows[0].balance;
                return balance;
            }
        }
    }

    [Serializable]
    public class NFTRequest {
        public NFTTemplateInfo NFTTemplateInfo;
        public string wallet;

        public NFTRequest(NFTTemplateInfo info, string w) {
            NFTTemplateInfo = info;
            wallet = w;
        }
    }

    [Serializable]
    public class FetchedAssets {
        //public waxAsset[] assets;
    }
}