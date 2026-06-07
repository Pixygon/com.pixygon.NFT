# Pixygon — NFT

The NFT viewer/system: links in-game content (skins, items, equipment) to NFTs, and
gates content by ownership. The premium-ownership layer beneath cosmetics.

## Key types

| Type | What it is |
|---|---|
| **`NFT` / `NFTObject`** | NFT model + asset wrapper. |
| **`NFTMinting`** | Minting flow. |
| **`NFTEquipment`** | NFT-backed equippables. |
| **`NFTCollectionList`** | A collection of NFTs. |
| **`NFTRandomizer`** | Randomized/gacha selection. |
| **`NFTAccessLock`** | Gate content/features by NFT ownership. |
| **`Actions/Nft*` (`NftActionTween`/`Scale`/`Position`/`Asset`)** | Driven presentation actions for NFT objects. |

## Dependencies

None declared.

## Usage

`micro.SkinCard._nftLink` ties a collectible skin to an NFT; `NFTAccessLock` unlocks
gated content for owners.

## Status

`0.5.x`. The ownership/collectible substrate for premium cosmetics (avatar parts,
skins). Ties into the account/ItemBox (`passport`) for what a player actually owns.
