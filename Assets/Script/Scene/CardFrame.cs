using Compound;
using System.Collections;
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
}

[Serializable]
public class CardFrameView
{
    public List<GameObject> TierStar = null;
    public Image Icon = null;
    public Text Level = null;

    public void ApplyCardData(CardData data)
    {
        for (int i = 0; i < TierStar.Count; ++i)
        {
            TierStar[i].SetActive(i < data.CardSO.Tier);
        }

        Icon.ApplyCardIcon(data);
        Level.ApplyCardLevel(data);
    }
}