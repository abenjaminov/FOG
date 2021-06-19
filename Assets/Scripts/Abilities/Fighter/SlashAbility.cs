using System;
using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

namespace Abilities.Fighter
{
    public class SlashAbility : MeleeAttack
    {
        private AnimationEvents _animationEvents;
        protected void Start()
        {
            _animationEvents = GetComponentInChildren<AnimationEvents>();
            _animationEvents.MeleeStrikeEvent += OnMeleeHit;
        }

        public override void Use()
        {
            _hostWrapper.GetCharacter().Slash();
        }
    }
}