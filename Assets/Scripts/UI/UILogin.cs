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
    [Header("Notice Text")]
    public TMP_Text _noticeText;
    [Header("Button")]
    public Button _btnLogin;
    public Button _btnRegister;
    public Button _btnBack;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _btnLogin.onClick.RemoveAllListeners();
        _btnLogin.onClick.AddListener(() => 
        {
            _gameManager._authManager.LoginButton(_inputEmail.text,_inputPass.text);
        });

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

    public void Notice(string value, bool isWarning)
    {
        _noticeText.GetComponent<TMP_Text>().enabled = true;

        if (isWarning)
        {
            _noticeText.color = Color.red;
        }
        else
        {
            _noticeText.color = Color.green;
        }
        _noticeText.text = value;
        StartCoroutine(TimeHideNotice());
    }

    private IEnumerator TimeHideNotice()
    {
        yield return new WaitForSeconds(1.5f);
        _noticeText.GetComponent<TMP_Text>().enabled = false;
    }
}
