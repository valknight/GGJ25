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

   private static List<ISimValueProvider> _valueProviders;

   public static void Init()
   {
      _valueProviders = new List<ISimValueProvider>();
      State = new SimState();
   }
   
   public static void Update()
   {
      var newValue = 0f;
      // prevent enumator collection issues
      for (var index = _valueProviders.Count - 1; index >= 0; index--)
      {
         var provider = _valueProviders[index];
#if UNITY_EDITOR
         if (provider is MonoBehaviour)
         {
            if (!(MonoBehaviour)provider)
            {
               _valueProviders.RemoveAt(index);
               EditorUtility.DisplayDialog("ISim LEAK", $"{provider.GetType().FullName} did not unsubscribe!! This will **EXPLODE** in a build", "OK");
               continue;
            }
         }
#endif
         var value = provider.GetValue();
         newValue += value;
         SystemEventManager.RaiseEvent(
            SystemEventManager.SystemEventType.SimProviderPolled,
            new SimProviderPolledEvent()
            {
               provider = provider,
               value = value
            });
      }

      State.lastDelta = newValue - State.currentValue;
      State.currentValue = newValue;
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.SimValueUpdated, State);
   }

   public static void RegisterProvider(ISimValueProvider provider)
   {
      if (_valueProviders.Contains(provider))
         return;
      _valueProviders.Add(provider);
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.ProviderRegistered, provider);
   }

   public static void DeRegisterProvider(ISimValueProvider provider)
   {
      if (!_valueProviders.Contains(provider)) return;
      
      _valueProviders.Remove(provider);
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.ProviderDeRegistered, provider);
   }
}
