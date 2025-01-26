using System;
using System.Collections;
using Data;
using DefaultNamespace;
using Market_Simulation;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour, ISimValueProvider
{
   public float simSpeed;
   public float simRandom;
   public bool simulating;
   public GameObject titleScreen;
   public TMP_Text loginText;
   public TMP_Text usernameText;
   
   private void Awake()
   {
      SystemEventManager.Init();
      ApplicationManager.Init();
      ChatManager.Init();
      SimManager.Init();
      titleScreen?.SetActive(true);
      loginText?.SetText(PlayerPrefs.GetString("Username", string.Empty));
      
      SystemEventManager.Subscribe(SystemEventManager.SystemEventType.Login, OnLogin);
   }

   private void Start()
   {
      SimManager.RegisterProvider(this);
      simulating = true;
      StartCoroutine(UpdateSim());
   }

   private void OnDestroy()
   {
      SimManager.DeRegisterProvider(this);
   }

   private IEnumerator UpdateSim()
   {
      while (true)
      {
         yield return new WaitForSeconds(simSpeed);
         if (!simulating) continue;
         
         SimManager.Update();
      }
   }

   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         SoundManager.Instance.PlayEventSound(SoundManager.SoundEvent.OnMouseDown);
         return;
      }

      if (Input.GetMouseButtonUp(0))
      {
         SoundManager.Instance.PlayEventSound(SoundManager.SoundEvent.OnMouseUp);
         return;
      }

      if (Input.anyKeyDown)
      {
         SoundManager.Instance.PlayEventSound(SoundManager.SoundEvent.OnKeyboardSounds);
      }
   }
   
   private void OnLogin(object obj)
   {
      usernameText.text = $"Logged in as:  {PlayerPrefs.GetString("Username")}";
   }

   public float GetValue()
   {
      return Random.Range(-simRandom,simRandom);
   }

   public string GetProviderName()
   {
      return "Market Forces";
   }

   private void OnDisable()
   {
      SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.Login, OnLogin);
   }
}
