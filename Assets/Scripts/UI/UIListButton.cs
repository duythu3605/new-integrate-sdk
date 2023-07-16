using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIListButton : UIScene
{
    private GameManager _gameManager;

    [Header("FireBase")]
    public Button _bntLoginFireBase;
    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _bntLoginFireBase.onClick.AddListener(() => 
        {
            _gameManager._uIManager._uILogin.ShowUI();
            HideUI();
        });
    }


}
