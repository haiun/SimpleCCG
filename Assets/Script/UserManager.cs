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

    UserManagerInitData initData = null;

    public void Initialize(UserManagerInitData data)
    {
        this.initData = data;
        userField ??= new UserField();
    }

    public void Load()
    {
        var json = PlayerPrefs.GetString(key);
        userField = JsonUtility.FromJson<UserField>(json) ?? new UserField();
        userField.Initialize();
    }

    public void Save()
    {
        var json = JsonUtility.ToJson(userField);
        PlayerPrefs.SetString(key, json);
    }

    public List<CardData> GetCardDataList()
    {
        var ret = userField.UserCardList.ConvertAll<CardData>(u => new CardData()
        {
            CardSO = initData.CardListSO.List.FirstOrDefault(c => c.CardId == u.CardId),
            UserCard = u
        });
        ret.ForEach(m => m.OnUpdateUserCard(initData.TableManager));
        return ret;
    }

    public CCGAsset GetNewCard()
    {
        var randIndex = Random.Range(0, initData.CardListSO.List.Count);
        var newCardSO = initData.CardListSO.List[randIndex];
        int newUserId = userField.UserCardList.Count > 0 ? userField.UserCardList.Max(m => m.UserCardId) + 1 : 1;

        userField.UserCardList.Add(new UserCard()
        {
            UserCardId = newUserId,
            CardId = newCardSO.CardId,
            Tier = newCardSO.Tier,
            TotalExp = 0
        });

        return new CCGAsset()
        {
            AssetType = CCGAssetType.Card,
            Id = newCardSO.CardId,
            Count = 1
        };
    }

    public CCGAsset GetNewT1Card()
    {
        var t1List = initData.CardListSO.List.Where(w => w.Tier == 1).ToList();
        var randIndex = Random.Range(0, t1List.Count);
        var newCardSO = t1List[randIndex];
        var newUserId = userField.UserCardList.Count > 0 ? userField.UserCardList.Max(m => m.UserCardId) + 1 : 1;

        userField.UserCardList.Add(new UserCard()
        {
            UserCardId = newUserId,
            CardId = newCardSO.CardId,
            Tier = newCardSO.Tier,
            TotalExp = 0
        });

        return new CCGAsset()
        {
            AssetType = CCGAssetType.Card,
            Id = newCardSO.CardId,
            Count = 1
        };
    }

    public void CardLevelUp(int targetUserCardId, List<int> materialCardUserIdList)
    {
        var targetUserCard = userField.UserCardList.FirstOrDefault(t => t.UserCardId == targetUserCardId);

        var sumExp = 0;
        foreach (var materialUserCardId in materialCardUserIdList)
        {
            var materialUserCard = userField.UserCardList.FirstOrDefault(u => u.UserCardId == materialUserCardId);
            var cardTier = initData.TableManager.CardTierList.FirstOrDefault(t => t.Tier == materialUserCard.Tier);
            sumExp += cardTier.MaterialExp;

            userField.UserCardList.Remove(materialUserCard);
        }

        targetUserCard.TotalExp += sumExp;
    }

    public CCGAsset CardTierUp(int userCardId1, int userCardId2)
    {
        var tableManager = initData.TableManager;

        var userCard1 = userField.UserCardList.FirstOrDefault(u => u.UserCardId == userCardId1);
        var userCard2 = userField.UserCardList.FirstOrDefault(u => u.UserCardId == userCardId2);

        var cardLevel1 = tableManager.CardLevelList.LastOrDefault(c => c.Tier == userCard1.Tier && c.TotalExp <= userCard1.TotalExp);
        var cardLevel2 = tableManager.CardLevelList.LastOrDefault(c => c.Tier == userCard2.Tier && c.TotalExp <= userCard2.TotalExp);

        var tierUpWeight = tableManager.GetTierUpWeight(userCard1.Tier, cardLevel1.Level, userCard2.Tier, cardLevel2.Level);
        var rand = Random.Range(0, 100);
        var tier = tierUpWeight[0].Tier;
        if (tierUpWeight[0].Weight < rand)
        {
            tier = tierUpWeight[1].Tier;
        }

        var tierCardIdList = initData.CardListSO.List.Where(w => w.Tier == tier).ToList();
        Debug.Log(tierCardIdList.Count);
        var randIndex = Random.Range(0, tierCardIdList.Count);
        Debug.Log(randIndex);
        var newCardSO = tierCardIdList[randIndex];
        Debug.Log(newCardSO);
        var newUserId = userField.UserCardList.Count > 0 ? userField.UserCardList.Max(m => m.UserCardId) + 1 : 1;

        userField.UserCardList.Add(new UserCard()
        {
            UserCardId = newUserId,
            CardId = newCardSO.CardId,
            Tier = newCardSO.Tier,
            TotalExp = 0
        });

        userField.UserCardList.Remove(userCard1);
        userField.UserCardList.Remove(userCard2);

        return new CCGAsset()
        {
            AssetType = CCGAssetType.Card,
            Id = newCardSO.CardId,
            Count = 1
        };
    }
}
