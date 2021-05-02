using Animations;
using Character;
using Player;
using UnityEngine;

namespace State.States
{
    public class WalkRightState : WalkState
    {
        public WalkRightState(ICharacterMovement characterMovement, Animator animator, float speed) : 
            base(characterMovement, animator, speed)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _characterMovement.SetYRotation(0);
        }
    }
}