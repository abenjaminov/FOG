using Animations;
using Character;
using UnityEngine;

namespace State.States
{
    public class DieState : IState
    {
        public float TimeDead = 0;
        private ICharacterMovement _characterMovement;
        private Animator _animator;

        protected DieState(ICharacterMovement characterMovement, Animator animator)
        {
            _characterMovement = characterMovement;
            _animator = animator;
        }

        public virtual void Tick()
        {
            TimeDead += Time.deltaTime;
        }

        public virtual void OnEnter()
        {
            TimeDead = 0;
            _characterMovement.SetVelocity(Vector2.zero);
            _animator.SetTrigger(CachedAnimatorPropertyNames.Dead);
        }

        public virtual void OnExit()
        {
            TimeDead = 0;
        }
    }
}