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

    [Header("Iron Source")]
    public Button _btnReward;
    public Button _btnBanner;
    public Button _btnInterstitial;

    [Header("Facebook")]
    public Button _btnLoginFb;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        //---------FireBase-----------
        _bntLoginFireBase.onClick.AddListener(() => 
        {
            _gameManager._uIManager._uILogin.ShowUI();
            HideUI();
        });
        //---------IronSource-----------
        _btnReward.onClick.AddListener(() =>
        {
            _gameManager._uIManager._uiRewardVideo.ShowUI();
            HideUI();
        });
        _btnBanner.onClick.AddListener(() =>
        {
            _gameManager._uIManager._uIBanner.ShowUI();
            HideUI();
        });
        _btnInterstitial.onClick.AddListener(() =>
        {
            _gameManager._uIManager._uIInterstitial.ShowUI();
            HideUI();
        });

        //---------Facebook-----------
        _btnLoginFb.onClick.AddListener(() =>
        {
            _gameManager._facebookSystem.Login();
        });
    }


}
