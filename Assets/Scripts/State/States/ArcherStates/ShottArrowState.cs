using Abilities.Archer;
using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Entity;
using Entity.Player.ArcherClass;
using UnityEngine;

namespace State.States.ArcherStates
{
    public class ArcherShootArrowAbilityState : PlayerAbilityState<ShootBasicArrowAbility>
    {
        private Archer _archer;

        public ArcherShootArrowAbilityState(CharacterWrapper character, ShootBasicArrowAbility ability) : base(character, ability)
        {
            _archer = character as Archer;
            _animationEvents.BowChargeEndEvent += BowChargeEndEvent;
        }

        private void BowChargeEndEvent()
        {
            _archer.GetCharacter().Animator.SetInteger(CachedAnimatorPropertyNames.Charge, 2);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _character.GetCharacter().Animator.SetInteger(CachedAnimatorPropertyNames.Charge, 1);
            
            Ability.WorldMovementDirection =
                (int)_archer.transform.rotation.y != 0 ? Vector2.left : Vector2.right;
        }
    }
}