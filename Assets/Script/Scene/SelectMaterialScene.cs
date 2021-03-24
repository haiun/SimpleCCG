using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;

public class SelectMaterialSceneInitData
{
    public UserManager UserManager = null;
    public List<Compound.CardData> IgnoreCardDataList = null;
    public List<Compound.CardData> selectedCardDataList = null;
    public int LimitCount = 0;
    public bool FullSelect = false;
    public Action<List<Compound.CardData>> OnSelect = null;
}

[PrefabPath("Prefab/UI/SelectMaterialScene")]
public class SelectMaterialScene : MonoBehaviour
{
    [SerializeField]
    private SelectMaterialSceneView view = null;
    [SerializeField]
    private GridLayoutGroup gridGroup = null;

    private SelectMaterialSceneInitData initData = null;
    private MaterialCardSlot.Grid grid = null;

    private readonly List<MaterialCardSlotData> selectedCardDataList = new List<MaterialCardSlotData>();

    public void Initialize(SelectMaterialSceneInitData data)
    {
        initData = data;

        grid ??= new MaterialCardSlot.Grid(CreateMaterialCardSlot, DestroyMaterialCardSlot);

        var cardDataList = data.UserManager.GetCardDataList();
        cardDataList.RemoveAll(r => initData.IgnoreCardDataList.Exists(e => e.UserCard.UserCardId == r.UserCard.UserCardId));
        var myCardDataList = cardDataList.ConvertAll<MaterialCardSlotData>(d => new MaterialCardSlotData()
        {
            CardData = d,
            SelectedIndex = 0,
            OnClickSlot = OnClickMaterialCardSlot
        });

        var selectedIndex = 0;
        foreach (var selectedCardData in initData.selectedCardDataList)
        {
            selectedIndex++;

            var materialSlotData = myCardDataList.FirstOrDefault(d => d.CardData.UserCard.UserCardId == selectedCardData.UserCard.UserCardId);
            materialSlotData.SelectedIndex = selectedIndex;
            selectedCardDataList.Add(materialSlotData);
        }

        grid.ApplyList(myCardDataList);
        view.ApplySelectCount(selectedCardDataList.Count, initData.LimitCount, initData.FullSelect);
    }

    private List<MaterialCardSlot> CreateMaterialCardSlot(List<MaterialCardSlotData> dataList)
    {
        var slotList = GenericPrefab.Instantiate<MaterialCardSlot>(gridGroup.transform, dataList.Count);
        for (var i = 0; i < dataList.Count; ++i)
        {
            slotList[i].SetData(dataList[i]);
        }
        return slotList;
    }

    private void DestroyMaterialCardSlot(MaterialCardSlot slot)
    {
        Destroy(slot.gameObject);
    }

    private void OnClickMaterialCardSlot(MaterialCardSlotData data, MaterialCardSlot slot)
    {
        var selected = selectedCardDataList.FirstOrDefault(d => d.CardData.UserCard.UserCardId == data.CardData.UserCard.UserCardId);
        if (selected == null)
        {
            if (initData.LimitCount <= selectedCardDataList.Count)
            {
                return;
            }

            selectedCardDataList.Add(data);
        }
        else
        {
            selectedCardDataList.RemoveAll(r => r.CardData.UserCard.UserCardId == data.CardData.UserCard.UserCardId);
            data.SelectedIndex = 0;
        }
        UpdateCardSelection();
    }

    public void OnClickSelect()
    {
        initData.OnSelect?.Invoke(selectedCardDataList.ConvertAll(d => d.CardData));
        Destroy(gameObject);
    }

    public void OnClickClose()
    {
        Destroy(gameObject);
    }

    private void UpdateCardSelection()
    {
        for (var i = 0; i < selectedCardDataList.Count; ++i)
        {
            selectedCardDataList[i].SelectedIndex = i + 1;
        }

        foreach (var materialSlot in grid.SlotList)
        {
            materialSlot.Invalidate();
        }

        view.ApplySelectCount(selectedCardDataList.Count, initData.LimitCount, initData.FullSelect);
    }
}

[Serializable]
public class SelectMaterialSceneView
{
    public Text Count = null;
    public Button SelectButton = null;

    private StringBuilder sb = new StringBuilder();
    public void ApplySelectCount(int selectedCount, int maxCount, bool buttonLockable)
    {
        sb.Length = 0;
        sb.AppendFormat("{0}/{1}", selectedCount, maxCount);
        Count.text = sb.ToString();

        if (buttonLockable)
        {
            SelectButton.interactable = selectedCount == maxCount;
        }
    }
}
