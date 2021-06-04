﻿using System;
using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using State.States.ArcherStates;
using UnityEngine;

namespace Player.Archer
{
    public class ArcherStates : PlayerStates
    {
        private bool _isLeftControlDown;
        private bool _isShootAnimationActive = false;
        
        private ArcherShootArrowState _shootArrow;

        private Character.Player.ArcherCharacter.Archer _archer;
        private AnimationEvents _animationEvents;
        
        protected override void Awake()
        {
            base.Awake();

            _animationEvents = GetComponentInChildren<AnimationEvents>();
            _animationEvents.BowChargeEndEvent += BowChargeEndEvent;
            
            _archer = GetComponent<Character.Player.ArcherCharacter.Archer>();
            

            var ShouldAttack = new Func<bool>(() => _isLeftControlDown && !_isShootAnimationActive);

            _shootArrow = new ArcherShootArrowState(_archer, _animator,_rigidBody);

            var noHorizontalInput = new Func<bool>(() => _horizontalAxisRaw == 0 && !_isShootAnimationActive);
            var walkLeft = new Func<bool>(() => _horizontalAxisRaw < 0 && 
                                                     (int)_rigidBody.velocity.y == 0 && 
                                                     !_isShootAnimationActive);
            var walkRight = new Func<bool>(() => _horizontalAxisRaw > 0 && 
                                                (int)_rigidBody.velocity.y == 0 && 
                                                !_isShootAnimationActive);
            var setShootAnimationActive = new Action(() => _isShootAnimationActive = true);
            
            _stateMachine.AddTransition(_idle, noHorizontalInput, _shootArrow,null, "Shoot Arrow -> Idle");
            _stateMachine.AddTransition(_walkLeft, walkLeft, _shootArrow, null,"Shoot Arrow -> Walk Left");
            _stateMachine.AddTransition(_walkRight, walkRight, _shootArrow, null,"Shoot Arrow -> Walk Right");
            
            _stateMachine.AddTransition(_shootArrow, ShouldAttack, _idle, setShootAnimationActive,"Idle -> Transition To Shoot");
            _stateMachine.AddTransition(_shootArrow, ShouldAttack, _walkLeft, setShootAnimationActive,"Walk Left -> Transition To Shoot");
            _stateMachine.AddTransition(_shootArrow, ShouldAttack, _walkRight, setShootAnimationActive,"Walk Right -> Transition To Shoot");
            _stateMachine.AddTransition(_shootArrow, ShouldAttack, _shootArrow, setShootAnimationActive,"Walk Right -> Transition To Shoot");
        }

        private void BowChargeEndEvent()
        {
            _archer.GetCharacter().Animator.SetInteger(CachedAnimatorPropertyNames.Charge, 2);
            _isShootAnimationActive = false;
        }

        protected override void Update()
        {
            _isLeftControlDown = Input.GetKey(KeyCode.LeftControl);
            
            base.Update();
        }
    }
}