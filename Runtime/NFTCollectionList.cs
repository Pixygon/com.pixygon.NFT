using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pixygon.NFT;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Pixygon.NFT {
    [CreateAssetMenu(fileName = "New NFTCollections", menuName = "Pixygon/NFTCollections")]
    [Serializable]
    public class NFTCollectionList : ScriptableObject {
        //DEPRECATED
        /*
        public NFTCollection[] NFTCollections;
        public AssetReference[] NFTCollectionReferences;


        [HideInInspector] public List<NFTCollection> collectionList = new List<NFTCollection>();
        [HideInInspector] public List<NFTData> ownedNfts = new List<NFTData>();
        [HideInInspector] public List<NFTCollection> ownedNftsCollections = new List<NFTCollection>();
        [HideInInspector] public NFTData[] allNfts;
        [HideInInspector] public waxAsset currentData;
        [HideInInspector] public NFTAsset currentAsset;
        [HideInInspector] public NFTCollection currentCollection;

        private bool isInitialized;

        public void Init() {
            ownedNfts = new List<NFTData>();
            ownedNftsCollections = new List<NFTCollection>();
            currentAsset = null;
            currentCollection = null;
            currentData = null;

            GetAllNfts();

            StringBuilder collectionWhiteList = new StringBuilder();
            collectionWhiteList.Append(collectionList[0].title);
            for(int i = 1; i < collectionList.Count; i++) {
                collectionWhiteList.Append(',');
                collectionWhiteList.Append(collectionList[i].title);
            }

            NFT.FetchAllAssets(Chain.Wax, OnFetchAllAssets, collectionWhiteList.ToString());
        }

        private void GetAllNfts() {
            List<NFTData> nftDatas = new List<NFTData>();
            foreach(NFTCollection nftCollection in NFTCollections) {
                if(nftCollection.nfts != null)
                    nftDatas.AddRange(nftCollection.nfts);
            }

            allNfts = nftDatas.ToArray();

            collectionList = new List<NFTCollection>();
            foreach(NFTCollection data in NFTCollections)
                collectionList.Add(data);
            collectionList.Reverse();
        }


        public NFTData GetNftFromWaxAsset(waxAsset data) {
            NFTData d = null;
            if(data.templateInfo.template != 0)
                d = allNfts.FirstOrDefault(nft => nft.NFTLink.Template != null && nft.NFTLink.Template.Length > 0 && nft.NFTLink.Template[0].template == data.templateInfo.template);
            else
                d = allNfts.FirstOrDefault(nft => nft.NFTLink.Template != null && nft.NFTLink.Template.Length > 0 && nft.NFTLink.Template[0].schema == data.templateInfo.schema);
            return d;
        }


        void OnFetchAllAssets(waxAsset[] assets) {
            ownedNfts.Clear();
            ownedNftsCollections.Clear();
            isInitialized = true;
            List<NFTAsset> nftAsset = new List<NFTAsset>();
            foreach(waxAsset w in assets) {
                NFTData d = GetNftFromWaxAsset(w);
                if(d != null) {
                    foreach(AssetData assetID in w.assets) {
                        NFTAsset b = new NFTAsset();
                        b.Asset = assetID;
                        b.Data = d;

                        nftAsset.Add(b);
                    }
                }
            }

            if(nftAsset.Count == 0)
                return;
            foreach(NFTAsset data in nftAsset) {
                ownedNfts.Add(data.Data);
                if(!ownedNftsCollections.Contains(data.Data.collection))
                    ownedNftsCollections.Add(data.Data.collection);
            }

            isInitialized = true;
        }
        */
    }
}