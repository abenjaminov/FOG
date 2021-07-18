using System.Collections.Generic;
using Entity;
using Entity.Player;
using Player;
using ScriptableObjects.Channels;
using UnityEngine;

namespace State.States.PlayerStates
{
    public class PlayerClimbState : IState
    {
        protected Entity.Player.Player _player;
        protected PlayerMovement _playerMovement;
        protected InputChannel _inputChannel;
        protected PlayerClimb _playerClimb;
        protected float _climbSpeed;
        protected Rigidbody2D _rigidbody2D;
        protected Collider2D _collider;

        private int _direction;

        protected List<KeySubscription> _subscriptions = new List<KeySubscription>();
        
        public PlayerClimbState(Entity.Player.Player player,
            PlayerClimb playerClimb,
            PlayerMovement playerMovement,
            Rigidbody2D rigidBody,
            Collider2D collider,
            InputChannel inputChannel,
            float climbSpeed)
        {
            _player = player;
            _playerClimb = playerClimb;
            _playerMovement = playerMovement;
            _rigidbody2D = rigidBody;
            _collider = collider;
            _inputChannel = inputChannel;
            _climbSpeed = climbSpeed;
        }

        public void Tick()
        {
            
        }

        public virtual void OnEnter()
        {
            _player.Climb();    
            _playerMovement.SetHorizontalVelocity(0);
            _rigidbody2D.gravityScale = 0;
            _collider.enabled = false;
            var playerTransform = _playerMovement.transform;
            var playerPosition = playerTransform.position;
            
            playerPosition = new Vector3(_playerClimb.CurrentLadder.Center.x,
                playerPosition.y, playerPosition.z);
            
            playerTransform.position = playerPosition;

            _subscriptions.Add(_inputChannel.SubscribeKeyDown(KeyCode.UpArrow, ClimbUp));
            _subscriptions.Add(_inputChannel.SubscribeKeyDown(KeyCode.DownArrow, ClimbDown));
            _subscriptions.Add(_inputChannel.SubscribeKeyUp(KeyCode.UpArrow, Stop));
            _subscriptions.Add(_inputChannel.SubscribeKeyUp(KeyCode.DownArrow, Stop));

            if (_playerClimb.CurrentLadder.Center.y > _player.transform.position.y)
            {
                ClimbUp();
            }
            else
            {
                ClimbDown();
            }
        }

        private void ClimbUp()
        {
            _playerMovement.SetVerticalVelocity(_climbSpeed);
        }
        
        private void ClimbDown()
        {
            _playerMovement.SetVerticalVelocity(-_climbSpeed);
        }
        
        private void Stop()
        {
            _playerMovement.SetVerticalVelocity(0);
        }

        public void OnExit()
        {
            _subscriptions.ForEach(x => x.Unsubscribe());
            _subscriptions.Clear();
            _rigidbody2D.gravityScale = 1;
            _collider.enabled = true;
        }
    }
}