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
        
        [Header("Shoot Arrow Ability")]
        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] protected Transform _fireTransform;
        
        [Header("Shoot Fire Arrow Ability")]
        [SerializeField] private GameObject _fireArrowPrefab;


        [Header("Fast Attack Buff")] 
        [SerializeField] private float _fastAttackFactor;
        [SerializeField] private float _fastAttackBuffTime;
        [SerializeField] private Sprite _fastAttackBuffSprite;
        
        protected override void Start()
        {
            _archer = GetComponent<Archer>();
            _basicAttackState = new ArcherShootArrowAbilityState(_archer, new ShootBasicArrowAbility(_archer, 
                KeyCode.LeftControl, 
                _arrowPrefab, 
                _fireTransform));
            
            base.Start();

            _animationEvents.BowChargeEndEvent += BowChargeEndEvent;
            
            var strongArrowState = new ArcherShootArrowAbilityState(_archer,
                new FireArrowAbility(_archer, KeyCode.X, _fireArrowPrefab, _fireTransform));
            
            AddAbilityState(strongArrowState, _shouldBasicAttack, _attackTransitionLogic);

            var fastAttackBuffState =
                new ArcherApplyFastAttackBuffState(_archer, new FastAttackBuff(_archer, KeyCode.C, _fastAttackBuffTime, _fastAttackBuffSprite, _fastAttackFactor));
            
            AddAbilityState(fastAttackBuffState, _shouldBasicAttack, _buffTransitionLogic,() => fastAttackBuffState.IsBuffApplied);
        }

        private void BowChargeEndEvent()
        {
            _isAbilityAnimationActivated = false;
        }
    }
}