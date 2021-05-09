using Animations;
using Player;
using UnityEngine;

namespace State.States
{
    public class PlayerFallState : IState
    {
        private Rigidbody2D _rigidBody2D;
        private PlayerMovement _playerMovement;
        private Animator _animator;
        private float _jumpHeight;
        private Collider2D _collider;

        public PlayerFallState(Collider2D collider, Animator playerAnimator, Rigidbody2D rigidBody2D)
        {
            _collider = collider;
            _animator = playerAnimator;
            _rigidBody2D = rigidBody2D;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _animator.SetBool(CachedAnimatorPropertyNames.IsFalling, true);
            _collider.enabled = false;
        }

        public void OnExit()
        {
            _collider.enabled = true;
            _animator.SetBool(CachedAnimatorPropertyNames.IsFalling, false);
        }
    }
}