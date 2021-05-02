using Animations;
using Character;
using Player;
using UnityEngine;

namespace State.States
{
    public class WalkState : IState
    {
        protected ICharacterMovement _characterMovement;
        protected  Animator _animator;
        protected  float _speed;

        public WalkState(ICharacterMovement characterMovement, Animator animator, float speed)
        {
            _characterMovement = characterMovement;
            _animator = animator;
            _speed = speed;
        }

        public virtual void Tick()
        {
            _characterMovement.SetHorizontalVelocity(_speed);
        }

        public virtual void OnEnter()
        {
            _characterMovement.SetHorizontalVelocity(_speed);
            _animator.SetBool(CachedAnimatorPropertyNames.IsWalking, true);
        }

        public virtual void OnExit()
        {
            _animator.SetBool(CachedAnimatorPropertyNames.IsWalking, false);
        }
    }
}