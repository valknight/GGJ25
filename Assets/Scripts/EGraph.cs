using System;
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
        
        private void Start()
        {
            SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SimValueUpdated, OnSimValueUpdated);
            SystemEventManager.Subscribe(SystemEventManager.SystemEventType.ProviderRegistered, OnProviderRegistered);
        }

        private void OnProviderRegistered(object obj)
        {
            if (obj is ISimValueProvider provider) Instantiate(detailPrefab, detailParent).Init(provider);
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
