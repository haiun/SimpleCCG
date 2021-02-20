using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MyCardSceneInitData
{
    public UserManager UserManager = null;
}

[PrefabPath("Prefab/UI/MyCardScene")]
public class MyCardScene : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup gridGroup = null;
    
    private MyCardSceneInitData data = null;
    private MyCardSlot.Grid grid = null;

    public void Initialize(MyCardSceneInitData data)
    {
        this.data = data;

        if (grid == null)
            grid = new MyCardSlot.Grid(CreateMyCardSlot, DestroyMyCardSlot);

        var cardDataList = data.UserManager.GetUserCardDataList();
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
        Debug.Log(data.CardData.UserCard.UserCardId);
    }

    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}
