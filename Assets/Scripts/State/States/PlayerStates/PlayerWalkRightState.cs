using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Entity;
using Player;
using State.States.PlayerStates;
using UnityEngine;

namespace State.States
{
    public class PlayerWalkRightState : PlayerWalkState
    {
        public PlayerWalkRightState(CharacterWrapper character,
            ICharacterMovement characterMovement, Animator animator, float speed) : 
            base(character, characterMovement, animator, speed)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _characterMovement.SetYRotation(0);
        }
    }
}