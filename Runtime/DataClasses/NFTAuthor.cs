using System;
using UnityEngine;

namespace Pixygon.NFT {
    [Serializable]
    [CreateAssetMenu(fileName = "NFTAuthor", menuName = "Pixygon/NFT/NFTAuthor", order = 0)]
    public class NFTAuthor : ScriptableObject {
        public string title;
        public NFTCollection[] collections;
    }
}