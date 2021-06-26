using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class OpenClose : MonoBehaviour
    {
        public UnityAction TransitionEndEvent;
        
        public float MaxWidth;
        public float TransitionTime;

        private RectTransform _rectTransform;
        private float _currentTransitionTime = Mathf.Infinity;

        private float _startValue;
        private float _endValue;

        public bool IsClosed;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Open()
        {
            _currentTransitionTime = 0;
            _startValue = 0;
            _endValue = MaxWidth;
        }

        public void Close()
        {
            _currentTransitionTime = 0;
            _startValue = MaxWidth;
            _endValue = 0;
        }

        private void Update()
        {
            if (_currentTransitionTime > TransitionTime) return;
            
            _currentTransitionTime += Time.deltaTime;

            var width = Mathf.Lerp(_startValue, _endValue, _currentTransitionTime / TransitionTime);

            _rectTransform.sizeDelta = new Vector2(width, _rectTransform.sizeDelta.y);

            if (_currentTransitionTime > TransitionTime)
            {
                IsClosed = _rectTransform.sizeDelta.x == 0;
                
                TransitionEndEvent?.Invoke();
            }
        }
    }
}