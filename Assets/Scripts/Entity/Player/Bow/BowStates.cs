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

        private ArcherShootArrowAbilityState _strongArrowState;
        private ArcherApplyFastAttackBuffState _fastAttackBuffState;

        protected override EquipmentPart WeaponEquipmentType => EquipmentPart.Bow;

        public override void Initialize()
        {
            _archer = GetComponent<Player>();
        }

        public override void CreateStates()
        {
            BasicAttackState = new ArcherShootArrowAbilityState(_archer, _basicAttackAbility as ShootArrowAbility);
            
            _strongArrowState = new ArcherShootArrowAbilityState(_archer, _fireArrowAbility);
            _playerStates.AddAttackState(_strongArrowState);
            
            _fastAttackBuffState =
                new ArcherApplyFastAttackBuffState(_archer, _fastAttackBuff);
            _playerStates.AddBuffState(_fastAttackBuffState,() => _fastAttackBuffState.IsBuffApplied);
        }

        protected override void ActivateStates()
        {
            _playerStates.AnimationEvents.BowChargeEndEvent += BowChargeEndEvent;
            BasicAttackState.Ability.Activate();
            _strongArrowState.Ability.Activate();
            _fastAttackBuffState.Ability.Activate();
        }

        protected override void DeActivateStates()
        {
            _playerStates.AnimationEvents.BowChargeEndEvent -= BowChargeEndEvent;
            BasicAttackState.Ability.Deactivate();
            _strongArrowState.Ability.Deactivate();
            _fastAttackBuffState.Ability.Deactivate();
        }

        private void BowChargeEndEvent()
        {
            _playerStates.IsAbilityAnimationActivated = false;
        }
    }
}