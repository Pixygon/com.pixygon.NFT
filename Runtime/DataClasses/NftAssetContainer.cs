﻿using System.Text;
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
            } else {
                Debug.Log("Immutable Assets: " + a.immutable_data);
                //IpfsHashes = !string.IsNullOrWhiteSpace(a.immutable_data.ContainsKey("image")) ? new[] { a.data.video } : new[] { a.data.img };
            }
            CollectionName = a.collection.collection_name;
            Chain = Chain.Wax;
        }
    }

    //public class NftAssetObject {
    //    public string AssetId;
    //    public int Mint;
    //}
}