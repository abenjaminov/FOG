using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Player;
using UnityEngine;

namespace State.States
{
    public class IdleState : IState
    {
        private Assets.HeroEditor.Common.CharacterScripts.Character _character;
        private ICharacterMovement _characterMovement;
        public float IdleTime;
        
        public IdleState(ICharacterMovement characterMovement, Assets.HeroEditor.Common.CharacterScripts.Character character)
        {
            _characterMovement = characterMovement;
            _character = character;
        }

        public void Tick()
        {
            IdleTime += Time.deltaTime;
        }

        public void OnEnter()
        {
            IdleTime = 0;
            _character.SetState(CharacterState.Idle);
            _characterMovement.SetVelocity(Vector2.zero);
        }

        public void OnExit()
        {

        }
    }
}