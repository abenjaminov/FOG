using Entity.Player;
using Player;
using ScriptableObjects.Channels;
using UnityEngine;

namespace State.States.PlayerStates
{
    public class PlayerClimbDownState : PlayerClimbState
    {
        public PlayerClimbDownState(Entity.Player.Player player, PlayerClimb playerClimb, PlayerMovement playerMovement, Rigidbody2D rigidBody, Collider2D collider, InputChannel inputChannel, float climbSpeed) : base(player, playerClimb, playerMovement, rigidBody, collider, inputChannel, climbSpeed)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            _subscriptions.Add(_inputChannel.SubscribeKeyDown(KeyCode.DownArrow, ClimbDown));
        }

        private void ClimbDown()
        {
            _playerMovement.SetVerticalVelocity(-_climbSpeed);
        }
    }
}