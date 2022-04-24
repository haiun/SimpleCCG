using System.Collections.Generic;
using UnityEngine;

public class MainSceneInitData
{
    public TableManager TableManager = null;
    public UserManager UserManager = null;
    public CardListSO CardListSO = null;
}

[PrefabPath("Prefab/UI/MainScene")]
public class MainScene : MonoBehaviour
{
    private MainSceneInitData initData = null;

    public void Initialize(MainSceneInitData data)
    {
        this.initData = data;

        var newCardAssetList = new List<CCGAsset>();
        for (var i = 0; i < 20; ++i)
        {
            newCardAssetList.Add(data.UserManager.GetNewCard());
        }
    }

    public void OnClickGetCard()
    {
        var newCardAssetList = new List<CCGAsset>();
        for (var i = 0; i < 3; ++i)
        {
            newCardAssetList.Add(initData.UserManager.GetNewT1Card());
        }

        GetRewardPopup.CreatePopup(new GetRewardPopupInitData()
        {
            CardListSO = initData.CardListSO,
            CCGAssetList = newCardAssetList
        });
    }

    public void OnClickMyCard()
    {
        var myCardScene = GenericPrefab.Instantiate<MyCardScene>();
        myCardScene.Initialize(new MyCardSceneInitData()
        {
            UserManager = initData.UserManager
        });
    }

    public void OnClickLevelUp()
    {
        var selectTargetScene = GenericPrefab.Instantiate<SelectTargetScene>();
        selectTargetScene.Initialize(new SelectTargetSceneInitData()
        {
            UserManager = initData.UserManager,
            OnSelect = (myCardSlotData) =>
            {
                var myCardScene = GenericPrefab.Instantiate<LevelUpScene>();
                myCardScene.Initialize(new LevelUpSceneInitData()
                {
                    TableManager = initData.TableManager,
                    UserManager = initData.UserManager,
                    SelectedTargetData = myCardSlotData
                });
            }
        });

    }

    public void OnClickTierUp()
    {
        var myCardScene = GenericPrefab.Instantiate<TierUpScene>();
        myCardScene.Initialize(new TierUpSceneInitData()
        {
            CardListSO = initData.CardListSO,
            TableManager = initData.TableManager,
            UserManager = initData.UserManager
        });
    }
}
