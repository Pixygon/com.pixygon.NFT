using System.Text;
using Pixygon.PagedContent;
using Pixygon.NFT.Wax;

namespace Pixygon.NFT {
    public class NftAssetContainer : PagedContentDataObject {
        //public waxAsset asset;
        public string Title;
        public string CollectionName;
        public string Description;
        public string[] IpfsHashes;
        public NFTTemplateInfo TemplateInfo;
        public AssetData[] assets;
        public NFTType MediaType;
        public Chain Chain;
        
        public string DisplayInfo() {
            var nftDataText = new StringBuilder();
            //nftDataText.Append("Type: ");
            //nftDataText.Append(type);
            //nftDataText.Append("\nAction: ");
            //nftDataText.Append(nftAction);
            nftDataText.Append("\nTemplateID: ");
            nftDataText.Append(TemplateInfo.template);
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
        public NftAssetContainer(waxAssetData a) {
            Title = a.name;
            TemplateInfo = new NFTTemplateInfo(a.template.template_id, a.schema.schema_name,
                a.collection.collection_name, Chain.Wax);
            IpfsHashes = !string.IsNullOrWhiteSpace(a.data.video) ? new[] { a.data.video } : new[] { a.data.img };
            Description = a.data.Description;
            CollectionName = a.collection.collection_name;
            Chain = Chain.Wax;
        }
    }
}