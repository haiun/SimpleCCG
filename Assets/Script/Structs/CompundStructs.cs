using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using System.Linq;

namespace Compound
{
    public class CardData
    {
        public CardSO CardSO;
        public User.UserCard UserCard;

        public int Level { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int Exp { get; private set; }
        public int NextExp { get; private set; }

        public void OnUpdateUserCard(TableManager tableManager)
        {
            var cardLevel = tableManager.CardLevelList.LastOrDefault(c => c.Tier == UserCard.Tier && c.TotalExp <= UserCard.TotalExp);
            var cardTier = tableManager.CardTierList.FirstOrDefault(c => c.Tier == UserCard.Tier);

            Level = cardLevel.Level;

            //base stat ~ x2
            var statBonusPercentage = ((Level - 1) * 100) / (cardTier.MaxLevel - 1) + 100;
            Attack = (CardSO.Attack * statBonusPercentage) / 100;
            Defense = (CardSO.Defense * statBonusPercentage) / 100;

            Exp = UserCard.TotalExp - (cardLevel.TotalNextExp - cardLevel.NextExp);
            NextExp = cardLevel.NextExp;
        }
    }

    public static class CardDataExtension
    {
        private static StringBuilder sb = new StringBuilder();

        public static void ApplyCardIcon(this Image image, CardSO cardSO)
        {
            image.sprite = cardSO.Icon;
        }

        public static void ApplyCardLevel(this Text text, int level)
        {
            sb.Length = 0;
            sb.AppendFormat("Lv.{0:00}", level);
            text.text = sb.ToString();
        }

        public static void ApplyCardAtkStat(this Text text, int stat)
        {
            sb.Length = 0;
            sb.AppendFormat("ATK {0}", stat);
            text.text = sb.ToString();
        }

        public static void ApplyCardDefStat(this Text text, int stat)
        {
            sb.Length = 0;
            sb.AppendFormat("DEF {0}", stat);
            text.text = sb.ToString();
        }

        public static void ApplyCardIcon(this Image image, CardData model)
        {
            ApplyCardIcon(image, model.CardSO);
        }

        public static void ApplyCardLevel(this Text text, CardData model)
        {
            ApplyCardLevel(text, model.Level);
        }

        public static void ApplyCardAtkStat(this Text text, CardData model)
        {
            ApplyCardAtkStat(text, model.Attack);
        }

        public static void ApplyCardDefStat(this Text text, CardData model)
        {
            ApplyCardDefStat(text, model.Defense);
        }
    }
}