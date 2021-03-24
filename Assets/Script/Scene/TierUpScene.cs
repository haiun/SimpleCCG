using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TierUpSceneInitData
{
    public CardListSO CardListSO = null;
    public TableManager TableManager = null;
    public UserManager UserManager = null;
}

[PrefabPath("Prefab/UI/TierUpScene")]
public class TierUpScene : MonoBehaviour
{
    [SerializeField]
    private Button TierUpButton = null;

    [SerializeField]
    private List<CardFrame> CardFrameList = null;

    [SerializeField]
    private RectTransform tierUpResultSlotLayoutGroup = null;
    private TierUpResultSlot.Grid tierUpResultSlotGrid = null;

    private TierUpSceneInitData initData = null;
    private List<Compound.CardData> materialCardDataList = new List<Compound.CardData>();

    public void Initialize(TierUpSceneInitData data)
    {
        initData = data;

        tierUpResultSlotGrid ??= new TierUpResultSlot.Grid(CreateTierUpResurltSlot, DestroyTierUpResultSlot);

        SetMaterialCardDataList(new List<Compound.CardData>());
    }

    private void SetMaterialCardDataList(List<Compound.CardData> materialCardDataList)
    {
        this.materialCardDataList = materialCardDataList;

        List<Table.CardTierUpWeight> weightList = null;

        if (materialCardDataList.Count == 2)
        {
            CardFrameList[0].gameObject.SetActive(true);
            CardFrameList[1].gameObject.SetActive(true);

            CardFrameList[0].ApplyCardData(materialCardDataList[0]);
            CardFrameList[1].ApplyCardData(materialCardDataList[1]);

            TierUpButton.interactable = true;

            weightList = initData.TableManager.GetTierUpWeight(
                materialCardDataList[0].UserCard.Tier,
                materialCardDataList[0].Level,
                materialCardDataList[1].UserCard.Tier,
                materialCardDataList[1].Level);
        }
        else
        {
            CardFrameList[0].gameObject.SetActive(false);
            CardFrameList[1].gameObject.SetActive(false);

            TierUpButton.interactable = false;

            weightList = new List<Table.CardTierUpWeight>();
        }

        tierUpResultSlotGrid.ApplyList(weightList);
    }

    private List<TierUpResultSlot> CreateTierUpResurltSlot(List<Table.CardTierUpWeight> dataList)
    {
        var slotList = GenericPrefab.Instantiate<TierUpResultSlot>(tierUpResultSlotLayoutGroup.transform, dataList.Count);
        for (var i = 0; i < dataList.Count; ++i)
        {
            slotList[i].SetData(dataList[i]);
        }
        return slotList;
    }

    private static void DestroyTierUpResultSlot(TierUpResultSlot slot)
    {
        Destroy(slot.gameObject);
    }

    public void OnClickSelectMaterial()
    {
        var selectMaterialScene = GenericPrefab.Instantiate<SelectMaterialScene>();
        selectMaterialScene.Initialize(new SelectMaterialSceneInitData()
        {
            UserManager = initData.UserManager,
            IgnoreCardDataList = new List<Compound.CardData>(),
            selectedCardDataList = new List<Compound.CardData>(),
            LimitCount = 2,
            FullSelect = true,
            OnSelect = SetMaterialCardDataList
        });
    }

    public void OnClickTierUp()
    {
        var newCardAsset = initData.UserManager.CardTierUp(materialCardDataList[0].UserCard.UserCardId, materialCardDataList[1].UserCard.UserCardId);

        GetRewardPopup.CreatePopup(new GetRewardPopupInitData()
        {
            CardListSO = initData.CardListSO,
            CCGAssetList = new List<CCGAsset>() { newCardAsset }
        });

        SetMaterialCardDataList(new List<Compound.CardData>());
    }

    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}
