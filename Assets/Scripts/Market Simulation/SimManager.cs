using System.Collections.Generic;
using Market_Simulation;

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
         delta += provider.GetValue();
      }
      State.currentValue += delta;
      State.lastDelta = delta;
      SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.SimValueUpdated, State);
   }

   public static void RegisterProvider(ISimValueProvider provider)
   {
      _valueProviders.Add(provider);
   }

   public static void DeRegisterProvider(ISimValueProvider provider)
   {
      _valueProviders.Remove(provider);
   }
}
