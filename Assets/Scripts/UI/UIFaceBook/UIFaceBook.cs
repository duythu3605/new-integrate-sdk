using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIFaceBook : UIScene
{
    public Button _btnLogout;
    public Button _btnShare;
    public Button _btnGameRequest;
    public Button _btnInvite;
    public Button _btnGetFriend;

    public Image _avatar;
    public TMP_Text _nameText;
    public TMP_Text _noticeText;

    public void Init(GameManager gameManager)
    {
        _btnLogout.onClick.AddListener(() => 
        {
            gameManager._facebookSystem.LogOut();
        });

        _btnShare.onClick.AddListener(()=>
        {
            gameManager._facebookSystem.Share();
        });
        _btnGameRequest.onClick.AddListener(() =>
        {
            
            gameManager._facebookSystem.FBGameRequest();
        });
        _btnInvite.onClick.AddListener(() =>
        {
            gameManager._facebookSystem.FbInvite();
        });
        _btnGetFriend.onClick.AddListener(() =>
        {

        });
    }

    public void SetName(string value)
    {
        _nameText.text = value;
    }

}
