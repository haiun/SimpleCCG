using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRewardPopupInitParam
{
    List<CCGAsset> CCGAssetList = new List<CCGAsset>();
}

[PrefabPath("Prefab/UI/GetReward")]
public class GetRewardPopup : MonoBehaviour
{
    GetRewardPopupInitParam initParam = null;

    public static GetRewardPopup CreatePopup(GetRewardPopupInitParam param)
    {
        var popup = GenericPrefab.Instantiate<GetRewardPopup>();
        popup.Initialize(param);
        return popup;
    }

    private void Initialize(GetRewardPopupInitParam param)
    {
        initParam = param;

    }

    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}
