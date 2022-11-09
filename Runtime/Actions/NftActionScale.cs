using System;
using UnityEngine;

namespace Pixygon.NFT {
    [CreateAssetMenu(fileName = "Action_Scale", menuName = "Pixygon/NFT/Scale Action")]
    public class NftActionScale : NftActionTween {
        private void OnEnable() {
            _type = NftActionType.Scale;
        }
        public override void Init(NFTObject nftObject) {
            if (IsInit) return;
            base.Init(nftObject);
            switch (_params._animationType) {
                case NftActionParameters.AnimationType.Normal:
                    //t = _nft.Root.DOScale(_params._target, _params._duration).SetRelative(true).SetLoops(-1, _params.loopType)
                    //    .SetEase(_params.easeType).SetDelay(_params._delayPerCycle);
                    break;
                case NftActionParameters.AnimationType.Punch:
                    //t = _nft.Root.DOPunchScale(_params._target, _params._duration).SetRelative(true).SetLoops(-1, _params.loopType)
                    //    .SetEase(_params.easeType).SetDelay(_params._delayPerCycle);
                    break;
                case NftActionParameters.AnimationType.Shake:
                    //t = _nft.Root.DOShakeScale(_params._duration, _params._shakeStrength).SetRelative(true).SetLoops(-1, _params.loopType)
                    //    .SetEase(_params.easeType).SetDelay(_params._delayPerCycle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}