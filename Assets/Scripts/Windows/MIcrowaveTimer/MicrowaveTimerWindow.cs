using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Windows.Windows.MIcrowaveTimer
{
    public class MicrowaveTimerWindow: MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private bool m_IsDragging;
        
        public void OnDrag(PointerEventData eventData)
        {
            RotateByFloat(-eventData.delta.x * 5f);
        }

        private void Start()
        {
            RotateByFloat(270f);
        }

        private void Update()
        {
            if (m_IsDragging)
                return;
            RotateByFloat(5f * Time.deltaTime);
        }

        private void RotateByFloat(float angle)
        {
            var angles = transform.rotation.eulerAngles;
            angles.z += angle;
            angles.z = Mathf.Clamp(angles.z, 90f, 270f);
            var rotation = transform.rotation;
            rotation.eulerAngles = angles;
            transform.rotation = rotation;
        }
        
        public void OnBeginDrag(PointerEventData eventData) => m_IsDragging = true;
        
        public void OnEndDrag(PointerEventData eventData) => m_IsDragging = false;
    }
}