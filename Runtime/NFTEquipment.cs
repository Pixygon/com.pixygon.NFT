using UnityEngine;
using UnityEngine.UI;

namespace Pixygon.NFT {
    public class NFTEquipment : MonoBehaviour {
        [SerializeField] private GameObject locked;

        public void Setup(NFTTemplateInfo template, GameObject locks) {
            locked = locks;
            GetComponent<Button>().enabled = false;
            locked.SetActive(true);
            NFT.ValidateTemplate(template, this.HasNFT);
        }

        private void HasNFT() {
            GetComponent<Button>().enabled = true;  
            locked.SetActive(false);
        }
    }
}