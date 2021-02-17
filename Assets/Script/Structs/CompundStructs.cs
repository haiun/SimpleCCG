using System.Collections;
using System.Collections.Generic;

namespace Compound
{
    public class CardData
    {
        public Table.Card Card;
        public User.UserCard UserCard;

        public int Level { get; private set; }
        public int Attack { get; private set; }
        public int Defanse { get; private set; }
    }
}