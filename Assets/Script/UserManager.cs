using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using User;
using Compound;

public class UserManagerInitData
{
    public TableManager TableManager = null;
    public CardListSO CardListSO = null;
}

public class UserManager
{
    private readonly string key = "SAVE";
    private UserField userField = null;

    UserManagerInitData data = null;

    public void Initialzie(UserManagerInitData data)
    {
        this.data = data;
        if (userField == null) userField = new UserField();
    }

    public void Load()
    {
        var json = PlayerPrefs.GetString(key);
        userField = JsonUtility.FromJson<UserField>(json);
        if (userField == null) userField = new UserField();
        userField.Initialize();
    }

    public void Save()
    {
        var json = JsonUtility.ToJson(userField);
        PlayerPrefs.SetString(key, json);
    }

    public List<CardData> GetUserCardDataList()
    {
        var ret = userField.UserCardList.ConvertAll<CardData>(u => new CardData()
        {
            CardSO = data.CardListSO.List.FirstOrDefault(c => c.CardId == u.CardId),
            UserCard = u
        });
        ret.ForEach(m => m.OnUpdateUserCard(data.TableManager));
        return ret;
    }

    public CCGAsset GetNewT1Card()
    {
        var t1List = data.CardListSO.List.Where(w => w.Tier == 1).ToList();
        int randIndex = Random.Range(0, t1List.Count);
        var newCardSO = t1List[randIndex];
        int newUserId = userField.UserCardList.Count > 0 ? userField.UserCardList.Max(m => m.UserCardId) + 1 : 1;

        userField.UserCardList.Add(new UserCard()
        {
            UserCardId = newUserId,
            CardId = newCardSO.CardId,
            Tier = newCardSO.Tier,
            Exp = 0
        });

        return new CCGAsset()
        {
            AssetType = CCGAssetType.Card,
            Id = newCardSO.CardId,
            Count = 1
        };
    }
}
