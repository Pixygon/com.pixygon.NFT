using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pixygon.NFT {
    [CreateAssetMenu(fileName = "New NFTActions", menuName = "Pixygon/NFTActions")]
    public class NftActionsList : ScriptableObject {
        [FormerlySerializedAs("lookAtAction")] public NftActionAsset _lookAtAction;
        [FormerlySerializedAs("nftActions")] public List<NftActionAsset> _nftActions = new List<NftActionAsset>();

        public List<NftActionAsset> GetActionsOfType(NftActionType type) {
            return _nftActions.Where(action => action._type == type).OrderBy(action => _nftActions.IndexOf(action)).ToList();
        }

        public NftActionAsset GetAction(string actionName) {
            return _nftActions.FirstOrDefault(action => action._actionName == actionName);
        }
    }
}