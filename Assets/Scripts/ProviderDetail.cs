using System;
using Market_Simulation;
using TMPro;
using UnityEngine;
using static Market_Simulation.ISimValueProvider;

public class ProviderDetail : MonoBehaviour
{
    public TMP_Text providerNameText;
    public TMP_Text providerAmountText;
    private ISimValueProvider _provider;
    
    public void Init(ISimValueProvider provider)
    {
        _provider = provider;
        providerNameText.text = provider.GetProviderName();
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SimProviderPolled, OnProviderPolled);
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.ProviderDeRegistered, OnProviderDeregistered);
    }

    private void OnProviderDeregistered(object obj)
    {
        if(obj == _provider) Destroy(gameObject);
    }

    private void OnProviderPolled(object obj)
    {
        if (obj is not SimProviderPolledEvent eventData) return;
        if (eventData. provider != _provider) return;
        
        var sign = eventData.value > 0 ? "+" : "-";
        providerAmountText.text = $"{sign}{eventData.value:F2}";
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SimProviderPolled, OnProviderPolled);
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.ProviderDeRegistered, OnProviderDeregistered);
    }
}
