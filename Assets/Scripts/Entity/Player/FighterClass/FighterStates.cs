using System;
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
            
            _attack = new FighterAttackState(_fighter);
            
            base.Start();
        }
    }
}