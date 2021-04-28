using Animations;
using UnityEngine;

namespace Player.States
{
    public class PlayerWalkLeftState : PlayerWalkingState
    {
        public PlayerWalkLeftState(PlayerMovement playerMovement, Animator animator, float speed) : 
            base(playerMovement, animator, speed)
        {
        }
        
        public override void OnEnter()
        {
            _playerMovement.SetHorizontalVelocity(-_speed);
            _animator.SetBool(CachedAnimatorPropertyNames.IsWalking, true);
            _playerMovement.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        public override void OnExit()
        {
            _animator.SetBool(CachedAnimatorPropertyNames.IsWalking, false);
        }
    }
}