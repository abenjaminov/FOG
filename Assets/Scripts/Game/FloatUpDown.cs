using System;
using UnityEngine;

namespace Game
{
    public class FloatUpDown : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] private float _segmentTime;
        private float _timeInSegment;
        private int direction = 1;
        private Vector2 _target;

        private void OnEnable()
        {
            _target = transform.position + (Vector3.down * distance);
        }

        private void Update()
        {
            _timeInSegment += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, _target, _timeInSegment / _segmentTime);

            if (Vector3.Distance(transform.position, _target) <= 0.01)
            {
                transform.position = _target;
                direction *= -1;
                _target = transform.position + (Vector3.up * (direction * 2 * distance));
                _timeInSegment = 0;
            }
        }
    }
}