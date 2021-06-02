using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Player;
using UnityEngine;

namespace State.States
{
    public class PlayerJumpingState : IState
    {
        private Assets.HeroEditor.Common.CharacterScripts.Character _character;
        private Rigidbody2D _rigidBody2D;
        private PlayerMovement _playerMovement;
        private Animator _playerAnimator;
        private float _jumpHeight;
        private Collider2D _collider;
        private CharacterState _previousState;

        public PlayerJumpingState(Collider2D collider, Animator playerAnimator, PlayerMovement playerMovement, float jumpHeight, Rigidbody2D rigidBody2D, Assets.HeroEditor.Common.CharacterScripts.Character character)
        {
            _collider = collider;
            _playerAnimator = playerAnimator;
            _playerMovement = playerMovement;
            _jumpHeight = jumpHeight;
            _rigidBody2D = rigidBody2D;
            _character = character;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            var jumpVelocity = Mathf.Sqrt(2 * 9.8f * _jumpHeight);
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, jumpVelocity);
            _collider.enabled = false;
            //_playerAnimator.SetBool(CachedAnimatorPropertyNames.IsJumping, true);
            _previousState = _character.GetState();
            _character.SetState(CharacterState.Jump);
        }

        public void OnExit()
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, 0);
            _collider.enabled = true;
            //_playerAnimator.SetBool(CachedAnimatorPropertyNames.IsJumping, false);
            _character.SetState(_previousState);
        }
    }
}