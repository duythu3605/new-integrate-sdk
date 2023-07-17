using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;

public class FacebookSystem : MonoBehaviour
{
    private GameManager _gameManager;
    private static readonly string EVENT_PARAM_SCORE = "score";
    private static readonly string EVENT_NAME_GAME_PLAYED = "game_played";

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    
    public void Login()
    {
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    public void LogOut()
    {
        FB.LogOut();
        _gameManager._uIManager._uiFaceBook.HideUI();
        _gameManager._uIManager._uiListButton.ShowUI();
    }


    public void Share()
    {
        FB.ShareLink(new Uri("https://www.youtube.com/"), "Check!", "Watched", new Uri("https://www.youtube.com/watch?v=QVuVwTwKjxE"));
    }

    

    public void FBGameRequest()
    {
        FB.AppRequest("Play this game!", title: "Test");
    }
    public void FbInvite()
    {
        var tutParams = new Dictionary<string, object>();
        tutParams[AppEventParameterName.ContentID] = "tutorial_step_1";
        tutParams[AppEventParameterName.Description] = "First step in the tutorial, clicking the first button!";
        tutParams[AppEventParameterName.Success] = "1";

        FB.LogAppEvent(
            AppEventName.CompletedTutorial,
            parameters: tutParams
        );
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            DealWithFbMenus(FB.IsLoggedIn);
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
            _gameManager._uIManager._uiFaceBook.ShowUI();
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }
    void DealWithFbMenus(bool isLoggedId)
    {
        if (isLoggedId)
        {
            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&with=128", HttpMethod.GET, DisplayProfilePic);
        }
        else
        {

        }
    }


    private void DisplayUsername(IResult result)
    {
        if(result.Error == null)
        {
            string name = "" + result.ResultDictionary["first_name"];
            _gameManager._uIManager._uiFaceBook.SetName(name);
            Debug.Log(name);
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
    private void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
        {
            
            _gameManager._uIManager._uiFaceBook._avatar.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
            
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

}
