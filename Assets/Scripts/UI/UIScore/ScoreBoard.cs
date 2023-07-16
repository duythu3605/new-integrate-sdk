using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [Header("Output Text")]
    public TMP_Text _outputUserName;
    public TMP_Text _outputKill;
    public TMP_Text _outputDeath;
    public TMP_Text _outputExp;

    public void SetData(string _username, int _kill, int _death, int _exp)
    {
        _outputUserName.text = _username;
        _outputKill.text = _kill.ToString();
        _outputDeath.text = _death.ToString();
        _outputExp.text = _exp.ToString();

    }
}
