using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Player;
using UnityEngine;

namespace State.States
{
    public class PlayerFallState : IState
    {
        private PlayerMovement _playerMovement;
        private float _jumpHeight;
        private Collider2D _collider;
        private Entity.Player.Player _player;
        private CharacterState _previousState;
        
        public PlayerFallState(Entity.Player.Player player, Collider2D collider)
        {
            _collider = collider;
            _player = player;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _collider.enabled = false;
            _previousState = _player.GetCharacter().GetState();
            _player.GetCharacter().SetState(CharacterState.Jump);
        }

        public void OnExit()
        {
            _collider.enabled = true;
            _player.GetCharacter().SetState(_previousState);
        }
    }
}