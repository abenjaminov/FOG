using Game;
using Player;
using UnityEngine;

namespace State.States.DropStates
{
    public class FloatState : IState
    {
        private FloatUpDown _floatComponent;
        private GroundCheck _groundCheck;
        private Collider2D _collider2D;
        private Transform _dropTransform;

        public FloatState(FloatUpDown floatComponent, 
                          GroundCheck groundCheck, 
                          Collider2D collider2D,
                          Transform dropTransform)
        {
            _floatComponent = floatComponent;
            _groundCheck = groundCheck;
            _collider2D = collider2D;
            _dropTransform = dropTransform;
        }

        public void Tick()
        { }

        public void OnEnter()
        {
            _floatComponent.enabled = true;
            _groundCheck.enabled = false;
            
            var position = _dropTransform.position;
            position = new Vector3(position.x, _groundCheck.CurrentPlatformY - _collider2D.offset.y, position.z);
            _dropTransform.position = position;
        }

        public void OnExit()
        { }
    }
}