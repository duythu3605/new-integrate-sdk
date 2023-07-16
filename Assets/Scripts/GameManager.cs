using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public UIManager _uIManager;

    [HideInInspector]
    public AuthManager _authManager;
    private void Awake()
    {
        _uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _authManager = GameObject.Find("AuthManager").GetComponent<AuthManager>();

        _authManager.Init(this);
        _uIManager.Init(this);
    }
}
