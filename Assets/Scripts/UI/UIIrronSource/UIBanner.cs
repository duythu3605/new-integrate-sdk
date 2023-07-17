using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBanner : UIScene
{
    public Button _btnBack;
    public Button _btnLoad;
    public Button _btnHide;
    public Button _btnShow;
    public Button _btnDestroy;

    public void Init(GameManager gameManager)
    {
        _btnBack.onClick.AddListener(() =>
        {
            HideUI();
            gameManager._uIManager._uiListButton.ShowUI();
        });
        _btnLoad.onClick.AddListener(() =>
        {
            gameManager._unityAdsỈonSource.LoadBannerAd();
        });
        _btnHide.onClick.AddListener(() =>
        {
            gameManager._unityAdsỈonSource.HideBannerAd();
        });
        _btnShow.onClick.AddListener(() =>
        {
            gameManager._unityAdsỈonSource.ShowBanerAd();
        });
        _btnDestroy.onClick.AddListener(() =>
        {
            gameManager._unityAdsỈonSource.DestroyBannerAd();
            gameManager._unityAdsỈonSource.ShowInterstitialAd();
        });
    }

}
