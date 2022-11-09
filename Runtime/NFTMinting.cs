using System;
using System.Threading.Tasks;
using Pixygon.Core;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;

namespace Pixygon.NFT {
    public class NFTMinting : MonoBehaviour {
        private static string AuthEndpoint = "https://d2jg5c0belmpnz.cloudfront.net/token";
        private static string MintEndpoint = "https://mint.immersys.io/mint";
        private static string username = "shinyeleblob";
        private static string password = "!mmer5y50nW4X2";

        private static async void Auth(string collection, string schema, int template, Action onSuccess) {
            WWWForm form = new WWWForm();
            form.AddField("username", username);
            form.AddField("password", password);
            form.AddField("grant_type", "password");
            UnityWebRequest www = UnityWebRequest.Post(AuthEndpoint, form);
            www.SetRequestHeader("content-type", "application/x-www-form-urlencoded");
            www.SendWebRequest();
            while(!www.isDone)
                await Task.Yield();
            if(www.error != null) {
                Analytics.CustomEvent("Minting-error: " + www.error);
                www.Dispose();
                return;
            }
            Mint(JsonUtility.FromJson<Auth>(www.downloadHandler.text), collection, schema, template, onSuccess);
            www.Dispose();
        }
        private static async void Mint(Auth auth, string collection, string schema, int template, Action onSuccess) {
            //Debug.Log("MINT! " + auth.access_token);
            Mintable m = new Mintable();
            m.authorized_minter = "m.immersys";
            m.collection_name = collection;
            m.schema_name = schema;
            m.template_id = template;
            m.new_asset_owner = PlayerPrefs.GetString("Wallet");
            m.immutable_data = new string[0];
            m.mutable_data = new string[0];
            m.tokens_to_back = new string[0];
            var bytes = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(m));
            UnityWebRequest www = UnityWebRequest.Put(MintEndpoint, bytes);
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer " + auth.access_token);
            www.SendWebRequest();
            while(!www.isDone)
                await Task.Yield();
            if(www.error != null) {
                Analytics.CustomEvent("Minting-error: " + www.error);
                www.Dispose();
                return;
            }
            MintResponse response = JsonUtility.FromJson<MintResponse>(www.downloadHandler.text);
            if(!response.success) {
                Debug.Log("Error! " + response.details);
                Analytics.CustomEvent("Minting-error: " + response.details);
                return;
            }
            www.Dispose();
            onSuccess?.Invoke();
        }

        public static void MintTemplate(string collection, string schema, int template, Action onSuccess = null) {
            //Auth(collection, schema, template, onSuccess);
        }

        public static void MintTemplate(NFTTemplateInfo info, Action onSuccess = null) {
            //Auth(info.collection, info.schema, info.template, onSuccess);
            MintApi(info.collection, info.schema, info.template, PlayerPrefs.GetString("Wallet"), onSuccess);
        }

        private static async void MintApi(string collection, string schema, int template, string wallet, Action onSuccess) {
            PixygonAPI.MintAssetAsync(collection, schema, template, onSuccess);
        }
    }

    
    public class UserData {
        public int id;
        public string username;
        public string wallet;
        public string token;
    }
    [System.Serializable]
    public class Credentials {
        public string username;
        public string password;
        public string grant_type;
    }

    [System.Serializable]
    public class Auth {
        public string access_token;
        public string token_type;
    }

    [System.Serializable]
    public class Mintable {
        public string authorized_minter;
        public string collection_name;
        public string schema_name;
        public int template_id;
        public string new_asset_owner;
        public string[] immutable_data;
        public string[] mutable_data;
        public string[] tokens_to_back;
    }

    [Serializable]
    public class MintResponse {
        public bool success;
        public string details;
    }
}