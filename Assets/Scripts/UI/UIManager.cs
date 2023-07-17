using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    public UIListButton _uiListButton;
    public UILogin _uILogin;
    public UIRegister _uIRegister;
    public UIUserData _uiUserData;
    public UIScore _uIScore;
    public UIBanner _uIBanner;
    public UIRewardVideo _uiRewardVideo;
    public UIInterstitial _uIInterstitial;

    public void Init(GameManager gameManager)
    {
        _uiListButton.Init(gameManager);
        _uILogin.Init(gameManager);
        _uIRegister.Init(gameManager);
        _uiUserData.Init(gameManager);
        _uIScore.Init(gameManager);
        _uIBanner.Init(gameManager);
        _uiRewardVideo.Init(gameManager);
        _uIInterstitial.Init(gameManager);

    }

}
