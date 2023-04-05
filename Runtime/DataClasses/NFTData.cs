using Pixygon.PagedContent;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Video;

namespace Pixygon.NFT {
    [Serializable]
    [CreateAssetMenu(fileName = "NFTData", menuName = "Pixygon/NFT/NFTData", order = 1)]
    public class NFTData : PagedContentDataObject {
        public string title;
        public string description;

        public NFTCollection collection;

        //public string schema;
        //public int template;
        public bool nsfw;
        public AssetReference prefabRef;
        public AssetReference modelRef;
        public AudioClip audio;
        public Material material;
        public NFTType type;
        public NFTAction nftAction;
        public NFTPlacement placement;
        public VideoClip video;
        public Sprite[] randomizable;
        [Range(.1f, 10f)] public float minScale = 1;
        [Range(.1f, 10f)] public float maxScale = 1;
        public TextAsset gifAsset;

        public Vector3 rotationAxis;
        public float rotationAmount;
        public bool getFromIPFS;
        public string IPFSHash;

        [InspectorButton("GetTemplate", ButtonWidth = 150), SerializeField]
        private bool GetTemplateInfo;

        public NFTLink NFTLink;

        public async void GetTemplate() {
            //if(template == 0)
            //    return;
            /*
            waxAssetData t = await NFT.GetTemplate(NFTLink.Template[0].template);
            if(t == null)
                Debug.Log("Wrong template");
            else {
                title = t.name;
                Title = t.name;
                if(!string.IsNullOrEmpty(t.data.Description)) {
                    description = t.data.Description;
                    Description = t.data.Description;
                }
                if(!string.IsNullOrEmpty(t.data.img)) {
                    IPFSHash = t.data.img;
                }
                //collection = t.collection.name;
                //schema = t.schema.schema_name;
            }

            //if(NFTLink.Template == null) {
            //    NFTLink.Template = new NFTTemplateInfo[1];
            //    NFTLink.Template[0] = new NFTTemplateInfo(template);
            //}
            //if(NFTLink.Template.Length == 0) {
            //    NFTLink.Template = new NFTTemplateInfo[1];
            //    NFTLink.Template[0] = new NFTTemplateInfo(template);
            //}
            await NFTLink.Template[0].GetTemplate();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
#endif
            */
        }


        public string DisplayInfo() {
            var nftDataText = new StringBuilder();
            nftDataText.Append("Type: ");
            nftDataText.Append(type);
            nftDataText.Append("\nAction: ");
            nftDataText.Append(nftAction);
            nftDataText.Append("\nTemplateID: ");
            nftDataText.Append(NFTLink.Template[0].template);
            return nftDataText.ToString();
        }
    }
    public enum NFTPlacement {
        Anywhere,
        Horizontal,
        Vertical
    }

    public enum NFTType {
        Image = 0,
        Model = 1,
        Audio = 2,
        Video = 3,
        Book = 4
    }

    public enum NFTAction {
        Display,
        Follow,
        FollowAnimated,
        Spin,
        Randomize,
        Equipment
    }
}