using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Entity;
using Player;
using UnityEngine;

namespace State.States
{
    public class WalkState : IState
    {
        protected WorldEntity _worldEntity;
        protected ICharacterMovement _characterMovement;
        protected Animator _animator;
        protected float _speed;

        
        
        public WalkState(WorldEntity worldEntity,
            ICharacterMovement characterMovement, Animator animator, float speed)
        {
            _characterMovement = characterMovement;
            _animator = animator;
            _speed = speed;
            _worldEntity = worldEntity;
        }

        public virtual void Tick()
        {
            _characterMovement.SetHorizontalVelocity(_speed);
        }

        public virtual void OnEnter()
        {
            _characterMovement.SetHorizontalVelocity(_speed);
        }

        public virtual void OnExit()
        {
            
        }
    }
}