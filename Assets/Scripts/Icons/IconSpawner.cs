using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Icons
{
    public class IconSpawner: MonoBehaviour
    {
        [SerializeField] private List<IconScrob> icons;

        public IEnumerator Start()
        {
            var transforms = new List<Transform>();
            foreach (var icon in icons)
            {
                transforms.Add(instantiateIcon(transform, icon));
            }
            yield return null;
            foreach (var t in transforms)
                t.GetComponent<WindowMover>().UpdatePositionAsLastDragged();
            GetComponent<GridLayoutGroup>().enabled = false;
        }

        public void OnValidate()
        {
#if UNITY_EDITOR
            var assets = AssetDatabase.FindAssets("t:IconScrob");
            icons = new List<IconScrob>();
            foreach (var assetGuids in assets)
            {
                var path = AssetDatabase.GUIDToAssetPath(assetGuids);
                var asset = AssetDatabase.LoadAssetAtPath<IconScrob>(path);
                icons.Add(asset);
            }
#endif
        }

        private static Transform instantiateIcon(Transform parent, IconScrob scrob)
        {
            var go = new GameObject();
            var image = go.AddComponent<Image>();
            image.sprite = scrob.desktopIcon;
            var button = go.AddComponent<Button>();
            button.onClick.AddListener(scrob.SpawnOrFocus);
            go.transform.SetParent(parent);
            var rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(100, 100);
            go.transform.localScale = Vector3.one;
            go.AddComponent<WindowMover>();
            return go.transform;
        }
    }
}