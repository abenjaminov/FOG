using System;
using Abilities.Archer;
using Assets.HeroEditor.Common.CharacterScripts;
using Player;
using State.States.FighterStates;
using UnityEngine;

namespace Entity.Player.FighterClass
{
    public class FighterStates : PlayerStates
    {
        private Fighter _fighter;

        protected override void Start()
        {
            _fighter = GetComponent<Fighter>();
            
            _basicAttackState = new FighterAbilityState(_fighter, new ShootBasicArrowAbility(_fighter,KeyCode.LeftControl,1, null, null));
            
            base.Start();
        }
    }
}