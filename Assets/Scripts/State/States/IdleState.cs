using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Entity;
using Player;
using UnityEngine;

namespace State.States
{
    public class IdleState : IState
    {
        private WorldEntity _worldEntity;
        private ICharacterMovement _characterMovement;
        public float IdleTime;
        
        public IdleState(WorldEntity worldEntity, ICharacterMovement characterMovement)
        {
            _characterMovement = characterMovement;
            _worldEntity = worldEntity;
        }

        public void Tick()
        {
            IdleTime += Time.deltaTime;
        }

        public virtual void OnEnter()
        {
            IdleTime = 0;
            _characterMovement.SetVelocity(Vector2.zero);
        }

        public void OnExit()
        {

        }
    }
}