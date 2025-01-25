using System.Collections;
using Market_Simulation;
using UnityEngine;

public class GameManager : MonoBehaviour, ISimValueProvider
{
   public float simSpeed;
   public float simRandom;

   public bool simulating;
   private void Awake()
   {
      SystemEventManager.Init();
      SimManager.Init();
   }

   private void Start()
   {
      SimManager.RegisterProvider(this);
      simulating = true;
      StartCoroutine(UpdateSim());
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

   public float GetValue()
   {
      return Random.Range(-simRandom,simRandom);
   }

   public string GetProviderName()
   {
      return "Market Forces";
   }
}
