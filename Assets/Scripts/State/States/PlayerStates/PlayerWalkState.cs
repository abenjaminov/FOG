using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Entity;
using UnityEngine;

namespace State.States.PlayerStates
{
    public class PlayerWalkState : WalkState
    {
        protected CharacterState _previousState;
        private CharacterWrapper _character;
        
        public PlayerWalkState(CharacterWrapper character, ICharacterMovement characterMovement, Animator animator, float speed) 
            : base(character, characterMovement, animator, speed)
        {
            _character = character;
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            _characterMovement.SetYRotation(180);
            _previousState = _character.GetCharacter().GetState();
            _character.GetCharacter().SetState(CharacterState.Walk);
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            _character.GetCharacter().SetState(_previousState);
        }
    }
}