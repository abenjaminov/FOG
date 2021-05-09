using Game;
using UnityEngine;

namespace State.States.DropStates
{
    public class DroppedState : IState
    {
        private Transform _dropTransform;
        private FloatUpDown _floatUpDown;
        private float _dropHeight;
        private FadeoutToPoint _fadeoutToPoint;
        
        private float _speed;

        public DroppedState(FloatUpDown floatUpDown, 
            Transform dropTransform,
            float dropHeight, 
            FadeoutToPoint fadeoutToPoint)
        {
            _floatUpDown = floatUpDown;
            _dropTransform = dropTransform;
            _dropHeight = dropHeight;
            _fadeoutToPoint = fadeoutToPoint;
        }
        
        public void Tick()
        {
            _dropTransform.Translate(Vector3.up * (_speed * Time.deltaTime));
            _speed -= 9.8f * Time.deltaTime;
        }

        public void OnEnter()
        {
            _speed = Mathf.Sqrt(2 * 9.8f * (_dropHeight + Random.Range(-.05f,.05f)));
            _floatUpDown.enabled = false;
            _fadeoutToPoint.enabled = false;
        }

        public void OnExit()
        { }
    }
}