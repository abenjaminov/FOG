using Character;
using Character.Enemies;
using UnityEngine;

namespace State.States.EnemyStates
{
    public class EnemyWalkState : WalkState
    {
        private EnemyMovement _movement;
        
        public EnemyWalkState(ICharacterMovement characterMovement, Animator animator, float speed) : 
            base(characterMovement, animator, speed)
        {
            _movement = (EnemyMovement) characterMovement;
        }

        public override void Tick()
        {
            
        }

        public override void OnEnter()
        {
            var random = Random.Range(-3, 3);
            var target = new Vector2(_movement.transform.position.x + random, _movement.transform.position.y);
            _movement.SetTarget(target);
            
            base.OnEnter();
        }
    }
}