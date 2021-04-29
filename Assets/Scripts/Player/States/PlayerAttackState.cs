using Animations;
using State;
using UnityEngine;

namespace Player.States
{
    public abstract class PlayerAttackState : IState
    {
        private Animator _animator;

        protected PlayerAttackState(Animator animator)
        {
            _animator = animator;
        }

        public void Tick() { }

        public virtual void OnEnter()
        {
            _animator.SetTrigger(CachedAnimatorPropertyNames.Attack1);
        }

        public virtual void OnExit()
        {
            
        }
    }
}