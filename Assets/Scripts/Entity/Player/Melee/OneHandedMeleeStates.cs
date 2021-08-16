using Abilities.Melee;
using HeroEditor.Common.Enums;
using Player;
using ScriptableObjects.Inventory.ItemMetas;
using State.States.FighterStates;

namespace Entity.Player.Melee
{
    public class OneHandedMeleeStates : WeaponStates<SlashAbility>
    {
        private Entity.Player.Player _player;

        protected override EquipmentPart WeaponEquipmentType => EquipmentPart.MeleeWeapon1H;
        
        public override void LinkToStates(PlayerStates playerStates)
        {
            _player = GetComponent<Player>();
            _playerStates = playerStates;
            BasicAttackState = new FighterSlashState(_player, _basicAttackAbility as SlashAbility);
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

        protected override void OnWeaponChanged(WeaponItemMeta weapon)
        {
            IsEnabled = weapon.Part == WeaponEquipmentType && !weapon.IsStaff;

            if (IsEnabled)
            {
                ActivateStates();
            }
        }
    }
}