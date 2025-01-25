using System;
using System.Linq;
using UnityEngine;

public class GraphViewWorld : MonoBehaviour
{
    public LineRenderer line;
    public float posScale;

    private int targetposition = 1;
    private float[] values = new float[10];

    private void Start()
    {
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SimValueUpdated, OnSimValueUpdated);
        line.positionCount = 0;
        line.positionCount = values.Length;
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, Vector3.zero);
        }
        values = new float[10];
    }

    private void OnSimValueUpdated(object obj)
    {
        if (obj is not SimState state) return;
        
        if (targetposition == values.Length - 1)
        {
            for (int i = 1; i < line.positionCount; i++)
            {
                values[i-1] = values[i];
            }
        }
        
        values[targetposition] = state.currentValue;

        var total = values.Sum();
        var scalar = total / values.Where(val => val != 0f).ToArray().Length;
        
        for (int i = 0; i < line.positionCount; i++)
        {
            var pos = new Vector3(i, values[i]/scalar, 1);
            line.SetPosition(i,pos);
        }
        
        targetposition = Mathf.Clamp(targetposition + 1, 0, values.Length-1);
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SimValueUpdated, OnSimValueUpdated);
    }
}
