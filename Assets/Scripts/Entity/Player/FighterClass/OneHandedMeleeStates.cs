using Abilities.Fighter;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common.Enums;
using Player;
using State.States.FighterStates;

namespace Entity.Player.FighterClass
{
    public class OneHandedMeleeStates : WeaponStates<SlashAbility>
    {
        private Entity.Player.Player _fighter;

        protected override EquipmentPart WeaponEquipmentType => EquipmentPart.MeleeWeapon1H;
        
        public override void LinkToStates(PlayerStates playerStates)
        {
            _fighter = GetComponent<Entity.Player.Player>();
            _playerStates = playerStates;
            BasicAttackState = new FighterSlashState(_fighter, _basicAttackAbility as SlashAbility);
        }

        protected override void ActivateStates()
        {
            _playerStates.AnimationEvents.SlashEndEvent += SlashEndEvent;
        }

        protected override void DeActivateStates()
        {
            _playerStates.AnimationEvents.SlashEndEvent -= SlashEndEvent;
        }

        private void SlashEndEvent()
        {
            _playerStates.IsAbilityAnimationActivated = false;
        }
    }
}