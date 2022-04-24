using System;
using UnityEngine;

public class SelectCardPopupInitData
{
    public MyCardSlotData SelectedCardData = null;
    public Action<MyCardSlotData> OnSelect = null;
}

[PrefabPath("Prefab/UI/SelectCardPopup")]
public class SelectCardPopup : MonoBehaviour
{
    [SerializeField]
    MyCardSlot myCardSlot = null;

    SelectCardPopupInitData data = null;

    public static SelectCardPopup CreatePopup(SelectCardPopupInitData data)
    {
        var popup = GenericPrefab.Instantiate<SelectCardPopup>();
        popup.Initialize(data);
        return popup;
    }

    private void Initialize(SelectCardPopupInitData data)
    {
        this.data = data;
        myCardSlot.SetData(data.SelectedCardData);
    }

    public void OnClickSelect()
    {
        data.OnSelect(data.SelectedCardData);
        Destroy(gameObject);
    }

    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}
