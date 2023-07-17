using System;
using System.Collections.Generic;

namespace Pixygon.NFT.Wax {
    
    /// <summary>
    /// This is a template, and the number of assets associated with it
    /// </summary>
    /*
    [Serializable]
    public class waxAsset {
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

        public NFTTemplateInfo TemplateInfo() {
            NFTTemplateInfo info = new NFTTemplateInfo();
            info.chain = chain;
            info.collection = collectionName;
            info.schema = schemaID;
            info.template = templateID;
            return info;
        }
        public NFTType type;
    }
    */
    [Serializable]
    public class waxAssetData {
        public string contract;
        public string asset_id;
        public string owner;
        public string name;
        public int template_id;
        public bool is_transferable;
        public bool is_burnable;
        public collection collection;
        public schema schema;
        public template template;
        //public backed_tokens[] backed_tokens;
        public Dictionary<string, string> mutable_data;
        public Dictionary<string, string> immutable_data;
        public int template_mint;

        public string burned_by_account;
        public string burned_at_block;
        public string burned_at_time;
        //public string updated_at_block;
        //public string updated_at_time;
        //public string transferred_at_block;
        //public string transferred_at_time;
        //public string minted_at_block;
        //public string minted_at_time;
        public nftdata data;
        public override string ToString() {
            return
                $"Contract: {contract}\nAssetID: {asset_id}\nOwner: {owner}\nName: {name}\nTemplate: {template.template_id}";
        }
    }
    [Serializable]
    public class nftdata {
        public string img;
        public string video;
        public string Info;
        public string name;
        public string Description;
    }
    [Serializable]
    public class collection
    {
        public string collection_name;
        public string name;
        public string img;
        public string images;
        public string author;
        public bool allow_notify;
        public string[] authorized_accounts;
        public string[] notify_accounts;
        public float market_fee;
        public string created_at_block;
        public string created_at_time;
    }
    [Serializable]
    public class schema
    {
        public string schema_name;
        public format[] format;
        public string created_at_block;
        public string created_at_time;
    }
    [Serializable]
    public class template
    {
        public int template_id;
        public int max_supply;
        public bool is_transferable;
        public bool is_burnable;
        public int issued_supply;
        public Dictionary<string, string> mutable_data;
        public Dictionary<string, string> immutable_data;
        public string created_at_time;
        public string created_at_block;
    }
    public class backed_tokens
    {
        /*
        "token_contract": "string",
          "token_symbol": "string",
          "token_precision": 0,
          "amount": "string"
        */
    }

    [Serializable]
    public class format {
        public string name;
        public string type;
    }
}