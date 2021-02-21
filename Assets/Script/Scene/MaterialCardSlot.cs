using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialCardSlotData
{
    public Compound.CardData CardData = null;
    public int SelectedIndex = 0;
    public Action<MaterialCardSlotData, MaterialCardSlot> OnClickSlot = null;
}

[PrefabPath("Prefab/UI/MaterialCardSlot")]
public class MaterialCardSlot : GenericSlot<MaterialCardSlotData, MaterialCardSlot>
{
    [SerializeField]
    private MaterialCardSlotView view = null;

    [SerializeField]
    private CardFrame cardFrame = null;

    protected override void OnSetData(MaterialCardSlotData data)
    {
        view.ApplyMaterialCardSlotData(data);
        cardFrame.ApplyCardData(data.CardData);
    }

    public void OnClickSlot()
    {
        Data?.OnClickSlot?.Invoke(Data, this);
    }
}

[Serializable]
public class MaterialCardSlotView
{
    public GameObject SelectionRoot = null;
    public Text SelectionIndex = null;

    public void ApplyMaterialCardSlotData(MaterialCardSlotData data)
    {
        bool activeSelection = data.SelectedIndex > 0;
        SelectionRoot.SetActive(activeSelection);
        if (activeSelection)
        {
            SelectionIndex.text = data.SelectedIndex.ToString();
        }
    }
}
