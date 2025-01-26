using System;
using System.Collections.Generic;
using Market_Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Theme
{
    public class ETheme: MonoBehaviour, ISimValueProvider
    {
        private static readonly int ColorA = Shader.PropertyToID("_ColorA");
        private static readonly int ColorB = Shader.PropertyToID("_ColorB");

        [Serializable]
        public struct Theme
        {
            [SerializeField] public string name;
            [SerializeField] internal Color ColorA;
            [SerializeField] internal Color ColorB;
        }

        [SerializeField] private Material material;
        [SerializeField] private List<Theme> themes;
        [SerializeField] private LayoutGroup layoutGroup;
        [SerializeField] private GameObject labelPrefab;

        private Theme currentTheme;

        private bool m_DarkMode;

        public bool IsDarkMode
        {
            get => m_DarkMode;
            set
            {
                if (m_DarkMode != value)
                {
                    currentTheme = UpdateMat(material, currentTheme, value);
                }
                m_DarkMode = value;
            }
        }

        private void ChangeTheme(Theme theme)
        {
            currentTheme = UpdateMat(material, theme, IsDarkMode);
        }

        public void Start()
        {
            foreach (var theme in themes)
            {
                var go = InstantiateTheme(theme);
            }

            SimManager.RegisterProvider(this);
        }

        private void OnDestroy()
        {
            SimManager.DeRegisterProvider(this);
        }

        private GameObject InstantiateTheme(Theme theme)
        {
            var go = Instantiate(labelPrefab, layoutGroup.transform);
            var label = go.GetComponent<EThemeLabel>();
            label.text.text = theme.name;
            label.button.onClick.AddListener(() =>
            {
                Debug.Log($"Changing to {theme.name}");
                ChangeTheme(theme);
            });
            return go;
        }

        private static Theme UpdateMat(Material mat, Theme theme, bool darkMode)
        {
            if (theme.name.ToLower() == "flashbang")
            {
                theme.ColorA *= 2f;
                theme.ColorB *= 2f;
            }
            if (darkMode)
            {
                mat.SetColor(ColorA, theme.ColorB);
                mat.SetColor(ColorB, theme.ColorA);
                return theme;
            }
            mat.SetColor(ColorA, theme.ColorA);
            mat.SetColor(ColorB, theme.ColorB);
            return theme;
        }

        public float GetValue()
        {
            return currentTheme.ColorB.grayscale * 40f;
        }

        public string GetProviderName()
        {
            return "ETheme";
        }
    }
}