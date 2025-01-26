using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
   public Dictionary<string, List<AudioSource>> _activeSources;
   public AudioSource sourcePrefab;

   public SoundDefinition[] onClickSounds;
   public SoundDefinition[] mouseDownSounds;
   public SoundDefinition[] mouseUpSounds;
   public SoundDefinition[] keyboardSounds;

   public static SoundManager Instance;

   public enum SoundEvent
   {
      OnClick,
      OnMouseDown,
      OnMouseUp,
      OnKeyboardSounds
   }

   private void Awake()
   {
      Instance = this;
   }

   private SoundDefinition[] GetEventSoundList(SoundEvent soundEvent)
   {
      return soundEvent switch
      {
         SoundEvent.OnClick => onClickSounds,
         SoundEvent.OnMouseDown => mouseDownSounds,
         SoundEvent.OnMouseUp => mouseUpSounds,
         SoundEvent.OnKeyboardSounds => keyboardSounds,
         _ => null
      };
   }

   public void PlayEventSound(SoundEvent soundEvent)
   {
      PlayEventSound(soundEvent, Vector3.zero);
   }

   public void PlayEventSound(SoundEvent soundEvent, Vector3 soundPos, float killSoundTime = 3)
   {
      if(PlayerPrefs.GetInt("Muted", 0) == 1) return;
      
      var candidateSounds = GetEventSoundList(soundEvent);
      if (candidateSounds == null || candidateSounds.Length == 0) return;

      var sound = candidateSounds[Random.Range(0, candidateSounds.Length)];

      var newSource = Instantiate(sourcePrefab, soundPos, quaternion.identity);
      newSource.clip = sound.audioClip;
      newSource.volume = sound.volume + Random.Range(-sound.volumeRandom, sound.volumeRandom);
      newSource.pitch = sound.pitch + Random.Range(-sound.pitchRandom, sound.pitchRandom);
      newSource.Play();

      Destroy(newSource.gameObject, killSoundTime);
   }
}