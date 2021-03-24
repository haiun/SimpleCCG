using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Table;

public class TableManager
{
    public List<CardLevel> CardLevelList = null;
    public List<CardTier> CardTierList = null;

    public void Initialize()
    {
        #region CardLevel
        CardLevelList = new List<CardLevel>();
        CardLevelList.Add(new CardLevel() { Tier = 1, Level = 1, NextExp = 100 });
        CardLevelList.Add(new CardLevel() { Tier = 1, Level = 2, NextExp = 100 });
        CardLevelList.Add(new CardLevel() { Tier = 1, Level = 3, NextExp = 100 });
        CardLevelList.Add(new CardLevel() { Tier = 1, Level = 4, NextExp = 100 });
        CardLevelList.Add(new CardLevel() { Tier = 1, Level = 5, NextExp = 0 });
        CardLevelList.Add(new CardLevel() { Tier = 2, Level = 1, NextExp = 500 });
        CardLevelList.Add(new CardLevel() { Tier = 2, Level = 2, NextExp = 500 });
        CardLevelList.Add(new CardLevel() { Tier = 2, Level = 3, NextExp = 500 });
        CardLevelList.Add(new CardLevel() { Tier = 2, Level = 4, NextExp = 500 });
        CardLevelList.Add(new CardLevel() { Tier = 2, Level = 5, NextExp = 500 });
        CardLevelList.Add(new CardLevel() { Tier = 2, Level = 6, NextExp = 500 });
        CardLevelList.Add(new CardLevel() { Tier = 2, Level = 7, NextExp = 500 });
        CardLevelList.Add(new CardLevel() { Tier = 2, Level = 8, NextExp = 500 });
        CardLevelList.Add(new CardLevel() { Tier = 2, Level = 9, NextExp = 500 });
        CardLevelList.Add(new CardLevel() { Tier = 2, Level = 10, NextExp = 0 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 1, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 2, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 3, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 4, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 5, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 6, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 7, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 8, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 9, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 10, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 11, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 12, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 13, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 14, NextExp = 1000 });
        CardLevelList.Add(new CardLevel() { Tier = 3, Level = 15, NextExp = 0 });

        {
            int totalExp = 0;
            foreach (var c in CardLevelList)
            {
                c.TotalExp = totalExp;
                totalExp += c.NextExp;
                c.TotalNextExp = totalExp;
                if (c.NextExp == 0) totalExp = 0;
            }
        }

        #endregion
        #region CardTier
        CardTierList = new List<CardTier>();
        CardTierList.Add(new CardTier() { Tier = 1, MaxLevel = 5, MaterialExp = 50 });
        CardTierList.Add(new CardTier() { Tier = 2, MaxLevel = 10, MaterialExp = 100 });
        CardTierList.Add(new CardTier() { Tier = 3, MaxLevel = 15, MaterialExp = 5000 });
        #endregion
        #region CardTierUp
        #endregion
    }

    public List<CardTierUpWeight> GetTierUpWeight(int tier1, int level1, int tier2, int level2)
    {
        var cardTier1 = CardTierList.FirstOrDefault(t => t.Tier == tier1);
        var cardTier2 = CardTierList.FirstOrDefault(t => t.Tier == tier2);
        UnityEngine.Debug.LogFormat("tier {0} {1}", tier1, tier2);
        UnityEngine.Debug.LogFormat("level {0} {1}", level1, level2);

        var weight1 = ((level1) * 100) / (cardTier1.MaxLevel) + (tier1 - 1) * 100;
        var weight2 = ((level2) * 100) / (cardTier2.MaxLevel) + (tier2 - 1) * 100;
        UnityEngine.Debug.LogFormat("weight {0} {1}", weight1, weight2);

        var upTier100 = (weight1 + weight2) / 2;
        UnityEngine.Debug.LogFormat("up tier {0}", upTier100);

        var baseTier = upTier100 / 100 + 1;
        var upRatio = upTier100 % 100;

        if (upRatio == 0)
        {
            return new List<CardTierUpWeight>()
            {
                new CardTierUpWeight() { Tier = baseTier, Weight = 100 }
            };
        }
        else
        {
            var highTier = System.Math.Min(baseTier + 1, 3);

            if (baseTier == highTier)
            {
                return new List<CardTierUpWeight>()
                {
                    new CardTierUpWeight() { Tier = baseTier, Weight = 100 }
                };
            }
            else
            {
                return new List<CardTierUpWeight>()
                {
                    new CardTierUpWeight() { Tier = baseTier, Weight = 100 - upRatio },
                    new CardTierUpWeight() { Tier = highTier, Weight = upRatio }
                };
            }
        }
    }
}
