using UnityEngine;

namespace Pixygon.NFT {
    public class NftActionTween : NftActionAsset {
        private bool _isInit = false;

        //public Tween t;
        public NftActionParameters _params;
        
        public override void Init(NFTObject nftObject) {
            base.Init(nftObject);
            if (_isInit) return;
        }

        public override void Kill() {
            base.Kill();
            //if (t != null) t.Kill();
            _nft.Root.localScale = Vector3.one;
            _nft.Root.localPosition = Vector3.zero;
            _nft.Root.localEulerAngles = Vector3.zero;
        }
    }
}