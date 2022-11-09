using UnityEngine;
using UnityEngine.Events;

namespace Pixygon.NFT {
    public class NFTAccessLock : MonoBehaviour {
        [SerializeField] private NFTTemplateInfo[] _requiredTemplate;
        [SerializeField] private UnityEvent _unlockAction;
        private void Start() {
            foreach(NFTTemplateInfo template in _requiredTemplate)
                NFT.ValidateTemplate(template, () => { _unlockAction?.Invoke(); });
        }
    }
}