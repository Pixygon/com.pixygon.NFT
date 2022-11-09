using System;
using UnityEngine;

namespace Pixygon.NFT {
    [CreateAssetMenu(fileName = "Action_Rotation", menuName = "Pixygon/NFT/Rotation Action")]
    public class NftActionRotation : NftActionTween {
        private void OnEnable() {
            _type = NftActionType.Rotation;
        }
        public override void Init(NFTObject nftObject) {
            if (IsInit) return;
            base.Init(nftObject);
            switch (_params._animationType) {
                case NftActionParameters.AnimationType.Normal:
                    //t = _nft.Root.DOLocalRotate(_params._target, _params._duration, RotateMode.LocalAxisAdd).SetRelative(true)
                    //    .SetLoops(-1, _params.loopType)
                    //    .SetEase(_params.easeType).SetDelay(_params._delayPerCycle);
                    break;
                case NftActionParameters.AnimationType.Punch:
                    //t = _nft.Root.DOPunchRotation(_params._target, _params._duration).SetRelative(true)
                    //    .SetLoops(-1, _params.loopType)
                    //    .SetEase(_params.easeType).SetDelay(_params._delayPerCycle);
                    break;
                case NftActionParameters.AnimationType.Shake:
                    //t = _nft.Root.DOShakeRotation(_params._duration,_params._shakeStrength).SetRelative(true)
                    //    .SetLoops(-1, _params.loopType)
                    //    .SetEase(_params.easeType).SetDelay(_params._delayPerCycle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}