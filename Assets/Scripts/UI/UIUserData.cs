using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUserData : UIScene
{
    private GameManager _gameManager;

    [Header("Input")]
    public TMP_InputField _inputUserName;
    public TMP_InputField _inputExp;
    public TMP_InputField _inputKills;
    public TMP_InputField _inputDeaths;

    [Header("Button")]
    public Button _btnSave;
    public Button _btnScoreBoard;
    public Button _btnSignOut;
    public Button _btnBack;
    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _btnSave.onClick.AddListener(() =>
        {
            _gameManager._firebaseManager.SaveDataButton();
        });
        _btnScoreBoard.onClick.AddListener(() =>
        {
            HideUI();
            _gameManager._firebaseManager.ScoreboardButton();
        });

        _btnSignOut.onClick.AddListener(() =>
        {
            _gameManager._firebaseManager.SignOutButton();
        });


        _btnBack.onClick.AddListener(() => 
        {
            HideUI();
            _gameManager._uIManager._uiListButton.ShowUI();
        });
    }
}
