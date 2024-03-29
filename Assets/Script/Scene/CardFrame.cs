using Compound;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardFrame : MonoBehaviour
{
    [SerializeField]
    private CardFrameView view = null;

    public void ApplyCardData(CardData data)
    {
        view.ApplyCardData(data);
    }

    public void ApplyRewardData(RewardData data)
    {
        view.ApplyRewardData(data);
    }
}

[Serializable]
public class CardFrameView
{
    public List<GameObject> TierStar = null;
    public Image Icon = null;
    public Text Level = null;

    public void ApplyCardData(CardData data)
    {
        for (var i = 0; i < TierStar.Count; ++i)
        {
            TierStar[i].SetActive(i < data.CardSO.Tier);
        }

        Icon.ApplyCardIcon(data);
        Level.ApplyCardLevel(data);
    }

    public void ApplyRewardData(RewardData data)
    {
        for (var i = 0; i < TierStar.Count; ++i)
        {
            TierStar[i].SetActive(i < data.CardSO.Tier);
        }

        Icon.ApplyCardIcon(data.CardSO);
        Level.ApplyCardLevel(1);
    }
}