using System.Collections;
using Market_Simulation;
using UnityEngine;

public class GameManager : MonoBehaviour, ISimValueProvider
{
   public float simSpeed;
   public float simNumber;

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
         simNumber = SimManager.State.currentValue;
      }
   }

   public float GetValue()
   {
      return Random.Range(-5,7);
   }
}
