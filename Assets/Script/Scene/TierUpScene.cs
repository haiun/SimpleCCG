using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private RectTransform tierUpResurltSlotLayoutGroup = null;
    private TierUpResultSlot.Grid tierUpResurltSlotGrid = null;

    private TierUpSceneInitData initData = null;
    private List<Compound.CardData> mateiralCardDataList = new List<Compound.CardData>();

    public void Initialize(TierUpSceneInitData data)
    {
        initData = data;

        if (tierUpResurltSlotGrid == null) tierUpResurltSlotGrid = new TierUpResultSlot.Grid(CreateTierUpResurltSlot, DestroyTierUpResurltSlot);

        SetMateiralCardDataList(new List<Compound.CardData>());
    }

    private void SetMateiralCardDataList(List<Compound.CardData> mateiralCardDataList)
    {
        this.mateiralCardDataList = mateiralCardDataList;

        List<Table.CardTierUpWeight> weightList = null;

        if (mateiralCardDataList.Count == 2)
        {
            CardFrameList[0].gameObject.SetActive(true);
            CardFrameList[1].gameObject.SetActive(true);

            CardFrameList[0].ApplyCardData(mateiralCardDataList[0]);
            CardFrameList[1].ApplyCardData(mateiralCardDataList[1]);

            TierUpButton.interactable = true;

            weightList = initData.TableManager.GetTierUpWeight(
                mateiralCardDataList[0].UserCard.Tier,
                mateiralCardDataList[0].Level,
                mateiralCardDataList[1].UserCard.Tier,
                mateiralCardDataList[1].Level);
        }
        else
        {
            CardFrameList[0].gameObject.SetActive(false);
            CardFrameList[1].gameObject.SetActive(false);

            TierUpButton.interactable = false;

            weightList = new List<Table.CardTierUpWeight>();
        }

        tierUpResurltSlotGrid.ApplyList(weightList);
    }

    private List<TierUpResultSlot> CreateTierUpResurltSlot(List<Table.CardTierUpWeight> dataList)
    {
        var slotList = GenericPrefab.Instantiate<TierUpResultSlot>(tierUpResurltSlotLayoutGroup.transform, dataList.Count);
        for (int i = 0; i < dataList.Count; ++i)
        {
            slotList[i].SetData(dataList[i]);
        }
        return slotList;
    }

    private void DestroyTierUpResurltSlot(TierUpResultSlot slot)
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
            OnSelect = SetMateiralCardDataList
        });
    }

    public void OnClickTierUp()
    {
        var newCardAsset = initData.UserManager.CardTierUp(mateiralCardDataList[0].UserCard.UserCardId, mateiralCardDataList[1].UserCard.UserCardId);

        GetRewardPopup.CreatePopup(new GetRewardPopupInitData()
        {
            CardListSO = initData.CardListSO,
            CCGAssetList = new List<CCGAsset>() { newCardAsset }
        });

        SetMateiralCardDataList(new List<Compound.CardData>());
    }

    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}
