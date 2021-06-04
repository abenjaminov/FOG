using Animations;
using Character;
using Character.Enemies;
using Entity.Enemies;
using UnityEngine;

namespace State.States.EnemyStates
{
    public class EnemyWalkState : WalkState
    {
        private EnemyMovement _movement;
        private Vector2 _rightBounds;
        private Vector2 _leftBounds;
        
        public EnemyWalkState(Enemy enemy,
            EnemyMovement characterMovement, 
            Animator animator, 
            float speed) : 
            base(enemy, characterMovement, animator, speed)
        {
            _movement = characterMovement;
        }

        public override void Tick()
        {
            
        }

        public override void OnEnter()
        {
            var random = Random.Range(_movement.LeftBounds.x, _movement.RightBounds.x);
            var target = new Vector2(random, _movement.transform.position.y);
            _movement.SetTarget(target);
         
            _animator.SetBool(CachedAnimatorPropertyNames.IsWalking, true);
            base.OnEnter();
        }
        
        public override void OnExit()
        {
            _animator.SetBool(CachedAnimatorPropertyNames.IsWalking, false);
        }
    }
}