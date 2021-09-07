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

        public override void Initialize()
        {
            _player = GetComponent<Player>();
            BasicAttackState = new FighterSlashState(_player, _basicAttackAbility as SlashAbility);
        }

        public override void HookStates()
        {
            
        }

        protected override void ActivateStates()
        {
            AnimationEvents.SlashEndEvent += SlashEndEvent;
            BasicAttackState.Ability.Activate();
        }

        protected override void DeActivateStates()
        {
            AnimationEvents.SlashEndEvent -= SlashEndEvent;
            BasicAttackState.Ability.Deactivate();
        }

        private void SlashEndEvent()
        {
            IsAbilityAnimationActivated = false;
        }

        protected override void OnWeaponChanged(WeaponItemMeta weapon, EquipmentPart part)
        {
            IsEnabled = weapon != null && weapon.Part == WeaponEquipmentType && !weapon.IsStaff;

            TryEnableStates();
        }
    }
}