using System;
using UnityEngine;

namespace Windows
{
    public class EGraph : MonoBehaviour
    {
        public GameObject graphCandlePrefab;
        private void OnEnable()
        {
            SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SimValueUpdated, OnSimValueUpdated);
        }

        private void OnSimValueUpdated(object obj)
        {
            if (obj is SimState state)
            {
                UpdateView(state);
            }
        }

        private void UpdateView(SimState state)
        {
           
        }

        private void OnDisable()
        {
            SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SimValueUpdated, OnSimValueUpdated);
        }
    }
}
