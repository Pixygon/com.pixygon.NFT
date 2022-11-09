using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pixygon.NFT {
    public class NftActionAsset : ScriptableObject {
        [FormerlySerializedAs("actionName")] public string _actionName;
        [HideInInspector] public NFTObject _nft;
        public bool IsInit { get; set; }

        [FormerlySerializedAs("type")] public NftActionType _type;

        public virtual void Init(NFTObject nftObject) {
            _nft = nftObject;
        }

        public virtual void Update() {
        }

        public virtual void Kill() {
        }
    }


    public enum NftActionType {
        LocalPosition = 0,
        Move = 1,
        Rotation = 2,
        Scale = 3
    }
    

    [Serializable]
    public class NftActionGroup {
        [FormerlySerializedAs("actionNames")] public List<string> _actionNames = new List<string>();
    }
}