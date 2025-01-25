using System;
using Market_Simulation;
using TMPro;
using UnityEngine;

namespace Windows.Windows.Calculator
{
    public class CalculatorWindow: MonoBehaviour, ISimValueProvider
    {
        private enum Mode
        {
            SINE,
            COSINE
        }

        private Mode currentMode = Mode.SINE;
        [SerializeField] private TMP_Text modeText;
        
        private void Start()
        {
            SimManager.RegisterProvider(this);
        }

        private void OnDestroy()
        {
            SimManager.DeRegisterProvider(this);
        }

        public void SetSine()
        {
            currentMode = Mode.SINE;
        }

        public void SetCosine()
        {
            currentMode = Mode.COSINE;
        }

        public float GetValue()
        {
            if (currentMode == Mode.SINE)
            {
                return Mathf.Sin(Time.time * 0.33f) * 5f;
            }
            return Mathf.Cos(Time.time * 0.33f) * 5f;
        }

        public string GetProviderName() => "ECalc";
    }
}