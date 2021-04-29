using System;
using Player.States.Archer;
using UnityEngine;

namespace Player.Archer
{
    public class Archer : Player
    {
        private bool _isLeftControlDown;

        private ArcherShootArrowState _shootArrow;
        
        protected override void Awake()
        {
            base.Awake();

            var archerCombat = GetComponent<ArcherCombat>();
            
            var ShouldAttack = new Func<bool>(() => _isLeftControlDown);

            _shootArrow = new ArcherShootArrowState(_animator, archerCombat);
            
            var noHorizontalInput = new Func<bool>(() => _horizontalAxisRaw == 0);
            
            _stateMachine.AddTransition(_idle, noHorizontalInput, _shootArrow);
            
            _stateMachine.AddTransition(_shootArrow, ShouldAttack, _idle);
        }

        protected override void Update()
        {
            _isLeftControlDown = Input.GetKeyDown(KeyCode.LeftControl);
            
            base.Update();
        }
    }
}