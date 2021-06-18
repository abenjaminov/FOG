using System;
using Abilities.Archer;
using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Player;
using State.States.ArcherStates;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entity.Player.ArcherClass
{
    public class ArcherStates : PlayerStates
    {
        private Archer _archer;

        [Header("Shoot Fire Arrow Ability")]
        [SerializeField] private ShootArrowAbility _fireArrowAbility;

        [Header("Fast Attack Buff")]
        [SerializeField] private FastAttackBuff _fastAttackBuff;

        protected override void Start()
        {
            _archer = GetComponent<Archer>();
            _basicAttackState = new ArcherShootArrowAbilityState(_archer, _basicAttackAbility as ShootArrowAbility);
            
            base.Start();

            _animationEvents.BowChargeEndEvent += BowChargeEndEvent;
            
            var strongArrowState = new ArcherShootArrowAbilityState(_archer,_fireArrowAbility);
            
            AddAbilityState(strongArrowState, _shouldAbility, _attackTransitionLogic);

            var fastAttackBuffState =
                new ArcherApplyFastAttackBuffState(_archer, _fastAttackBuff);
            
            AddAbilityState(fastAttackBuffState, _shouldAbility, _buffTransitionLogic,() => fastAttackBuffState.IsBuffApplied);
        }

        private void BowChargeEndEvent()
        {
            _isAbilityAnimationActivated = false;
        }
    }
}