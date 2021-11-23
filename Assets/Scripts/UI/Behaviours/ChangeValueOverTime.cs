using UnityEngine;

namespace UI.Behaviours
{
    public class ChangeValueOverTime
    {
        private float _destination;
        private float _origin;
        private float _tolerance = 0.05f;
        private float _interpelationValue = 0;
        private float _totalUnits;
        public float CurrentValue;

        private float _unitsPerSecond;
        public float UnitsPerSecond => _unitsPerSecond;

        public void Start(float origin, float destination, float unitsPerSecond)
        {
            _origin = origin;
            _destination = destination;
            _unitsPerSecond = unitsPerSecond;
            _totalUnits = Mathf.Abs(destination - origin);
            CurrentValue = origin;
            _interpelationValue = 0;
        }

        public void Tick()
        {
            if ((_interpelationValue / _totalUnits) >= 1f) return;

            _interpelationValue += (Time.deltaTime * _unitsPerSecond);
            
            CurrentValue = Mathf.Lerp(_origin, _destination, _interpelationValue / _totalUnits);
        }
    }
}