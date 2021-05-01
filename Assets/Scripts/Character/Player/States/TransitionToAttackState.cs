using Animations;
using State;
using UnityEngine;

namespace Player.States
{
    public class TransitionToAttackState : IState
    {
        private Animator _animator;

        public TransitionToAttackState(Animator animator)
        {
            _animator = animator;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            _animator.SetTrigger(CachedAnimatorPropertyNames.Attack1);
        }

        public void OnExit()
        {
            
        }
    }
}