using Abilities.Bow;
using HeroEditor.Common.Enums;
using Player;
using State.States.ArcherStates;
using UnityEngine;

namespace Entity.Player.Bow
{
    public class BowStates : WeaponStates<ShootArrowAbility>
    {
        private Player _archer;

        [Header("Shoot Fire Arrow Ability")]
        [SerializeField] private ShootArrowAbility _fireArrowAbility;

        [Header("Fast Attack Buff")]
        [SerializeField] private FastAttackBuff _fastAttackBuff;

        protected override EquipmentPart WeaponEquipmentType => EquipmentPart.Bow;

        public override void Initialize()
        {
            _archer = GetComponent<Player>();
        }

        public override void CreateStates()
        {
            BasicAttackState = new ArcherShootArrowAbilityState(_archer, _basicAttackAbility as ShootArrowAbility);
            
            var strongArrowState = new ArcherShootArrowAbilityState(_archer, _fireArrowAbility);
            _playerStates.AddAttackState(strongArrowState);
            
            var fastAttackBuffState =
                new ArcherApplyFastAttackBuffState(_archer, _fastAttackBuff);
            _playerStates.AddBuffState(fastAttackBuffState,() => fastAttackBuffState.IsBuffApplied);
        }

        protected override void ActivateStates()
        {
            _playerStates.AnimationEvents.BowChargeEndEvent += BowChargeEndEvent;
        }

        protected override void DeActivateStates()
        {
            _playerStates.AnimationEvents.BowChargeEndEvent -= BowChargeEndEvent;
        }

        private void BowChargeEndEvent()
        {
            _playerStates.IsAbilityAnimationActivated = false;
        }
    }
}