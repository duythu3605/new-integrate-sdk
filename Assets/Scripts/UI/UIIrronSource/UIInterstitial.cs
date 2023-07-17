using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInterstitial : UIScene
{
    public Button _btnBack;
    public Button _btnLoad;
    public Button _btnShow;

    public void Init(GameManager gameManager)
    {
        _btnBack.onClick.AddListener(() =>
        {
            HideUI();
            gameManager._uIManager._uiListButton.ShowUI();
        });
        _btnLoad.onClick.AddListener(() =>
        {
            gameManager._unityAdsỈonSource.LoadInterstitialAd();
        });
        _btnShow.onClick.AddListener(() =>
        {
            gameManager._unityAdsỈonSource.DestroyBannerAd();
            gameManager._unityAdsỈonSource.LoadInterstitialAd();
        });
    }
}
