using System;

namespace Market_Simulation
{
    public interface ISimValueProvider
    {
        public float GetValue();

        public string GetProviderName();

        public class SimProviderPolledEvent
        {
            public ISimValueProvider provider;
            public float value;
        }
    }
}
