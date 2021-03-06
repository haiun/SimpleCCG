using System.Collections.Generic;
using System;

namespace User
{
    [Serializable]
    public class UserField
    {
        public List<UserCard> UserCardList = new List<UserCard>();

        public void Initialize()
        {
            UserCardList ??= new List<UserCard>();
        }
    }

    [Serializable]
    public class UserCard
    {
        public int UserCardId = 0;

        public int CardId = 0;
        public int TotalExp = 0;
        public int Tier = 0;
    }
}