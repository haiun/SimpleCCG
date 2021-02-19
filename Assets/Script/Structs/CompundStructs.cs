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

        public static void ApplyCardIcon(this Image image, CardData model)
        {
            image.sprite = model.CardSO.Icon;
        }

        public static void ApplyCardLevel(this Text text, CardData model)
        {
            sb.Length = 0;
            sb.AppendFormat("Lv.{0:00}", model.Level);
            text.text = sb.ToString();
        }

        public static void ApplyCardAtkStat(this Text text, CardData model)
        {
            sb.Length = 0;
            sb.AppendFormat("ATK {0}", model.Attack);
            text.text = sb.ToString();
        }

        public static void ApplyCardDefStat(this Text text, CardData model)
        {
            sb.Length = 0;
            sb.AppendFormat("DEF {0}", model.Defense);
            text.text = sb.ToString();
        }
    }
}