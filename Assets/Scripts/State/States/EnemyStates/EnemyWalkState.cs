using Character;
using Character.Enemies;
using UnityEngine;

namespace State.States.EnemyStates
{
    public class EnemyWalkState : WalkState
    {
        private EnemyMovement _movement;
        private Vector2 _rightBounds;
        private Vector2 _leftBounds;
        
        public EnemyWalkState(Assets.HeroEditor.Common.CharacterScripts.Character character,
            EnemyMovement characterMovement, 
            Animator animator, 
            float speed) : 
            base(character, characterMovement, animator, speed)
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
            
            base.OnEnter();
        }
    }
}