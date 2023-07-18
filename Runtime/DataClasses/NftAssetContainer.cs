using System.Collections.Generic;
using System.Text;
using Pixygon.PagedContent;
using Pixygon.NFT.Wax;
using UnityEngine;

namespace Pixygon.NFT {
    public class NftAssetContainer : PagedContentDataObject {
        //public waxAsset asset;
        public NftTemplateObject _nftObject;
    }

    //This is a template
    public class NftTemplateObject {
        public string Title;
        public string CollectionName;
        public string Description;
        public string[] IpfsHashes;
        public NFTTemplateInfo TemplateInfo;
        public AssetData[] assets;
        public NFTType MediaType;
        public Chain Chain;
        
        public string DisplayInfo() {
            var nftDataText = new StringBuilder();
            nftDataText.Append("\nTemplateID: ");
            nftDataText.Append(TemplateInfo.template);
            return nftDataText.ToString();
        }
        public NftTemplateObject(waxAssetData a) {
            Title = a.name;
            TemplateInfo = new NFTTemplateInfo((a.template == null ? a.template_id : a.template.template_id), a.schema.schema_name,
                a.collection.collection_name, Chain.Wax);
            if (a.data != null) {
                IpfsHashes = !string.IsNullOrWhiteSpace(a.data.video) ? new[] { a.data.video } : new[] { a.data.img };
                Description = a.data.Description;
            }
            IpfsHashes ??= GetIpfsHashes(a.immutable_data);
            if (string.IsNullOrWhiteSpace(Description))
                Description = GetDescription(a.immutable_data);
            CollectionName = a.collection.collection_name;
            Chain = Chain.Wax;
        }

        private string GetDescription(Dictionary<string, string> data) {
            if (data == null) return "";
            var desc = "";
            foreach (var pair in data) {
                Debug.Log(pair.Key + ": " + pair.Value);
                if (pair.Key.ToLower() == "description" || pair.Key.ToLower() == "desc") {
                    desc = pair.Value;
                }
            }

            return desc;
        }

        private string[] GetIpfsHashes(Dictionary<string, string> data) {
            if (data == null) return null;
            var ipfs = new List<string>();
            foreach (var pair in data) {
                Debug.Log(pair.Key + ": " + pair.Value);
                if (pair.Key.ToLower() == "video" || pair.Key.ToLower() == "image"  || pair.Key.ToLower() == "img") {
                    ipfs.Add(pair.Value);
                }
            }
            return ipfs.ToArray();
        }
    }

    //public class NftAssetObject {
    //    public string AssetId;
    //    public int Mint;
    //}
}