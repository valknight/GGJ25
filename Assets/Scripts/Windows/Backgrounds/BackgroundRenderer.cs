using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Backgrounds
{
    public class BackgroundRenderer : MonoBehaviour
    {
        [SerializeField] private List<Material> m_backgrounds;
        private Image m_meshRenderer;

        private static List<Material> s_backgrounds;
    
        public delegate void BackgroundChange(Material background);
        public static event BackgroundChange OnBackgroundChanged;
    
        public static List<Material> BackgroundMaterials
        {
            get
            {
                if (s_backgrounds == null)
                    s_backgrounds = new List<Material>();
                return s_backgrounds;
            }
        }

        public static void ChangeMat(Material background) => OnBackgroundChanged?.Invoke(background);

        private void Start()
        {
            m_meshRenderer = GetComponent<Image>();
            s_backgrounds = m_backgrounds;
            OnBackgroundChanged += MaterialChanged;
        }

        private void MaterialChanged(Material mat)
        {
            m_meshRenderer.material = mat;
        }

        public void OnDestroy()
        {
            OnBackgroundChanged -= MaterialChanged;
        }
    }
}
