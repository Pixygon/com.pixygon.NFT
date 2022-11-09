using UnityEngine;
using UnityEngine.Serialization;

namespace Pixygon.NFT {
    [CreateAssetMenu(fileName = "Action_LookAt", menuName = "Pixygon/NFT/LookAt Action")]
    public class NftActionLookAtTarget : NftActionAsset {

        [FormerlySerializedAs("target")] [SerializeField] private Transform _target;
        [FormerlySerializedAs("targetIsCamera")] [SerializeField] private bool _targetIsCamera = true;
        [FormerlySerializedAs("onlyY")] [SerializeField] private bool _onlyY = true;

        
        
        private void OnEnable() {
            _type = NftActionType.Rotation;
        }

        public override void Init(NFTObject nftObject) {
            if(IsInit)
                return;
            base.Init(nftObject);
            
        }

        public override void Update() {
            base.Update();
            
            if(_target == null)
                GetTarget();
            else
                _nft.Root.LookAt(_target);
            if(_onlyY)
                _nft.Root.localEulerAngles = new Vector3(0f, _nft.Root.localEulerAngles.y, 0f);
        }

        public override void Kill() {
            base.Kill();
            _nft.Root.transform.localEulerAngles = Vector3.zero;
        }
        
        private void GetTarget() {
            if(_targetIsCamera)
                _target = Camera.main.transform;
        }

    }
}
