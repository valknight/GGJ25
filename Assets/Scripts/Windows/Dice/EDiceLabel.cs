using System;
using TMPro;
using UnityEngine;

namespace Windows.Windows.Dice
{
    public class EDiceLabel: MonoBehaviour
    {
        private TMP_Text m_tmp;

        private void Start()
        {
            m_tmp = GetComponent<TMP_Text>();
        }

        public void SetLastRoll(int roll, int newToBeat)
        {
            m_tmp.text = $"You last rolled {roll} (win streak: {EDice.winStreak}!\nTo win, beat {newToBeat}";
        }
    }
}