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
                UnityWebRequest www = UnityWebRequest.Post(string.Format("http://18.225.33.231/{0}", url), json);

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
        public static async Task<waxAsset[]> FetchAssets(NFTTemplateInfo info) {
            string url = "FetchAsset";
            string json = JsonUtility.ToJson(new NFTRequest(info, GetWallet(info.chain)));
            Debug.Log(url + "\n" + json);
            UnityWebRequest www = await GetRequest(url, json);
            Debug.Log("PLEASE COPYPASTE THIS TO ME, MAGNTA:\n" + www.downloadHandler.text);
            waxAsset[] a = JsonUtility.FromJson<FetchedAssets>(www.downloadHandler.text).assets;
            Debug.Log(a.Length);
            www.Dispose();
            return a;
        }

        public static async Task<waxAsset[]> FetchAllAssets(string collectionFilter = "") {
            List<waxAsset> allAssets = new List<waxAsset>();
            int page = 1;
            bool isComplete = false;
            string url = "assets?owner=";// + Account;
            if (!string.IsNullOrEmpty(collectionFilter))
                url += "&collection_whitelist=" + collectionFilter;


            while (!isComplete) {
                UnityWebRequest www = await GetRequest(url, "");

                page++;

                response r = JsonUtility.FromJson<response>(www.downloadHandler.text);

                if (r.success == false || r.data.Length == 0) {
                    isComplete = true;
                    break;
                }

                waxAsset[] a = null; // GetList(JsonUtility.FromJson<response>(www.downloadHandler.text));
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
            UnityWebRequest www = null;
            string json = JsonUtility.ToJson(new NFTRequest(info, GetWallet(info.chain)));
            www = await GetRequest("ValidateTemplate", json);
            waxAsset[] wax = null; //GetList(JsonUtility.FromJson<response>(www.downloadHandler.text));
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
            UnityWebRequest www = await GetRequest("", "");
            waxAssetData d = JsonUtility.FromJson<response>(www.downloadHandler.text).data[0];
            www.Dispose();
            return d;
        }

        public static async Task<string> GetBalance(string symbol = "WAX", string code = "eosio.token") {
            PostData data = new PostData();
            data.json = true;
            data.code = code;
            //data.scope = Account;
            data.table = "accounts";
            data.lower_bound = symbol;
            data.upper_bound = symbol;
            data.limit = 1;
            UnityWebRequest www = UnityWebRequest.Put("https://wax.greymass.com/v1/chain/get_table_rows",
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

            AccountResult accountResult = JsonUtility.FromJson<AccountResult>(www.downloadHandler.text);
            www.Dispose();
            if (accountResult.rows.Length == 0) {
                return "0";
            }
            else {
                string balance = accountResult.rows[0].balance;
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
        public waxAsset[] assets;
    }
}