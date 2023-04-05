using System.Text;
using Pixygon.PagedContent;
using Pixygon.NFT.Wax;

namespace Pixygon.NFT {
    public class NftAssetContainer : PagedContentDataObject {
        //public waxAsset asset;
        public string name;
        public string collectionName;
        public string ipfs;
        public string description;
        public string video;
        //public string chain;
        //public int templateID;
        //public string schemaID;
        public NFTTemplateInfo templateInfo;
        public AssetData[] assets;
        
        public string DisplayInfo() {
            var nftDataText = new StringBuilder();
            //nftDataText.Append("Type: ");
            //nftDataText.Append(type);
            //nftDataText.Append("\nAction: ");
            //nftDataText.Append(nftAction);
            nftDataText.Append("\nTemplateID: ");
            nftDataText.Append(templateInfo.template);
            return nftDataText.ToString();
        }

        /*
        public NFTTemplateInfo TemplateInfo() {
            NFTTemplateInfo info = new NFTTemplateInfo();
            info.chain = chain;
            info.collection = collectionName;
            info.schema = schemaID;
            info.template = templateID;
            return info;
        }
        */
        public NFTType type;

        //public NftAssetContainer(waxAsset a) {
        //    asset = a;
            //Fill up the data here
        //}
        public NftAssetContainer(waxAssetData a) {
            name = a.name;
            templateInfo = new NFTTemplateInfo(a.template.template_id, a.schema.schema_name,
                a.collection.collection_name, Chain.Wax);
            //waxasset.templateID = data.template.template_id;
            ipfs = a.data.img;
            video = a.data.video;
            description = a.data.Description;
            collectionName = a.collection.collection_name;
        }
    }
}