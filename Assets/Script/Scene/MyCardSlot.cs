using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyCardSlotData
{
    public Compound.CardData CardData = null;
    public Action<MyCardSlotData, MyCardSlot> OnClickSlot = null;
}

[PrefabPath("Prefab/UI/MyCardSlot")]
public class MyCardSlot : GenericSlot<MyCardSlotData, MyCardSlot>
{
    [SerializeField]
    private CardFrame cardFrame = null;

    protected override void OnSetData(MyCardSlotData data)
    {
        cardFrame.ApplyCardData(data.CardData);
    }

    public void OnClickSlot()
    {
        Data?.OnClickSlot?.Invoke(Data, this);
    }
}
