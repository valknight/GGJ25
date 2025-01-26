using System.Collections.Generic;
using Market_Simulation;

public class SimState
{
    public float currentValue;
    public float lastDelta;
    public List<ISimValueProvider> activeProviders;
}
