using System.Collections;
using System.Collections.Generic;
using Table;

public class TableManager
{
    public List<CardLevel> CardLevelList = null;
    public List<CardTier> CardTierList = null;
    public List<CardTierUp> CardTierUpList = null;

    public void Initialize()
    {
        #region CardLevel
        CardLevelList = new List<CardLevel>();
        CardLevelList.Add(new CardLevel() { BaseTier = 1, Level = 1, Exp = 100 });
        CardLevelList.Add(new CardLevel() { BaseTier = 1, Level = 2, Exp = 100 });
        CardLevelList.Add(new CardLevel() { BaseTier = 1, Level = 3, Exp = 100 });
        CardLevelList.Add(new CardLevel() { BaseTier = 1, Level = 4, Exp = 100 });
        CardLevelList.Add(new CardLevel() { BaseTier = 1, Level = 5, Exp = 100 });
        CardLevelList.Add(new CardLevel() { BaseTier = 2, Level = 1, Exp = 500 });
        CardLevelList.Add(new CardLevel() { BaseTier = 2, Level = 2, Exp = 500 });
        CardLevelList.Add(new CardLevel() { BaseTier = 2, Level = 3, Exp = 500 });
        CardLevelList.Add(new CardLevel() { BaseTier = 2, Level = 4, Exp = 500 });
        CardLevelList.Add(new CardLevel() { BaseTier = 2, Level = 5, Exp = 500 });
        CardLevelList.Add(new CardLevel() { BaseTier = 2, Level = 6, Exp = 500 });
        CardLevelList.Add(new CardLevel() { BaseTier = 2, Level = 7, Exp = 500 });
        CardLevelList.Add(new CardLevel() { BaseTier = 2, Level = 8, Exp = 500 });
        CardLevelList.Add(new CardLevel() { BaseTier = 2, Level = 9, Exp = 500 });
        CardLevelList.Add(new CardLevel() { BaseTier = 2, Level = 10, Exp = 500 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 1, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 2, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 3, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 4, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 5, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 6, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 7, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 8, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 9, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 10, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 11, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 12, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 13, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 14, Exp = 1000 });
        CardLevelList.Add(new CardLevel() { BaseTier = 3, Level = 15, Exp = 1000 });
        #endregion
        #region CardTier
        CardTierList = new List<CardTier>();
        CardTierList.Add(new CardTier() { Tier = 1, MaxLevel = 5, MaterialExp = 100 });
        CardTierList.Add(new CardTier() { Tier = 2, MaxLevel = 10, MaterialExp = 5000 });
        CardTierList.Add(new CardTier() { Tier = 3, MaxLevel = 15, MaterialExp = 25000 });
        #endregion
        #region CardTierUp
        CardTierUpList = new List<CardTierUp>();
        CardTierUpList.Add(new CardTierUp()
        {
            TierSum = 2,
            WeightList = new List<CardTierUpWeight>()
            {
                new CardTierUpWeight() { Tier = 1, Weight = 100 }
            }
        });
        CardTierUpList.Add(new CardTierUp()
        {
            TierSum = 3,
            WeightList = new List<CardTierUpWeight>()
            {
                new CardTierUpWeight() { Tier = 2, Weight = 33 },
                new CardTierUpWeight() { Tier = 1, Weight = 67 }
            }
        });
        CardTierUpList.Add(new CardTierUp()
        {
            TierSum = 4,
            WeightList = new List<CardTierUpWeight>()
            {
                new CardTierUpWeight() { Tier = 2, Weight = 50 },
                new CardTierUpWeight() { Tier = 1, Weight = 50 }
            }
        });
        CardTierUpList.Add(new CardTierUp()
        {
            TierSum = 5,
            WeightList = new List<CardTierUpWeight>()
            {
                new CardTierUpWeight() { Tier = 3, Weight = 33 },
                new CardTierUpWeight() { Tier = 2, Weight = 33 },
                new CardTierUpWeight() { Tier = 1, Weight = 33 }
            }
        });
        CardTierUpList.Add(new CardTierUp()
        {
            TierSum = 6,
            WeightList = new List<CardTierUpWeight>()
            {
                new CardTierUpWeight() { Tier = 3, Weight = 50 },
                new CardTierUpWeight() { Tier = 2, Weight = 50 }
            }
        });
        #endregion
    }
}
