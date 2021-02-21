using System.Collections;
using System.Collections.Generic;

namespace Table
{
    public class Card
    {
        public int CardId;

        public string Name;
        public string Icon;
        public int BaseTier;
    }

    public class CardLevel
    {
        public int Tier;
        public int Level;

        public int TotalExp;
        public int NextExp;
        public int TotalNextExp;
    }

    public class CardTier
    {
        public int Tier;

        public int MaxLevel;
        public int MaterialExp;
    }

    public class CardTierUpWeight
    {
        public int Tier;
        public int Weight;
    }

    public class PackageItem
    {
        public int PackageItemId;
        public string Name;
        public string Icon;
        public List<CCGAsset> AssetList;
    }
}