using Animations;
using Entity.Enemies;
using ScriptableObjects.Channels;
using UnityEngine;

namespace State.States.EnemyStates
{
    public class EnemyDieState : DieState
    {
        private CombatChannel _combatChannel;
        private Enemy _enemy;
        protected Animator _animator;
        
        public EnemyDieState(EnemyMovement movement,Animator animator, CombatChannel combatChannel, Enemy enemy) 
            : base(enemy, movement, animator)
        {
            _animator = animator;
            _combatChannel = combatChannel;
            _enemy = enemy;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _combatChannel.OnEnemyDied(_enemy);
            _animator.SetTrigger(CachedAnimatorPropertyNames.Dead);
        }
    }
}