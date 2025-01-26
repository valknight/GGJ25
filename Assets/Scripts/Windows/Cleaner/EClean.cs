using System;
using Market_Simulation;
using UnityEngine;
using UnityEngine.Events;

namespace Windows.Cleaner
{
    public class EClean: MonoBehaviour, ISimValueProvider
    {
        private Vector3 LastPosition;
        private float Distance;

        public UnityEvent OnClean;

        private void Start()
        {
            SimManager.RegisterProvider(this);
            LastPosition = transform.position;
        }

        private void OnDestroy()
        {
            SimManager.DeRegisterProvider(this);
        }

        private void Update()
        {
            var delta = transform.position - LastPosition;
            delta.x = Mathf.Abs(delta.x);
            delta.y = Mathf.Abs(delta.y);
            delta.z = Mathf.Abs(delta.z);
            LastPosition = transform.position;
            if (delta.magnitude >= Mathf.Epsilon)
            {
                Distance = delta.magnitude;
                OnClean.Invoke();
            }
            else
            {
                Distance -= 0.01f * Time.deltaTime;
            }
            Mathf.Clamp(Distance, -0.1f, 1.0f);
        }

        public float GetValue()
        {
            return Distance * 200f;
        }

        public string GetProviderName()
        {
            return "EClean";
        }
    }
}