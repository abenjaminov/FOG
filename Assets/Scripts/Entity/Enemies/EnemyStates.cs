using System;
using ScriptableObjects.Channels;
using State;
using State.States;
using State.States.EnemyStates;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity.Enemies
{
    public class EnemyStates : MonoBehaviour
    {
        private StateMachine _stateMachine;

        [SerializeField] private CombatChannel _combatChannel;
        [SerializeField] private float _walkingSpeed;
        [SerializeField] private float _maxIdleTimeBetweenTargets;
        [SerializeField] private float _minIdleTimeBetweenTargets;
        private float _idleTimeBetweenTargets;
        [SerializeField] private float _deadDelayTime;

        [SerializeField] private bool isAlwaysIdle;
        
        private IState _defaultState;
        private Enemy _enemy;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            
            var enemyMovement = GetComponentInParent<EnemyMovement>();
            var animator = GetComponent<Animator>();
            _enemy = GetComponent<Enemy>();
            
            var idle = new IdleState(_enemy, enemyMovement);
            var walk = new EnemyWalkState(_enemy, enemyMovement, animator, _walkingSpeed);
            var dead = new EnemyDieState(enemyMovement,animator,_combatChannel, _enemy);
            var vanishState = new EmptyState();

            var shouldStand = new Func<bool>(() => !_enemy.IsDead && enemyMovement.Target != Vector2.positiveInfinity &&
                                                   (Vector2.Distance(enemyMovement.Target, enemyMovement.transform.position) <= .2f ||
                                                   enemyMovement.transform.position.x <= enemyMovement.LeftBounds.x ||
                                                   enemyMovement.transform.position.x >= enemyMovement.RightBounds.x));
            var shouldWalk = new Func<bool>(() => !_enemy.IsDead && idle.IdleTime >= _idleTimeBetweenTargets && !isAlwaysIdle);
            var shouldDie = new Func<bool>(() => _enemy.IsDead && !isAlwaysIdle);
            var shouldVanish = new Func<bool>(() => _enemy.IsDead && dead.TimeDead >= _deadDelayTime && !isAlwaysIdle);
            
            _stateMachine.AddTransition(idle, shouldStand, walk, () =>
            {
                _idleTimeBetweenTargets = Random.Range(_minIdleTimeBetweenTargets, _maxIdleTimeBetweenTargets);
            });
            _stateMachine.AddTransition(walk, shouldWalk, idle);
            
            _stateMachine.AddTransition(dead, shouldDie, idle);
            _stateMachine.AddTransition(dead, shouldDie, walk);
            
            _stateMachine.AddTransition(vanishState,shouldVanish,null, () =>
            {
                enemyMovement.gameObject.SetActive(false);
            });

            _defaultState = idle;
        }

        private void OnEnable()
        {
            _stateMachine.SetState(_defaultState);
        }

        private void Start()
        {
            _stateMachine.SetState(_defaultState);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}