using Abilities;
using Abilities.Archer;
using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Entity;
using Entity.Player.FighterClass;
using UnityEngine;

namespace State.States.FighterStates
{
    public class FighterAbilityState : PlayerAbilityState<ShootBasicArrowAbility>
    {
        private Fighter _fighter;
        
        public FighterAbilityState(CharacterWrapper character, ShootBasicArrowAbility ability) : 
            base(character,ability)
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