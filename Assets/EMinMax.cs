using System;
using Market_Simulation;
using TMPro;
using UnityEngine;

public class EMinMax : MonoBehaviour, ISimValueProvider
{
    public TMP_Text highestScore;
    public TMP_Text lowestScore;


    private void Update()
    {
        highestScore.text = PlayerPrefs.GetFloat("HighestValue", 0).ToString();
        lowestScore.text = PlayerPrefs.GetFloat("LowestValue", 0).ToString();
    }

    public float GetValue()
    {
        var highest = PlayerPrefs.GetFloat("HighestValue", 0);
        var lowest = PlayerPrefs.GetFloat("LowestValue", 0);
        if (highest == 0 || lowest == 0) return 0;

        return Mathf.Abs(highest - lowest) / 100;
    }

    public string GetProviderName()
    {
        return "EMinMax";
    }
}
