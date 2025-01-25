using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace UI
{
    public class WindowMover: MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private static readonly Vector2 CanvasReferenceResolution = new Vector2(1280, 720);
        private const float DURATION_BEFORE_SNAPPING = 0.3f;
        
        private RectTransform _rectTransform;
        private float _timeDragEnded = float.MaxValue;
        private bool _isDragging;

        private Vector3 _targetMousePosition;

        private Vector2 _sizeAndBorder => _rectTransform.sizeDelta + new Vector2(20f, 80f);

        private Vector2 positionSnappedToCanvas
        {
            get
            {
                var anchoredPos = _rectTransform.anchoredPosition;
                var centered = CanvasReferenceResolution / 2;
                var size = _sizeAndBorder;
                anchoredPos.x = Mathf.Clamp(anchoredPos.x, -centered.x + size.x / 2, centered.x - size.x / 2);
                anchoredPos.y = Mathf.Clamp(anchoredPos.y,  -centered.y + size.y / 2, centered.y - size.y / 2);
                return anchoredPos;
            }
        }
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _targetMousePosition = transform.position;
        }

        public void Update()
        {
            if (_isDragging || Time.time - _timeDragEnded < DURATION_BEFORE_SNAPPING)
            {
                transform.position = transform.position.Decay(_targetMousePosition, 8f, Time.deltaTime);
                return;
            }

            _rectTransform.anchoredPosition = _rectTransform.anchoredPosition.Decay(positionSnappedToCanvas, 8f, Time.deltaTime);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == null)
                return;
            _targetMousePosition = eventData.pointerCurrentRaycast.worldPosition;
            transform.SetAsLastSibling();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            _timeDragEnded = Time.time;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
        }
    }
}