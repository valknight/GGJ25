using System.Collections.Generic;
using Market_Simulation;
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
      var delta = 0f;
      foreach (var provider in _valueProviders)
      {
         var value = provider.GetValue();
         delta += value;
         SystemEventManager.RaiseEvent(
            SystemEventManager.SystemEventType.SimProviderPolled, 
            new SimProviderPolledEvent()
            {
               provider = provider,
               value =  value
            });
      }
      State.currentValue += delta;
      State.lastDelta = delta;
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.SimValueUpdated, State);
   }

   public static void RegisterProvider(ISimValueProvider provider)
   {
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
