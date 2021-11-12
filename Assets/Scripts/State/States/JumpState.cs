using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Player;
using UnityEngine;

namespace State.States
{
    public class PlayerJumpingState : IState
    {
        private Entity.Player.Player _player;
        private Rigidbody2D _rigidBody2D;
        private PlayerMovement _playerMovement;
        private float _jumpHeight;
        private CharacterState _previousState;
        private Collider2D _collider;

        public PlayerJumpingState(Entity.Player.Player player,
            PlayerMovement playerMovement, 
            float jumpHeight, 
            Rigidbody2D rigidBody2D,
            Collider2D collider)
        {
            _collider = collider;
            _playerMovement = playerMovement;
            _jumpHeight = jumpHeight;
            _rigidBody2D = rigidBody2D;
            _player = player;
        }

        public void Tick()
        {
            
        }

        public virtual void OnEnter()
        {
            var jumpVelocity = Mathf.Sqrt(2 * 9.8f * _jumpHeight);
            _playerMovement.SetVelocity(new Vector2(_rigidBody2D.velocity.x, jumpVelocity));
            _collider.enabled = false;
            _previousState = _player.GetCharacter().GetState();
            _player.GetCharacter().SetState(CharacterState.Jump);
        }

        public void OnExit()
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, 0);
            _collider.enabled = true;
            _player.GetCharacter().SetState(_previousState);
        }
    }
}