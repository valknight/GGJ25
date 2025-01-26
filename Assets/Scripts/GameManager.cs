using System;
using System.Collections;
using Data;
using Market_Simulation;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour, ISimValueProvider
{
   public float simSpeed;
   public float simRandom;

   public bool simulating;
   public GameObject titleScreen;
   private void Awake()
   {
      SystemEventManager.Init();
      ApplicationManager.Init();
      SimManager.Init();
      titleScreen.SetActive(true);
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
         SoundManager.Instance.PlayOnClickSound();
      }
   }

   public float GetValue()
   {
      return Random.Range(-simRandom,simRandom);
   }

   public string GetProviderName()
   {
      return "Market Forces";
   }
}
