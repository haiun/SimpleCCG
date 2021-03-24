using System.Collections;
using System.Collections.Generic;
using System;
using Table;
using UnityEngine;
using UnityEngine.UI;

[PrefabPath("Prefab/UI/TierUpResultSlot")]
public class TierUpResultSlot : GenericSlot<CardTierUpWeight, TierUpResultSlot>
{
    [SerializeField]
    private TierUpResultSlotView view = null;

    protected override void OnSetData(CardTierUpWeight data)
    {
        view.ApplyCardTierUpWeight(data);
    }
}

[Serializable]
public class TierUpResultSlotView
{
    public List<GameObject> TierStar = null;
    public Text Weight = null;

    public void ApplyCardTierUpWeight(CardTierUpWeight data)
    {
        for (int i = 0; i < TierStar.Count; ++i)
        {
            TierStar[i].SetActive(i < data.Tier);
        }

        Weight.text = $"{data.Weight}%%";
    }
}