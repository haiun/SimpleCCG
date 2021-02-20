using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardData
{
    public CardSO CardSO = null;
}

[PrefabPath("Prefab/UI/RewardSlot")]
public class RewardSlot : GenericSlot<RewardData, RewardSlot>
{
    [SerializeField]
    private CardFrame cardFrame = null;

    protected override void OnSetData(RewardData data)
    {
        cardFrame.ApplyRewardData(data);
    }

    public void OnClickSlot()
    {

    }
}
