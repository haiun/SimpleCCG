using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public CardListSO CardListSO = null;

    void Start()
    {
        var tableManager = new TableManager();
        tableManager.Initialize();

        var userManager = new UserManager();
        userManager.Initialzie(new UserManagerInitData()
        {
            TableManager = tableManager,
            CardListSO = CardListSO
        });
        //userManager.Load();

        var startScene = GenericPrefab.Instantiate<MainScene>();
        startScene.Initailze(new MainSceneInitData()
        {
            TableManager = tableManager,
            UserManager = userManager,
            CardListSO = CardListSO
        });
    }
}
