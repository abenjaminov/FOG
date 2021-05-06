using Character;
using UnityEngine;

namespace State.States.PlayerStates
{
    public class PlayerDieState : DieState
    {
        public PlayerDieState(ICharacterMovement characterMovement, Animator animator) : base(characterMovement, animator)
        {
        }
    }
}