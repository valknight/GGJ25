using System;
using Market_Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Windows.Ads
{
    public class AdsWindow: MonoBehaviour, ISimValueProvider
    {
        public int cookiesAccepted;

        private float _timeSinceLastCookieAccepted;
        [SerializeField] private TMP_Text _text;

        public Transform cookieSpawnAnchor;

        public void Start()
        {
            SimManager.RegisterProvider(this);
        }

        public void OnDestroy()
        {
            SimManager.DeRegisterProvider(this);
        }

        public void Update()
        {
            if (!(Time.time - _timeSinceLastCookieAccepted >= 1f)) return;
            cookiesAccepted--;
            cookiesAccepted = Math.Clamp(cookiesAccepted, 0, int.MaxValue);
            _timeSinceLastCookieAccepted = Time.time;
            
            _text.SetText(cookiesAccepted.ToString());
        }
        
        public float GetValue()
        {
            if (cookiesAccepted < 5)
            {
                return ((5 - cookiesAccepted) * -Random.Range(0.45f, 0.55f));
            }
            return cookiesAccepted * Random.Range(0.45f, 0.55f);
        }

        public string GetProviderName()
        {
            return "E-UCookiePrompt";
        }

        public void ClickedButton()
        {
            cookiesAccepted++;
            _text.SetText(cookiesAccepted.ToString());
        }
    }
}