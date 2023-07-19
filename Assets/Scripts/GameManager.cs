using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public UIManager _uIManager;

    [HideInInspector]
    public FireBaseManager _firebaseManager;
    [HideInInspector]
    public UnityAdsIronSource _unityAdsỈonSource;

    [HideInInspector]
    public FacebookSystem _facebookSystem;
    private void Awake()
    {
        _uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _firebaseManager = GameObject.Find("FirebaseManager").GetComponent<FireBaseManager>();
        _unityAdsỈonSource = GameObject.Find("Ads").GetComponent<UnityAdsIronSource>();
        _facebookSystem = GameObject.Find("Facebook").GetComponent<FacebookSystem>();

        _uIManager.Init(this);
        _unityAdsỈonSource.Init(this);
        _firebaseManager.Init(this);        
        _facebookSystem.Init(this);
    }
}
