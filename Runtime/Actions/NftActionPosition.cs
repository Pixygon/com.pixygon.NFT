using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pixygon.NFT {
    [CreateAssetMenu(fileName = "Action_Position", menuName = "Pixygon/NFT/Position Action")]
    public class NftActionPosition : NftActionTween {
        [FormerlySerializedAs("offsetFromGround")] [SerializeField] private float _offsetFromGround;

        private void OnEnable() {
            _type = NftActionType.LocalPosition;
        }

        public override void Init(NFTObject nftObject) {
            if (IsInit) return;

            base.Init(nftObject);

            switch (_params._animationType) {
                case NftActionParameters.AnimationType.Normal:
                    //t = _nft.Root.DOLocalMove(_params.target, _params.duration).SetRelative(true)
                    //    .SetLoops(-1, _params.loopType)
                    //    .SetEase(_params.easeType).SetDelay(_params.delayPerCycle);
                    break;
                case NftActionParameters.AnimationType.Punch:
                    //t = _nft.Root.DOPunchPosition(_params.target, _params.duration).SetRelative(true)
                    //    .SetLoops(-1, _params.loopType)
                    //    .SetEase(_params.easeType).SetDelay(_params.delayPerCycle);
                    break;
                case NftActionParameters.AnimationType.Shake:
                    //t = _nft.Root.DOShakePosition(_params.duration, _params.shakeStrength).SetRelative(true)
                    //    .SetLoops(-1, _params.loopType)
                    //    .SetEase(_params.easeType).SetDelay(_params.delayPerCycle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}