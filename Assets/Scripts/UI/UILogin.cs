using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UILogin : UIScene
{
    private GameManager _gameManager;

    [Header("Box Login")]
    public TMP_InputField _inputEmail;
    public TMP_InputField _inputPass;

    [Header("Button")]
    public Button _btnLogin;
    public Button _btnRegister;
    public Button _btnBack;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _btnRegister.onClick.RemoveAllListeners();
        _btnRegister.onClick.AddListener(() => 
        {
            HideUI();
            _gameManager._uIManager._uIRegister.ShowUI();
        });

        _btnBack.onClick.RemoveAllListeners();
        _btnBack.onClick.AddListener(() => 
        {
            HideUI();
            _gameManager._uIManager._uiListButton.ShowUI();
        });
    }
}
