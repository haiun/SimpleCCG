using System.Collections;
using System.Collections.Generic;

namespace Table
{
    public class Money
    {
        public int MoneyId;
        public string Name;
        public string Icon;
    }

    public class Card
    {
        public int CardId;
        public string Name;
        public string Icon;

        public int BaseTier;
        public int BaseAttackRatio;
        public int BaseDefenseRatio;
    }

    public class CardLevel
    {
        public int BaseTier;
        public int Exp;

        public int Level;
    }

    public class CardMaxLevel
    {
        public int Tier;
        public int MaxLevel;
    }

    public class PackageItem
    {
        public int PackageItemId;
        public string Name;
        public string Icon;
        public List<Asset> AssetList;
    }
}