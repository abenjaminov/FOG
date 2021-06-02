using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Player;
using UnityEngine;

namespace State.States
{
    public class WalkState : IState
    {
        protected Assets.HeroEditor.Common.CharacterScripts.Character _character;
        protected ICharacterMovement _characterMovement;
        protected  Animator _animator;
        protected  float _speed;

        private CharacterState _previousState;
        
        public WalkState(Assets.HeroEditor.Common.CharacterScripts.Character character,
            ICharacterMovement characterMovement, Animator animator, float speed)
        {
            _characterMovement = characterMovement;
            _animator = animator;
            _speed = speed;
            _character = character;
        }

        public virtual void Tick()
        {
            _characterMovement.SetHorizontalVelocity(_speed);
        }

        public virtual void OnEnter()
        {
            _characterMovement.SetHorizontalVelocity(_speed);
            //_animator.SetBool(CachedAnimatorPropertyNames.IsWalking, true);
            _previousState = _character.GetState();
            _character.SetState(CharacterState.Walk);
        }

        public virtual void OnExit()
        {
            //_animator.SetBool(CachedAnimatorPropertyNames.IsWalking, false);
            _character.SetState(_previousState);
        }
    }
}