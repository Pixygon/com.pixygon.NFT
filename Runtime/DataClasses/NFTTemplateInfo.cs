using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Pixygon.NFT {
    [Serializable]
    public class NFTTemplateInfo {
        [ContextMenuItem("Get template", "GetTemplate")]
        public int template = -1;
        public string schema = string.Empty;
        public string collection = string.Empty;
        public Chain chain = Chain.Wax;

        public NFTTemplateInfo(int template = -1, string schema = "", string collection = "", Chain chain = Chain.Wax) {
            this.template = template;
            this.schema = schema;
            this.collection = collection;
            this.chain = chain;
        }

        public async Task GetTemplate() {
            if(template == 0)
                return;
            var t = await NFT.GetTemplate(template);
            if(t == null)
                Debug.Log("Wrong template");
            else {
                collection = t.CollectionName;
                //schema = t.schema.schema_name;
            }
        }
    }

    public enum Chain {
        Wax = 0,
        EOS = 1,
        Ethereum = 2,
        Tezos = 3,
        Polygon = 4,
        Polkadot = 5,
        Elrond = 6,
        BinanceChain = 7,
        Cardano = 8,
        Stellar = 9,
        Neo = 10,
        HyperledgerFabric = 11,
        Waves = 12,
        Cosmos = 13,
        Ripple = 14,
        Nem = 15,
        Solana = 16,
        Hive = 17,
        Phantom = 18,
        Flow = 19
    }
}