using System.Collections.Generic;
using Market_Simulation;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using static Market_Simulation.ISimValueProvider;

public static class SimManager
{

   public static SimState State;


   public static void Init()
   {
      State = new SimState();
      State.activeProviders = new List<ISimValueProvider>();
   }
   
   public static void Update()
   {
      var newValue = 0f;
      var mult = 1f;
      // prevent enumator collection issues
      for (var index = State.activeProviders.Count - 1; index >= 0; index--)
      {
         var provider = State.activeProviders[index];
#if UNITY_EDITOR
         if (provider is MonoBehaviour behaviour)
         {
            if (!behaviour)
            {
               State.activeProviders.RemoveAt(index);
               EditorUtility.DisplayDialog("ISim LEAK", $"{behaviour.GetType().FullName} did not unsubscribe!! This will **EXPLODE** in a build", "OK");
               continue;
            }
         }
#endif
         var value = provider.GetValue();
         if (provider is ISimValueProviderMultiplier)
         {
            mult += value;
         }
         else
         {
            newValue += value;
         }

         SystemEventManager.RaiseEvent(
            SystemEventManager.SystemEventType.SimProviderPolled,
            new SimProviderPolledEvent()
            {
               provider = provider,
               value = value
            });
      }

      newValue *= mult;

      State.lastDelta = newValue - State.currentValue;
      State.currentValue = newValue;
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.SimValueUpdated, State);
   }

   public static void RegisterProvider(ISimValueProvider provider)
   {
      if (State.activeProviders.Contains(provider))
         return;
      State.activeProviders.Add(provider);
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.ProviderRegistered, provider);
   }

   public static void DeRegisterProvider(ISimValueProvider provider)
   {
      if (!State.activeProviders.Contains(provider)) return;
      
      State.activeProviders.Remove(provider);
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.ProviderDeRegistered, provider);
   }
}
