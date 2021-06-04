using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Entity;
using UnityEngine;

namespace State.States.PlayerStates
{
    public class PlayerWalkLeftState : PlayerWalkState
    {
        public PlayerWalkLeftState(CharacterWrapper character,
            ICharacterMovement characterMovement, Animator animator, float speed) : 
            base(character, characterMovement, animator, -speed)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            _characterMovement.SetYRotation(180);
        }
    }
}