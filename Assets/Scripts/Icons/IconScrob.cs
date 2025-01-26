using UnityEngine;

namespace Windows.Icons
{
    [CreateAssetMenu(menuName = "Create/Window Icon", fileName = "IconScrob", order = 0)]
    public class IconScrob: ScriptableObject
    {
        public Sprite desktopIcon;
        [SerializeField] private Transform windowPrefab;
        private Transform instantiated;

        public static Canvas targetCanvas;

        public void SpawnOrFocus()
        {
            if (instantiated)
            {
                if (!instantiated.gameObject.activeSelf)
                    instantiated.gameObject.SetActive(true);
                instantiated.SetAsLastSibling();
                return;
            }
            instantiated = Instantiate(windowPrefab.gameObject, targetCanvas.transform).transform;
            instantiated.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}