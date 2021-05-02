using Character;
using Player;
using UnityEngine;

namespace State.States
{
    public class IdleState : IState
    {
        private ICharacterMovement _characterMovement;
        public float IdleTime;
        
        public IdleState(ICharacterMovement characterMovement)
        {
            _characterMovement = characterMovement;
        }

        public void Tick()
        {
            IdleTime += Time.deltaTime;
        }

        public void OnEnter()
        {
            IdleTime = 0;
            _characterMovement.SetVelocity(Vector2.zero);
        }

        public void OnExit()
        {

        }
    }
}