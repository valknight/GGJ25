using Market_Simulation;
using UnityEngine;

namespace Windows.Backgrounds
{
    public class BackgroundMultiplier: MonoBehaviour, ISimValueProvider, ISimValueProviderMultiplier
    {
        private void Start()
        {
            SimManager.RegisterProvider(this);
        }

        private void OnDestroy()
        {
            SimManager.DeRegisterProvider(this);
        }
        
        public float GetValue()
        {
            return Random.Range(0.8f, 1.2f);
        }

        public string GetProviderName()
        {
            return "EBackground";
        }
    }
}