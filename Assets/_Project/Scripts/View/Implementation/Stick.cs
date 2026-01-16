using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Mathf;

namespace _Project.Scripts.View.Implementation
{
    public class Stick : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _offset;
        
        private RectTransform _rectTransform;
        private Vector2 _initialAnchoredPosition;

        public Vector2 Axis
        {
            get
            {
                float xAxis = Sign(_rectTransform.localPosition.x) switch
                {
                    1f => Lerp(0f, 1f, _rectTransform.localPosition.x / _offset),
                    -1f => Lerp(0f, -1f, Abs(_rectTransform.localPosition.x) / _offset),
                    _ => 0
                };
                float yAxis = Sign(_rectTransform.localPosition.y) switch
                {
                    1f => Lerp(0f, 1f, _rectTransform.localPosition.y / _offset),
                    -1f => Lerp(0f, -1f, Abs(_rectTransform.localPosition.y) / _offset),
                    _ => 0
                };
                
                return new Vector2(xAxis, yAxis);
            }
        }
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialAnchoredPosition = _rectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData) => 
            MoveTo(_rectTransform.anchoredPosition + eventData.delta);

        public void OnEndDrag(PointerEventData eventData) =>
            MoveToInitialPosition();
        
        public void OnPointerDown(PointerEventData eventData) => 
            MoveStickToPosition(eventData);

        public void OnPointerUp(PointerEventData eventData) =>
            MoveToInitialPosition();

        private void MoveStickToPosition(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint);
            _rectTransform.localPosition = localPoint;
        }
        
        public void MoveToInitialPosition() => 
            _rectTransform.anchoredPosition = _initialAnchoredPosition;
        
        private void MoveTo(Vector2 cursorPosition)
        {
            if (Vector2.Distance(cursorPosition, _initialAnchoredPosition) <= _offset)
                _rectTransform.anchoredPosition = cursorPosition;
        }
    }
}