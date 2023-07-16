using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;

    public UIListButton _uiListButton;
    public UILogin _uILogin;
    public UIRegister _uIRegister;
    public UIUserData _uiUserData;
    public UIScore _uIScore;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _uiListButton.Init(_gameManager);
        _uILogin.Init(_gameManager);
        _uIRegister.Init(_gameManager);
        _uiUserData.Init(_gameManager);
        _uIScore.Init(_gameManager);
        FirstUI();
    }

    private void FirstUI()
    {
        _uILogin.HideUI();
        _uIRegister.HideUI();
        _uiUserData.HideUI();
        _uIScore.HideUI();
        _uiListButton.ShowUI();
        
    }
}
