using System;
using System.Collections.Generic;
using System.Globalization;
using Market_Simulation;
using TMPro;
using UnityEngine;
using Utils;

namespace Windows
{
    public class EGraph : MonoBehaviour
    {
        public GameObject upArrow;
        public GameObject downArrow;

        public TMP_Text valueText;

        public ProviderDetail detailPrefab;
        public RectTransform detailParent;

        private static List<object> details = new();

        [RuntimeInitializeOnLoadMethod]
        private static void StaticSubscribers()
        {
            SystemEventManager.Subscribe(SystemEventManager.SystemEventType.ProviderRegistered, OnProviderRegisteredS);
            SystemEventManager.Subscribe(SystemEventManager.SystemEventType.ProviderDeRegistered, OnProviderDeregisteredS);
        }
        
        private void Start()
        {
            SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SimValueUpdated, OnSimValueUpdated);
            SystemEventManager.Subscribe(SystemEventManager.SystemEventType.ProviderRegistered, OnProviderRegistered);
            SystemEventManager.Subscribe(SystemEventManager.SystemEventType.ProviderDeRegistered, OnProviderDeregistered);
            // Simulate existing events
            RebuildDetails();
        }

        private void OnProviderDeregistered(object obj)
        {
            // Solves race conditions? not pretty but Game Jam(TM)
            // ALSO fun 00:28 note - don't pull a Val and forget the S at the end here. Will get sad quickly.
            OnProviderDeregisteredS(obj);
            // Ok, the lazy way is we just entirely rebuild the details
            // It's hacky and bad. But it works!
            RebuildDetails();
        }

        private void RebuildDetails()
        {
            for (var i = detailParent.childCount - 1; i >= 0; i--)
            {
                Destroy(detailParent.GetChild(i).gameObject);
            }
            foreach (var obj in details)
                OnProviderRegistered(obj);
        }

        private static void OnProviderDeregisteredS(object obj)
        {
            if (details.Contains(obj))
                details.Remove(obj);
        }

        private void OnProviderRegistered(object obj)
        {
            if (obj is ISimValueProvider provider) Instantiate(detailPrefab, detailParent).Init(provider);
        }

        private static void OnProviderRegisteredS(object obj)
        {
            if (details.Contains(obj))
                return;
            details.Add(obj);
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
           upArrow.SetActive(state.lastDelta > 0);
           downArrow.SetActive(state.lastDelta < 0);

           m_lastValue = state.currentValue;
        }

        private float m_lastValue;
        private float m_currentValue;

        private void Update()
        {
            m_currentValue = m_currentValue.Decay(m_lastValue, 8f, Time.deltaTime);
            valueText.text = m_currentValue.ToString("F2");
        }

        private void OnDisable()
        {
            SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SimValueUpdated, OnSimValueUpdated);
            SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.ProviderRegistered, OnProviderRegistered);
        }
    }
}
