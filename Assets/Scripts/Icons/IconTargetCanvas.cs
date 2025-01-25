using UnityEngine;

namespace Windows.Icons
{
    public class IconTargetCanvas: MonoBehaviour
    {
        private void Start()
        {
            IconScrob.targetCanvas = GetComponent<Canvas>();
        }
    }
}