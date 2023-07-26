using System;
using System.Collections.Generic;

namespace Pixygon.NFT.Wax {
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
        public Dictionary<string, object> mutable_data;
        public Dictionary<string, object> immutable_data;
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
    public class collection {
        public string contract;
        public string collection_name;
        public string name;
        public string img;
        public string author;
        public bool allow_notify;
        public string[] authorized_accounts;
        public string[] notify_accounts;
        public float market_fee;
        public collectionData data;
        public string created_at_block;
        public string created_at_time;
    }

    [Serializable]
    public class collectionData {
        public string img;
        public string url;
        public string name;
        public string images;
        public string socials;
        public string description;
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
        public Dictionary<string, object> mutable_data;
        public Dictionary<string, object> immutable_data;
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