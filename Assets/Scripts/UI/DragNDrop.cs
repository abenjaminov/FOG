using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DragNDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _isMouseDown;
        private Vector2 _mouseDownLocation;
        private Vector2 _initialLocation;
    
        public void OnPointerDown(PointerEventData eventData)
        {
            _isMouseDown = true;
            _mouseDownLocation = eventData.position;
            _initialLocation = transform.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isMouseDown = false;
        }

        private void Update()
        {
            if (!_isMouseDown) return;

            Vector2 delta = (Vector2)(Input.mousePosition) - _mouseDownLocation;
            transform.position = _initialLocation + delta;
        }
    }
}
