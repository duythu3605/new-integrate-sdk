using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public UIManager _uIManager;
    private void Awake()
    {
        _uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        _uIManager.Init(this);
    }
}
