using Animations;
using UnityEngine;

namespace State.States
{
    public abstract class PlayerAttackState : IState
    {
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private float _previousHorizontalVelocity;

        protected PlayerAttackState(Animator animator, Rigidbody2D rigidbody2D)
        {
            _animator = animator;
            _rigidbody2D = rigidbody2D;
        }

        public void Tick() { }

        public virtual void OnEnter()
        {
            _previousHorizontalVelocity = _rigidbody2D.velocity.x;
            _animator.SetTrigger(CachedAnimatorPropertyNames.Attack1);
            
            if (_rigidbody2D.velocity.y == 0)
            {
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            }
        }

        public virtual void OnExit()
        {
            _rigidbody2D.velocity = new Vector2(_previousHorizontalVelocity, _rigidbody2D.velocity.y);
        }
    }
}