using System;
using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Player;
using State.States.ArcherStates;
using UnityEngine;

namespace Entity.Player.ArcherClass
{
    public class ArcherStates : PlayerStates
    {
        private Archer _archer;
        private AnimationEvents _animationEvents;
        
        protected override void Start()
        {
            _archer = GetComponent<Archer>();
            _attack = new ArcherShootArrowState(_archer);
            
            base.Start();

            _animationEvents = GetComponentInChildren<AnimationEvents>();
            _animationEvents.BowChargeEndEvent += BowChargeEndEvent;
        }

        private void BowChargeEndEvent()
        {
            _archer.GetCharacter().Animator.SetInteger(CachedAnimatorPropertyNames.Charge, 2);
            _isAttackAnimationActivated = false;
        }
    }
}