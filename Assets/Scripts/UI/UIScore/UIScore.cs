using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : UIScene
{
    private GameManager _gameManager;

    [Header("Button")]
    public Button _btnBack;

    [Header("Parent Score")]
    public GameObject scoreElement;
    public Transform scoreboardContent;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _btnBack.onClick.AddListener(() =>
        {
            HideUI();
            _gameManager._uIManager._uiListButton.ShowUI();
        });
    }
}
