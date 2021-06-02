using Character;
using UnityEngine;

namespace State.States.PlayerStates
{
    public class PlayerDieState : DieState
    {
        public PlayerDieState(Assets.HeroEditor.Common.CharacterScripts.Character _character,
            ICharacterMovement characterMovement, Animator animator) : base(_character, characterMovement, animator)
        {
        }
    }
}