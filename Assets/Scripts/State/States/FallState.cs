using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
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
        private Assets.HeroEditor.Common.CharacterScripts.Character _character;
        private CharacterState _previousState;
        
        public PlayerFallState(Collider2D collider, Animator playerAnimator, Rigidbody2D rigidBody2D, Assets.HeroEditor.Common.CharacterScripts.Character character)
        {
            _collider = collider;
            _animator = playerAnimator;
            _rigidBody2D = rigidBody2D;
            _character = character;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _animator.SetBool(CachedAnimatorPropertyNames.IsFalling, true);
            _collider.enabled = false;
            _previousState = _character.GetState();
            _character.SetState(CharacterState.Jump);
        }

        public void OnExit()
        {
            _collider.enabled = true;
            _animator.SetBool(CachedAnimatorPropertyNames.IsFalling, false);
            _character.SetState(_previousState);
        }
    }
}