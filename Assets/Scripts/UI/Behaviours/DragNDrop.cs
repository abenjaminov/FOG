using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DragNDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Optional")]
        [SerializeField] private GameObject _objectToDrag;
        private bool _isMouseDown;
        private Vector2 _mouseDownLocation;
        private Vector2 _initialLocation;

        private void Awake()
        {
            if (_objectToDrag == null)
                _objectToDrag = this.gameObject;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isMouseDown = true;
            _mouseDownLocation = eventData.position;
            _initialLocation = _objectToDrag.transform.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isMouseDown = false;
        }

        private void Update()
        {
            if (!_isMouseDown) return;

            Vector2 delta = (Vector2)(Input.mousePosition) - _mouseDownLocation;
            _objectToDrag.transform.position = _initialLocation + delta;
        }
    }
}
