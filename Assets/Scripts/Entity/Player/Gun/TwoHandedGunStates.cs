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
        
        public override void LinkToStates(PlayerStates playerStates)
        {
            _player = GetComponent<Player>();
            _playerStates = playerStates;
            BasicAttackState = new ShootState(_player, _basicAttackAbility as ShootAttackAbility);
        }

        protected override void ActivateStates()
        {
            _playerStates.AnimationEvents.ShootEndEvent += ShootEndEvent;
        }

        protected override void DeActivateStates()
        {
            _playerStates.AnimationEvents.ShootEndEvent -= ShootEndEvent;
        }
        
        private void ShootEndEvent()
        {
            
        }
    }
}