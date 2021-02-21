using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Compound;
using System;
using System.Linq;

public class LevelUpSceneInitData
{
    public TableManager TableManager = null;
    public UserManager UserManager = null;
    public MyCardSlotData SelectedTargetData = null;
}

[PrefabPath("Prefab/UI/LevelUpScene")]
public class LevelUpScene : MonoBehaviour
{
    [SerializeField]
    private LevelUpSceneView view = null;

    [SerializeField]
    private CardFrame previewCardFrame = null;

    [SerializeField]
    private RectTransform materialGridGroup = null;
    private MyCardSlot.Grid materialGrid = null;

    private LevelUpSceneInitData initData = null;

    private CardData previewCardData = null;
    private List<MyCardSlotData> materialCardSlotDataList = new List<MyCardSlotData>();
    private int addMaterialExp = 0;

    public void Initialize(LevelUpSceneInitData data)
    {
        initData = data;

        previewCardData = new CardData()
        {
            CardSO = initData.SelectedTargetData.CardData.CardSO,
            UserCard = new User.UserCard()
            {
                UserCardId = initData.SelectedTargetData.CardData.UserCard.UserCardId,
                CardId = initData.SelectedTargetData.CardData.UserCard.CardId,
                Tier = initData.SelectedTargetData.CardData.UserCard.Tier,
                TotalExp = initData.SelectedTargetData.CardData.UserCard.TotalExp
            }
        };
        previewCardData.OnUpdateUserCard(initData.TableManager);
        materialCardSlotDataList.Clear();
        addMaterialExp = 0;

        if (materialGrid == null) materialGrid = new MyCardSlot.Grid(CreateMyCardSlot, DestroyMyCardSlot);

        materialGrid.ApplyList(materialCardSlotDataList);
        UpdateAddExp();
    }

    private void UpdateAddExp()
    {
        var tableManager = initData.TableManager;
        addMaterialExp = 0;
        foreach (var materialCardSlotData in materialCardSlotDataList)
        {
            int tier = materialCardSlotData.CardData.UserCard.Tier;
            var levelTier = tableManager.CardTierList.FirstOrDefault(d => d.Tier == tier);
            addMaterialExp += levelTier.MaterialExp;
        }

        previewCardData.UserCard.TotalExp = initData.SelectedTargetData.CardData.UserCard.TotalExp + addMaterialExp;
        previewCardData.OnUpdateUserCard(tableManager);

        previewCardFrame.ApplyCardData(previewCardData);
        view.ApplyPreviewCardData(initData.SelectedTargetData.CardData, previewCardData, addMaterialExp);
    }

    private List<MyCardSlot> CreateMyCardSlot(List<MyCardSlotData> dataList)
    {
        var slotList = GenericPrefab.Instantiate<MyCardSlot>(materialGridGroup.transform, dataList.Count);
        for (int i = 0; i < dataList.Count; ++i)
        {
            slotList[i].SetData(dataList[i]);
        }
        return slotList;
    }

    private void DestroyMyCardSlot(MyCardSlot slot)
    {
        Destroy(slot.gameObject);
    }

    private void OnClickMyCardSlot(MyCardSlotData data, MyCardSlot slot)
    {
        materialCardSlotDataList.Remove(data);
        materialGrid.ApplyList(materialCardSlotDataList);

        UpdateAddExp();
    }

    public void OnClickAddMaterial()
    {
        var selectMaterialScene = GenericPrefab.Instantiate<SelectMaterialScene>();
        selectMaterialScene.Initialize(new SelectMaterialSceneInitData()
        {
            UserManager = initData.UserManager,
            IgnoreCardDataList = new List<Compound.CardData> { initData.SelectedTargetData.CardData },
            selectedCardDataList = materialCardSlotDataList.ConvertAll(d => d.CardData),
            LimitCount = 10,
            FullSelect = false,
            OnSelect = (result) =>
            {
                materialCardSlotDataList = result.ConvertAll(d => new MyCardSlotData() { CardData = d, OnClickSlot = OnClickMyCardSlot });
                materialGrid.ApplyList(materialCardSlotDataList);

                UpdateAddExp();
            }
        });
    }

    public void OnClickClearMaterial()
    {
        materialCardSlotDataList.Clear();
        materialGrid.ApplyList(materialCardSlotDataList);

        UpdateAddExp();
    }

    public void OnClickLevelUp()
    {
        int targetUserCardId = initData.SelectedTargetData.CardData.UserCard.UserCardId;
        List<int> materialUserCardIdList = materialCardSlotDataList.ConvertAll(d => d.CardData.UserCard.UserCardId);

        initData.UserManager.CardLevelUp(targetUserCardId, materialUserCardIdList);

        initData.SelectedTargetData.CardData.OnUpdateUserCard(initData.TableManager);
        Initialize(initData);
    }

    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}

[Serializable]
public class LevelUpSceneView
{
    public Text AttackStat = null;
    public Text DefenseStat = null;

    public Text ExpCurrentPerNext = null;
    public Scrollbar CurrentExp = null;
    public Scrollbar NextExp = null;

    public Text AddExp = null;

    public List<Text> ChangeTextColorByLevel = null;
    public Color NormalColor;
    public Color ChangedColor;

    public Button LevelUpButton = null;

    public void ApplyPreviewCardData(CardData originalCardData, CardData previewCardData, int addExp)
    {
        AttackStat.ApplyCardAtkStat(previewCardData);
        DefenseStat.ApplyCardDefStat(previewCardData);

        if (originalCardData.NextExp == 0)
        {
            ExpCurrentPerNext.text = "MAX";
        }
        else
        {
            ExpCurrentPerNext.text = string.Format("{0}/{1}", originalCardData.Exp + addExp, originalCardData.NextExp);
        }

        float currentRatio = (float)originalCardData.Exp / (float)originalCardData.NextExp;
        float nextRatio = Mathf.Min((float)(originalCardData.Exp + addExp) / (float)originalCardData.NextExp, 1f);

        CurrentExp.gameObject.SetActive(currentRatio > 0);
        CurrentExp.size = currentRatio;

        NextExp.size = nextRatio;
        NextExp.gameObject.SetActive(nextRatio > 0);

        AddExp.text = string.Format("+{0} EXP", addExp);

        foreach (var text in ChangeTextColorByLevel)
        {
            text.color = originalCardData.Level == previewCardData.Level ? NormalColor : ChangedColor;
        }

        LevelUpButton.interactable = addExp > 0;
    }
}
