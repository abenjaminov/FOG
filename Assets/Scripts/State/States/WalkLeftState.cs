using Animations;
using Character;
using Player;
using UnityEngine;

namespace State.States
{
    public class WalkLeftState : WalkState
    {
        public WalkLeftState(Assets.HeroEditor.Common.CharacterScripts.Character character,
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