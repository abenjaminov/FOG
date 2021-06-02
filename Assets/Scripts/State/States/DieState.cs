using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using UnityEngine;

namespace State.States
{
    public class DieState : IState
    {
        public float TimeDead = 0;
        private Assets.HeroEditor.Common.CharacterScripts.Character _character;
        private ICharacterMovement _characterMovement;
        private Animator _animator;

        protected DieState(Assets.HeroEditor.Common.CharacterScripts.Character character, ICharacterMovement characterMovement, Animator animator)
        {
            _character = character;
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
            _character.SetState(CharacterState.DeathB);
        }

        public virtual void OnExit()
        {
            TimeDead = 0;
        }
    }
}