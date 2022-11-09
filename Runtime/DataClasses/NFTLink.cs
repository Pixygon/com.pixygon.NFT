using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Pixygon.NFT {
    [Serializable]
    public class NFTLink {
        [ContextMenuItem("Get template", "GetTemplate")]
        public bool RequiresNFT;
        public NFTTemplateInfo[] Template;

        public async Task GetTemplate() {
            RequiresNFT = true;
            if(Template == null) {
                Template = new NFTTemplateInfo[1];
            }
            if(Template.Length == 0) {
                Template = new NFTTemplateInfo[1];
            }
            await Template[0].GetTemplate();
        }
    }
}