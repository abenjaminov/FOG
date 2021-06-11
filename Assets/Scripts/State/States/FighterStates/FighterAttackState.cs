using Animations;
using Entity;
using Entity.Player.FighterClass;
using UnityEngine;

namespace State.States.FighterStates
{
    public class FighterAttackState : PlayerAttackState
    {
        private Fighter _fighter;
        
        public FighterAttackState(CharacterWrapper character) : 
            base(character)
        {
            _fighter = character as Fighter;
        }
        
        public override void OnEnter()
        {
            base.OnEnter();

            _character.GetCharacter().Slash();
        }
    }
}