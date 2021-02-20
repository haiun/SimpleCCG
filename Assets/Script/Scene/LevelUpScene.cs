using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[PrefabPath("Prefab/UI/LevelUpScene")]
public class LevelUpScene : MonoBehaviour
{
    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}
