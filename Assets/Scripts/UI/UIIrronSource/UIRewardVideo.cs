using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRewardVideo : UIScene
{
    public Button _btnBack;
    public Button _btnShow;

    public TMP_Text _dataText;


    public void Init(GameManager gameManager)
    {

        _btnBack.onClick.AddListener(() => 
        {
            HideUI();
            gameManager._uIManager._uiListButton.ShowUI();
        });
        _btnShow.onClick.AddListener(() => 
        {
            gameManager._unityAdsỈonSource.DestroyBannerAd();
            gameManager._unityAdsỈonSource.ShowRewardAds();
            
        });

    }

    public void SetData(string value)
    {
        _dataText.text = value;
    }
}
