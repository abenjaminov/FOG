using System;
using Player.States;
using Player.States.Archer;
using UnityEngine;

namespace Player.Archer
{
    public class Archer : Player
    {
        private bool _isLeftControlDown;
        private bool _isShootAnimationActive;
        
        private ArcherShootArrowState _shootArrow;
        
        protected override void Awake()
        {
            base.Awake();

            var archerCombat = GetComponent<ArcherCombat>();
            
            var ShouldAttack = new Func<bool>(() => _isLeftControlDown && !_isShootAnimationActive);

            _shootArrow = new ArcherShootArrowState(_animator,_rigidBody, archerCombat);
            //var transitionToShoot = new TransitionToAttackState(_animator);
            
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
            
            //_stateMachine.AddTransition(_shootArrow, () => _isShootAnimationActive, transitionToShoot,null,"Transition To Shoot -> Shoot Arrow");
        }

        protected override void Update()
        {
            _isLeftControlDown = Input.GetKeyDown(KeyCode.LeftControl);
            
            base.Update();
        }

        public void ShootAnimationEnded()
        {
            _isShootAnimationActive = false;
        }
        
        public void ShootAnimationStarted()
        {
            //Debug.Log("Shoot animation started");
            //_isShootAnimationActive = true;
        }
    }
}