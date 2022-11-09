using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pixygon.NFT {
    [Serializable]
    public class NftActionParameters {
        [FormerlySerializedAs("target")] public Vector3 _target;
        [FormerlySerializedAs("duration")] public float _duration = 1;
        [FormerlySerializedAs("delayPerCycle")] public float _delayPerCycle = 0;
        //public Ease easeType = Ease.InOutSine;
        //public LoopType loopType = LoopType.Yoyo;
        [FormerlySerializedAs("animationType")] public AnimationType _animationType = AnimationType.Normal;
        [FormerlySerializedAs("shakeStrength")] public float _shakeStrength;
        public enum AnimationType {
            Normal,
            Punch,
            Shake
        }
    }
}