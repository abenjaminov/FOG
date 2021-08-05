using System;
using Abilities;
using Abilities.Archer;
using HeroEditor.Common.Enums;
using Player;
using State.States;
using State.States.ArcherStates;
using UnityEngine;

namespace Entity.Player.ArcherClass
{
    public class BowStates : WeaponStates<ShootArrowAbility>
    {
        private Player _archer;

        [Header("Shoot Fire Arrow Ability")]
        [SerializeField] private ShootArrowAbility _fireArrowAbility;

        [Header("Fast Attack Buff")]
        [SerializeField] private FastAttackBuff _fastAttackBuff;

        protected override EquipmentPart WeaponEquipmentType => EquipmentPart.Bow;

        public override void LinkToStates(PlayerStates playerStates)
        {
            _archer = GetComponent<Player>();
            _playerStates = playerStates;
            BasicAttackState = new ArcherShootArrowAbilityState(_archer, _basicAttackAbility as ShootArrowAbility);
        }

        protected override void ActivateStates()
        {
            _playerStates.AnimationEvents.BowChargeEndEvent += BowChargeEndEvent;
            var strongArrowState = new ArcherShootArrowAbilityState(_archer, _fireArrowAbility);
            
            _playerStates.AddAttackState(strongArrowState);

            var fastAttackBuffState =
                new ArcherApplyFastAttackBuffState(_archer, _fastAttackBuff);
            
            _playerStates.AddBuffState(fastAttackBuffState,() => fastAttackBuffState.IsBuffApplied);
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