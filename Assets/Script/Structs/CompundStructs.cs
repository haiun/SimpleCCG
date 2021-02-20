using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

namespace Compound
{
    public class CardData
    {
        public CardSO CardSO;
        public User.UserCard UserCard;

        public int Level { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }

        public void OnUpdateUserCard(TableManager tableManager)
        {

        }
    }

    public static class CardDataExtention
    {
        public static StringBuilder sb = new StringBuilder();

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