using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[PrefabPath("Prefab/UI/TierUpScene")]
public class TierUpScene : MonoBehaviour
{
    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}
