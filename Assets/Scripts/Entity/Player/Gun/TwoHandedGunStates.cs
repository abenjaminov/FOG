using Abilities.Gun;
using HeroEditor.Common.Enums;
using Player;
using State.States.GunStates;

namespace Entity.Player.Gun
{
    public class TwoHandedGunStates : WeaponStates<ShootAttackAbility>
    {
        private Entity.Player.Player _player;
        protected override EquipmentPart WeaponEquipmentType => EquipmentPart.Firearm2H;
        
        public override void HookStates()
        {
            _player = GetComponent<Player>();
            BasicAttackState = new ShootState(_player, _basicAttackAbility as ShootAttackAbility);
        }

        protected override void ActivateStates()
        {
            AnimationEvents.ShootEndEvent += ShootEndEvent;
        }

        protected override void DeActivateStates()
        {
            AnimationEvents.ShootEndEvent -= ShootEndEvent;
        }
        
        private void ShootEndEvent()
        {
            
        }
    }
}