using Abilities;
using Abilities.Magic;
using HeroEditor.Common.Enums;
using ScriptableObjects.Inventory.ItemMetas;
using State.States.MagicStates;

namespace Entity.Player.Magic
{
    public class MagicStates : WeaponStates<MagicAttackAbility>
    {
        private Player _player;
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
            AnimationEvents.SlashStartEvent += SlashStartEvent;
            BasicAttackState.Ability.Activate();
        }

        private void SlashStartEvent()
        {
            _combatChannel.OnUseAbility(_player);
        }

        private void SlashEndEvent()
        {
            IsAbilityAnimationActivated = false;
        }

        protected override void DeActivateStates()
        {
            AnimationEvents.SlashEndEvent -= SlashEndEvent;
            AnimationEvents.SlashStartEvent -= SlashStartEvent;
            BasicAttackState.Ability.Deactivate();
        }
        
        protected override void OnWeaponChanged(WeaponItemMeta weapon, EquipmentPart part)
        {
            IsEnabled = weapon != null && weapon.Part == WeaponEquipmentType && weapon.IsStaff;

            TryEnableStates();
        }
    }
}