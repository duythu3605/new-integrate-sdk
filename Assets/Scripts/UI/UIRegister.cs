using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRegister : UIScene
{
    private GameManager _gameManager;
    [Header("Input")]
    public TMP_InputField _inputName;
    public TMP_InputField _inputEmail;
    public TMP_InputField _inputPass;
    public TMP_InputField _inputRePass;
    [Header("Button")]
    public Button _btnBack;
    public Button _bntRegister;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;


        _bntRegister.onClick.RemoveAllListeners();
        _bntRegister.onClick.AddListener(() => 
        {
            Debug.Log("Register");
        });

        _btnBack.onClick.RemoveAllListeners();
        _btnBack.onClick.AddListener(() => 
        {
            HideUI();
            _gameManager._uIManager._uILogin.ShowUI();
        });
    }
}
