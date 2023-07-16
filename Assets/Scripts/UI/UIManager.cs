using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;

    public UIListButton _uiListButton;
    public UILogin _uILogin;
    public UIRegister _uIRegister;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _uiListButton.Init(_gameManager);
        _uILogin.Init(_gameManager);
        _uIRegister.Init(_gameManager);
        FirstUI();
    }

    private void FirstUI()
    {
        _uiListButton.ShowUI();
        _uILogin.HideUI();
        _uIRegister.HideUI();
    }
}
