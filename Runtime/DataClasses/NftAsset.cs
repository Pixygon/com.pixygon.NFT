using System;

namespace Pixygon.NFT {
    [Serializable]
    public class NFTAsset {
        public string collection;
        public int template;
        public NFTData Data;
        //public waxAsset WaxData;
        public AssetData Asset;
    }
}