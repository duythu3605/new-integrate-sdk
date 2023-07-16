using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public UIManager _uIManager;

    [HideInInspector]
    public FireBaseManager _firebaseManager;
    private void Awake()
    {
        _uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _firebaseManager = GameObject.Find("FirebaseManager").GetComponent<FireBaseManager>();

        _firebaseManager.Init(this);
        _uIManager.Init(this);
    }
}
