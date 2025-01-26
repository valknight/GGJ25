using UnityEngine;

[CreateAssetMenu(menuName = "SoundDefinition", fileName = "new Sound Definition")]
public class SoundDefinition : ScriptableObject
{
    public AudioClip audioClip;
    
    [Header("Disabling isPooled will force the sound to play")]
    public bool isPooled;
    
    [Range(0,1)]
    public float volume;
    [Range(-3,3)]
    public float pitch;

    [Range(0,1)]
    public float volumeRandom;
    [Range(0,1)]
    public float pitchRandom; 
}