using System;
using Market_Simulation;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Windows.Windows.Dice
{
    public class EDice: MonoBehaviour, ISimValueProvider, ISimValueProviderMultiplier
    {
        private int numberToBeat;
        public UnityEvent<int, int> OnFailedRoll;
        public UnityEvent<int, int> OnSuccessRoll;

        public static int winStreak = 0;

        private int lastRoll;

        private void Start()
        {
            SimManager.RegisterProvider(this);
        }

        private void OnDestroy()
        {
            SimManager.DeRegisterProvider(this);
        }

        private bool Roll()
        {
            numberToBeat = Mathf.Clamp(numberToBeat, 1, 19);
            lastRoll = Random.Range(1, 21);
            var success = lastRoll >= numberToBeat;
            if (success)
            {
                winStreak++;
                numberToBeat++;
            }
            else
            {
                winStreak = 0;
                numberToBeat = 0;
            }
            return success;
        }

        private float OddsMultiplier
        {
            get
            {
                // d20
                numberToBeat = Mathf.Clamp(numberToBeat, 1, 19);
                // OK, now do inverse lerp
                var t = Mathf.InverseLerp(1, 20, numberToBeat);
                // sure. i love balance
                return t * winStreak * 10;
            }
        }

        public void DoRoll()
        {
            if (Roll())
            {
                OnSuccessRoll.Invoke(lastRoll, numberToBeat);
            }
            else
            {
                OnFailedRoll.Invoke(lastRoll, numberToBeat);
            }
        }

        public float GetValue()
        {
            return OddsMultiplier;
        }

        public string GetProviderName()
        {
            return "EDice";
        }
    }
}