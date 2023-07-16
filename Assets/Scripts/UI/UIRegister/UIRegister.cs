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

    [Header("Notice Text")]
    public TMP_Text _noticeText;
    [Header("Button")]
    public Button _btnBack;
    public Button _bntRegister;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;


        _bntRegister.onClick.RemoveAllListeners();
        _bntRegister.onClick.AddListener(() => 
        {
            _gameManager._firebaseManager.RegisterButton(_inputEmail.text, _inputPass.text,_inputRePass.text, _inputName.text);
        });

        _btnBack.onClick.RemoveAllListeners();
        _btnBack.onClick.AddListener(() => 
        {
            HideUI();
            _gameManager._uIManager._uILogin.ShowUI();
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

    public void ClearRegisterFeilds()
    {
        _inputName.text = "";
        _inputEmail.text = "";
        _inputPass.text = "";
        _inputRePass.text = "";
    }
}
