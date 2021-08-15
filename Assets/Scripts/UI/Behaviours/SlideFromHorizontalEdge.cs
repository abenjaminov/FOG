using System;
using UnityEngine;
using UnityEngine.Apple;
using UnityEngine.Serialization;

namespace UI.Behaviours
{
    public class SlideFromHorizontalEdge : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private ChangeValueOverTime _changeValueOverTime = new ChangeValueOverTime();
        [SerializeField] private float _offset;
        [SerializeField] private float _unitsPerSecond;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            var sizeDelta = _rectTransform.sizeDelta;
            var origin = -sizeDelta.x;
            _changeValueOverTime.Start(origin, _offset, _unitsPerSecond);
        }

        private void Update()
        {
            _changeValueOverTime.Tick();

            var position = _rectTransform.localPosition;
            position = new Vector3(_changeValueOverTime.CurrentValue, position.y, position.z);
            _rectTransform.localPosition = position;
        }
    }
}