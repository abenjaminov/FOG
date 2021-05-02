using Animations;
using Player;
using UnityEngine;

namespace State.States
{
    public class PlayerJumpingState : IState
    {
        private Rigidbody2D _rigidBody2D;
        private PlayerMovement _playerMovement;
        private Animator _playerAnimator;
        private float _jumpHeight;
        private Collider2D _collider;

        public PlayerJumpingState(Collider2D collider, Animator playerAnimator, PlayerMovement playerMovement, float jumpHeight, Rigidbody2D rigidBody2D)
        {
            _collider = collider;
            _playerAnimator = playerAnimator;
            _playerMovement = playerMovement;
            _jumpHeight = jumpHeight;
            _rigidBody2D = rigidBody2D;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            var jumpVelocity = Mathf.Sqrt(2 * 9.8f * _jumpHeight);
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, jumpVelocity);
            _rigidBody2D.gravityScale = 1;
            _collider.enabled = false;
            _playerAnimator.SetBool(CachedAnimatorPropertyNames.IsJumping, true);
        }

        public void OnExit()
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, 0);
            _rigidBody2D.gravityScale = 0;
            _collider.enabled = true;
            _playerAnimator.SetBool(CachedAnimatorPropertyNames.IsJumping, false);
        }
    }
}