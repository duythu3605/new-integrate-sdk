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

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _bntLoginFireBase.onClick.AddListener(() => 
        {
            _gameManager._uIManager._uILogin.ShowUI();
            HideUI();
        });

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
    }


}
