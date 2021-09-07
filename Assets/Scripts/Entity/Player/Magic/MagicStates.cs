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
            BasicAttackState = new MagicAttackState(_player, _basicAttackAbility as MagicAttackAbility);
        }

        public override void HookStates()
        {
            
        }

        protected override void ActivateStates()
        {
            AnimationEvents.SlashEndEvent += SlashEndEvent;
            BasicAttackState.Ability.Activate();
        }

        private void SlashEndEvent()
        {
            IsAbilityAnimationActivated = false;
        }

        protected override void DeActivateStates()
        {
            AnimationEvents.SlashEndEvent -= SlashEndEvent;
            BasicAttackState.Ability.Deactivate();
        }
        
        protected override void OnWeaponChanged(WeaponItemMeta weapon, EquipmentPart part)
        {
            IsEnabled = weapon != null && weapon.Part == WeaponEquipmentType && weapon.IsStaff;

            TryEnableStates();
        }
    }
}