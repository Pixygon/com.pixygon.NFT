using Pixygon.PagedContent;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Pixygon.NFT {
    [Serializable]
    [CreateAssetMenu(fileName = "NFTCollection", menuName = "Pixygon/NFT/NFTCollection", order = 0)]
    public class NFTCollection : PagedContentDataObject {
        public string title;
        public string displayTitle;
        public bool newCollection;
        public AssetReference iconRef;
        public AssetReference coverRef;
        public NFTData[] nfts;
        public AssetReference[] nftReferences;
    }
}