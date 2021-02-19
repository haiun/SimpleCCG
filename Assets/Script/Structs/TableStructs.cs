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
        public int BaseTier;
        public int Level;

        public int Exp;
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

    public class CardTierUp
    {
        public int TierSum;
        public List<CardTierUpWeight> WeightList;
    }

    public class PackageItem
    {
        public int PackageItemId;
        public string Name;
        public string Icon;
        public List<CCGAsset> AssetList;
    }
}