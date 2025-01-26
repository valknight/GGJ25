using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    private bool muted;

    public AudioSource[] sourcesToMute;

    public Sprite activeSprite;
    public Sprite mutedSprite;

    public Image buttonImage;
    private void Awake()
    {
        muted = PlayerPrefs.GetInt("Muted", 0) == 1;
        UpdateSources();
    }

    public void ToggleMuted()
    {
        muted = !muted;
        SetPlayerPref();
        UpdateSources();
    }

    private void UpdateSources()
    {
        foreach (var audioSource in sourcesToMute)
        {
            audioSource.volume = muted ? 0 : 1;
        }

        buttonImage.sprite = muted ? mutedSprite : activeSprite;
    }

    private void SetPlayerPref()
    {
        PlayerPrefs.SetInt("Muted", muted ? 1 : 0);
    }
}
