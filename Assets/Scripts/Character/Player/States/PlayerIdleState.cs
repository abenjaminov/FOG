using Animations;
using State;
using UnityEngine;

namespace Player.States
{
    public class PlayerIdleState : IState
    {
        private PlayerMovement _playerMovement;
        private Animator _playerAnimator;

        public PlayerIdleState(PlayerMovement playerMovement, Animator playerAnimator)
        {
            _playerMovement = playerMovement;
            _playerAnimator = playerAnimator;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            _playerMovement.SetVelocity(Vector2.zero);
        }

        public void OnExit()
        {

        }
    }
}