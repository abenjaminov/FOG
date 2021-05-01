using Animations;
using State;
using UnityEngine;

namespace Player.States
{
    public class PlayerWalkingState : IState
    {
        protected PlayerMovement _playerMovement;
        protected  Animator _animator;
        protected  float _speed;

        protected PlayerWalkingState(PlayerMovement playerMovement, Animator animator, float speed)
        {
            _playerMovement = playerMovement;
            _animator = animator;
            _speed = speed;
        }

        public virtual void Tick() { }

        public virtual void OnEnter() {}

        public virtual void OnExit() {}
    }
}