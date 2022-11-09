using UnityEngine;
using UnityEngine.UI;

namespace Pixygon.NFT {
    public class NFTRandomizer : MonoBehaviour {
        [SerializeField] private Image _image;
        public void Init(Sprite sprite) {
            _image.sprite = sprite;
        }
    }
}