using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Entity;
using UnityEngine;

namespace State.States
{
    public class DieState : IState
    {
        public float TimeDead = 0;
        private WorldEntity _worldEntity;
        protected ICharacterMovement _characterMovement;

        protected DieState(WorldEntity worldEntity, ICharacterMovement characterMovement, Animator animator)
        {
            _worldEntity = worldEntity;
            _characterMovement = characterMovement;
        }

        public virtual void Tick()
        {
            TimeDead += Time.deltaTime;
        }

        public virtual void OnEnter()
        {
            TimeDead = 0;
            _characterMovement.SetVelocity(Vector2.zero);
        }

        public virtual void OnExit()
        {
            TimeDead = 0;
        }
    }
}