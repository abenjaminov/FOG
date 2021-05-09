using Game;
using UnityEngine;

namespace State.States.DropStates
{
    public class PickedUpState : IState
    {
        private FloatUpDown _floatComponent;
        private FadeoutToPoint _fadeoutToPoint;
        private Transform _playerTransform;
        private Transform _dropTransform;

        public bool PickupDone;

        public PickedUpState(FloatUpDown floatComponent, 
                             FadeoutToPoint fadeoutToPoint,
                             Transform dropTransform)
        {
            _floatComponent = floatComponent;
            _fadeoutToPoint = fadeoutToPoint;
            _dropTransform = dropTransform;
        }

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }
        
        public void Tick()
        {
            if (Vector3.Distance(_playerTransform.position, _dropTransform.position) <= 0.1f)
            {
                PickupDone = true;
                Object.Destroy(_dropTransform.gameObject);
            }
        }

        public void OnEnter()
        {
            _floatComponent.enabled = false;
            _fadeoutToPoint.enabled = true;
            _fadeoutToPoint.SetTransformToFollow(_playerTransform);
        }

        public void OnExit()
        { }
    }
}