namespace Pixygon.NFT {
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