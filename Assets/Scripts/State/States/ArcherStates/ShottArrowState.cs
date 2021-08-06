using Abilities.Bow;
using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Entity;
using Entity.Player.ArcherClass;
using UnityEngine;

namespace State.States.ArcherStates
{
    public class ArcherShootArrowAbilityState : PlayerAbilityState<ShootArrowAbility>
    {
        private Entity.Player.Player _archer;

        public ArcherShootArrowAbilityState(CharacterWrapper character, ShootArrowAbility ability) : base(character, ability)
        {
            _archer = character as Entity.Player.Player;
            _animationEvents.BowChargeEndEvent += BowChargeEndEvent;
        }

        private void BowChargeEndEvent()
        {
            _archer.GetCharacter().Animator.SetInteger(CachedAnimatorPropertyNames.Charge, 2);
        }

        public override void OnEnter()
        {
            _character.GetCharacter().Animator.SetInteger(CachedAnimatorPropertyNames.Charge, 1);
            
            base.OnEnter();
        }

        public override void OnExit()
        {
            Ability.Use();
            base.OnExit();
        }
    }
}