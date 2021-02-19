using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneInitData
{
    public TableManager TableManager = null;
    public UserManager UserManager = null;
    public CardListSO CardListSO = null;
}

[PrefabPath("Prefab/UI/MainScene")]
public class MainScene : MonoBehaviour
{
    MainSceneInitData data = null;

    public void Initailze(MainSceneInitData data)
    {
        this.data = data;
    }

    public void OnClickGetCard()
    {
        var ret = data.UserManager.GetNewT1Card();
    }

    public void OnClickMyCard()
    {
        var myCardScene = GenericPrefab.Instantiate<MyCardScene>();
        myCardScene?.Initialize(new MyCardSceneInitData()
        {
            UserManager = data.UserManager
        });
    }
}
