using System.Collections.Generic;
using System.Linq;
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
        public string CollectionId;
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
            TemplateInfo = new NFTTemplateInfo(a.template == null ? a.template_id : a.template.template_id, a.schema.schema_name, a.collection.collection_name);
            if (a.data != null) {
                IpfsHashes = !string.IsNullOrEmpty(a.data.video) ? new[] { a.data.video } : new[] { a.data.img };
                Description = a.data.Description;
            }
            IpfsHashes ??= GetIpfsHashes(a.immutable_data);
            if (string.IsNullOrEmpty(Description))
                Description = GetDescription(a.immutable_data);
            CollectionName = a.collection.name;
            CollectionId = a.collection.collection_name;
            Chain = Chain.Wax;
        }

        private static string GetDescription(Dictionary<string, object> data) {
            if (data == null) return "";
            foreach (var pair in data.Where(pair => pair.Key.ToLower() == "description" || pair.Key.ToLower() == "desc" || pair.Key.ToLower() == "info"))
                return pair.Value.ToString();
            return "";
        }
        private static string[] GetIpfsHashes(Dictionary<string, object> data) {
            return data == null ? null : (from pair in data where pair.Key.ToLower() == "video" || pair.Key.ToLower() == "image" || pair.Key.ToLower() == "img" select pair.Value.ToString()).ToArray();
        }
    }
}