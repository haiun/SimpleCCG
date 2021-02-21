using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTargetSceneInitData
{
    public UserManager UserManager = null;
    public Action<MyCardSlotData> OnSelect = null;
}

[PrefabPath("Prefab/UI/SelectTargetScene")]
public class SelectTargetScene : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup gridGroup = null;

    private SelectTargetSceneInitData initData = null;
    private MyCardSlot.Grid grid = null;

    public void Initialize(SelectTargetSceneInitData data)
    {
        initData = data;

        if (grid == null)
            grid = new MyCardSlot.Grid(CreateMyCardSlot, DestroyMyCardSlot);

        var cardDataList = data.UserManager.GetCardDataList();
        var myCardDataList = cardDataList.ConvertAll<MyCardSlotData>(d => new MyCardSlotData()
        {
            CardData = d,
            OnClickSlot = OnClickMyCardSlot
        });
        grid.ApplyList(myCardDataList);
    }

    private List<MyCardSlot> CreateMyCardSlot(List<MyCardSlotData> dataList)
    {
        var slotList = GenericPrefab.Instantiate<MyCardSlot>(gridGroup.transform, dataList.Count);
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

    public void OnClickMyCardSlot(MyCardSlotData data, MyCardSlot slot)
    {
        var popup = SelectCardPopup.CreatePopup(new SelectCardPopupInitData()
        {
            SelectedCardData = data,
            OnSelect = OnSelectCard
        });
    }

    private void OnSelectCard(MyCardSlotData data)
    {
        initData.OnSelect?.Invoke(data);
        Destroy(gameObject);
    }

    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}
