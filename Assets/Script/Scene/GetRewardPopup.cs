using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetRewardPopupInitData
{
    public CardListSO CardListSO = null;
    public List<CCGAsset> CCGAssetList = null;
}

[PrefabPath("Prefab/UI/GetRewardPopup")]
public class GetRewardPopup : MonoBehaviour
{
    [SerializeField]
    private RectTransform gridGroup = null;

    private RewardSlot.Grid rewardSlotGird = null;

    private GetRewardPopupInitData data = null;

    public static GetRewardPopup CreatePopup(GetRewardPopupInitData data)
    {
        var popup = GenericPrefab.Instantiate<GetRewardPopup>();
        popup.Initialize(data);
        return popup;
    }

    private void Initialize(GetRewardPopupInitData data)
    {
        this.data = data;
        rewardSlotGird ??= new RewardSlot.Grid(CreateRewardSlot, DestroyRewardSlot);

        var rewardDataList = data.CCGAssetList.ConvertAll<RewardData>(d => new RewardData()
        {
            CardSO = data.CardListSO.List.FirstOrDefault(so => so.CardId == d.Id)
        });
        rewardSlotGird.ApplyList(rewardDataList);
    }

    private List<RewardSlot> CreateRewardSlot(List<RewardData> dataList)
    {
        var slotList = GenericPrefab.Instantiate<RewardSlot>(gridGroup.transform, dataList.Count);
        for (var i = 0; i < dataList.Count; ++i)
        {
            slotList[i].SetData(dataList[i]);
        }
        return slotList;
    }

    private void DestroyRewardSlot(RewardSlot slot)
    {
        Destroy(slot.gameObject);
    }

    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}
