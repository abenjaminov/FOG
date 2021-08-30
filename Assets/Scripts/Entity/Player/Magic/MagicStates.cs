using Abilities.Magic;
using HeroEditor.Common.Enums;
using ScriptableObjects.Inventory.ItemMetas;
using State.States.MagicStates;
using UnityEngine;

namespace Entity.Player.Magic
{
    public class MagicStates : WeaponStates<MagicAttackAbility>
    {
        private Entity.Player.Player _player;
        protected override EquipmentPart WeaponEquipmentType => EquipmentPart.MeleeWeapon1H;

        public override void Initialize()
        {
            _player = GetComponent<Player>();
        }
        
        public override void CreateStates()
        {
            BasicAttackState = new MagicAttackState(_player, _basicAttackAbility as MagicAttackAbility);
        }

        protected override void ActivateStates()
        {
            _playerStates.AnimationEvents.SlashEndEvent += SlashEndEvent;
            BasicAttackState.Ability.Activate();
        }

        private void SlashEndEvent()
        {
            _playerStates.IsAbilityAnimationActivated = false;
        }

        protected override void DeActivateStates()
        {
            _playerStates.AnimationEvents.SlashEndEvent -= SlashEndEvent;
            BasicAttackState.Ability.Deactivate();
        }
        
        protected override void OnWeaponChanged(WeaponItemMeta weapon, EquipmentPart part)
        {
            IsEnabled = weapon != null && weapon.Part == WeaponEquipmentType && weapon.IsStaff;

            if (IsEnabled)
            {
                ActivateStates();
            }
        }
    }
}