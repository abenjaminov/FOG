using System;
using System.Collections.Generic;
using Abilities;
using Assets.HeroEditor.Common.CharacterScripts;
using Entity;
using Entity.Player;
using Game;
using ScriptableObjects.Channels;
using State;
using State.States;
using State.States.PlayerStates;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerStates : MonoBehaviour
    {
        private bool _isBasicAttackKeyDown;
        protected bool _isAbilityAnimationActivated = false;
        
        protected StateMachine _stateMachine;
        
        [SerializeField] private InputChannel _inputChannel;

        [SerializeField] protected Ability _basicAttackAbility;
        
        protected AnimationEvents _animationEvents;
        
        private IState _defaultState;
        protected int _horizontalAxisRaw;
        private bool _isJumpButtonDown;
        private bool _isClimUpButtonDown;
        private bool _isClimbDownButtonDown;

        protected Animator _animator;
        protected Rigidbody2D _rigidBody;
        protected Entity.Player.Player _player;
        protected PlayerMovement _playerMovement;
        protected PlayerClimb _playerClimb;
        protected GroundCheck _playerGroundCheck;
        protected Collider2D _collider2D;
        
        // States
        protected IdleState _idle;
        protected PlayerWalkLeftState _walkLeft;
        protected PlayerWalkRightState _walkRight;
        protected PlayerJumpingState _jump;
        protected PlayerFallState _fall;
        protected DieState _dead;
        protected IAbilityState _basicAttackState;
        protected PlayerClimbState _climb;

        // Transition conditions
        protected Func<bool> _shouldAbility;
        protected Func<bool> _walkLeftTransitionCondition;
        protected Func<bool> _walkRightTransitionCondition;
        protected Func<bool> _noHorizontalInput;
        
        // Transition Logic
        protected Action _attackTransitionLogic;
        protected Action _buffTransitionLogic;
        
        protected float _timeUntillNextAttack = 0;

        private List<KeySubscription> _allKeySubscription = new List<KeySubscription>();

        protected virtual void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _playerGroundCheck = GetComponentInChildren<GroundCheck>();
            _collider2D = GetComponent<Collider2D>();
            _animator = GetComponentInChildren<Animator>();
            _player = GetComponent<Entity.Player.Player>();
            _playerClimb = GetComponentInChildren<PlayerClimb>();
        }
        
        protected virtual void Start()
        {
            _stateMachine = new StateMachine(false);
            
            _animationEvents = GetComponentInChildren<AnimationEvents>();

            _idle = new PlayerIdleState(_player, _playerMovement);
            _walkLeft = new PlayerWalkLeftState(_player, _playerMovement, _animator, _player.Traits.WalkSpeed);
            _walkRight = new PlayerWalkRightState(_player, _playerMovement, _animator, _player.Traits.WalkSpeed);
            _jump = new PlayerJumpingState(_player, _collider2D, _playerMovement, _player.Traits.JumpHeight,_rigidBody);
            _fall = new PlayerFallState(_player,_collider2D);
            _dead = new PlayerDieState(_player, _playerMovement, _animator);
            _climb = new PlayerClimbState(_player, _playerClimb,_playerMovement,_rigidBody,_collider2D, _inputChannel, _player.PlayerTraits.ClimbSpeed);
            
            _noHorizontalInput = () => _horizontalAxisRaw == 0 && !_isAbilityAnimationActivated;
            _walkLeftTransitionCondition = () => _horizontalAxisRaw < 0 && _rigidBody.velocity.y == 0 && !_isAbilityAnimationActivated;
            _walkRightTransitionCondition = () => _horizontalAxisRaw > 0 && _rigidBody.velocity.y == 0 && !_isAbilityAnimationActivated;
            var shouldJump = new Func<bool>(() =>  _isJumpButtonDown && _playerGroundCheck.IsOnGround && _rigidBody.velocity.y == 0);
            var shouldFall = new Func<bool>(() =>  !_playerGroundCheck.IsOnGround && _rigidBody.velocity.y == 0);
            var walkLeftAfterLand = new Func<bool>(() => _playerGroundCheck.IsOnGround && _horizontalAxisRaw < 0 && _rigidBody.velocity.y < 0);
            var walkRightAfterLand = new Func<bool>(() => _playerGroundCheck.IsOnGround && _horizontalAxisRaw > 0 && _rigidBody.velocity.y < 0);
            var idleAfterJump = new Func<bool>(() => _playerGroundCheck.IsOnGround && _horizontalAxisRaw == 0 && _rigidBody.velocity.y < 0);
            var shouldClimb = new Func<bool>(() => ((_isClimbDownButtonDown && _playerClimb.CanClimbDown) || (_isClimUpButtonDown && _playerClimb.CanClimbUp)));

            var idleAfterClimb = new Func<bool>(() => (_playerClimb.IsOnEdge) && 
                                                      ((_playerClimb.CurrentEdge.Type == EdgeType.Upper && _isClimUpButtonDown) || 
                                                       (_playerClimb.CurrentEdge.Type == EdgeType.Lower && _isClimbDownButtonDown)));

            _shouldAbility = () => !_isAbilityAnimationActivated && _timeUntillNextAttack <= 0;
            _attackTransitionLogic = () =>
            {
                _timeUntillNextAttack = _player.Traits.DelayBetweenAttacks;
                _isAbilityAnimationActivated = true;
            };
            
            _buffTransitionLogic = () =>
            {
                _timeUntillNextAttack = _player.Traits.DelayBetweenAttacks;
            };
            
            _stateMachine.AddTransition(_idle, _noHorizontalInput, _walkLeft);
            _stateMachine.AddTransition(_idle, _noHorizontalInput, _walkRight);
            _stateMachine.AddTransition(_idle, idleAfterJump, _jump);
            _stateMachine.AddTransition(_idle, idleAfterJump, _fall);
            _stateMachine.AddTransition(_idle, idleAfterClimb, _climb);

            _stateMachine.AddTransition(_walkLeft, _walkLeftTransitionCondition, _idle);
            _stateMachine.AddTransition(_walkLeft, _walkLeftTransitionCondition, _walkRight);
            _stateMachine.AddTransition(_walkLeft, walkLeftAfterLand, _jump);
            _stateMachine.AddTransition(_walkLeft, walkLeftAfterLand, _fall);
            
            _stateMachine.AddTransition(_walkRight, _walkRightTransitionCondition, _idle);
            _stateMachine.AddTransition(_walkRight, _walkRightTransitionCondition, _walkLeft);
            _stateMachine.AddTransition(_walkRight, walkRightAfterLand, _jump);
            _stateMachine.AddTransition(_walkRight, walkRightAfterLand, _fall);
            
            _stateMachine.AddTransition(_jump,shouldJump, _walkLeft);
            _stateMachine.AddTransition(_jump,shouldJump, _walkRight);
            _stateMachine.AddTransition(_jump,shouldJump, _idle);
            
            _stateMachine.AddTransition(_fall, shouldFall,_walkLeft);
            _stateMachine.AddTransition(_fall, shouldFall,_walkRight);
            
            _stateMachine.AddTransition(_climb, shouldClimb, _idle);
            _stateMachine.AddTransition(_climb, shouldClimb, _walkLeft);
            _stateMachine.AddTransition(_climb, shouldClimb, _walkRight);
            _stateMachine.AddTransition(_climb, shouldClimb, _jump);
            
            ConfigureDeadState();

            AddAbilityState(_basicAttackState, _shouldAbility, _attackTransitionLogic);

            _defaultState = _idle;
            
            RegisterBooleanToKey(KeyCode.LeftAlt,(isKeyDown) => { _isJumpButtonDown = isKeyDown; });
            RegisterBooleanToKey(KeyCode.UpArrow,(isKeyDown) => { _isClimUpButtonDown = isKeyDown; });
            RegisterBooleanToKey(KeyCode.DownArrow,(isKeyDown) => { _isClimbDownButtonDown = isKeyDown; });

            var newSubs = new List<KeySubscription>()
            {
                _inputChannel.SubscribeKeyDown(KeyCode.RightArrow, () => _horizontalAxisRaw = 1),
                _inputChannel.SubscribeKeyDown(KeyCode.LeftArrow, () => _horizontalAxisRaw = -1),
                _inputChannel.SubscribeKeyUp(KeyCode.RightArrow, () =>
                {
                    if (_horizontalAxisRaw == 1)
                        _horizontalAxisRaw = 0;
                }),
                _inputChannel.SubscribeKeyUp(KeyCode.LeftArrow, () =>
                {
                    if (_horizontalAxisRaw == -1)
                        _horizontalAxisRaw = 0;
                })
            };
            
            _allKeySubscription.AddRange(newSubs);
            
            _stateMachine.SetState(_defaultState);
        }

        private void ConfigureDeadState()
        {
            var shouldDie = new Func<bool>(() => _player.IsDead);
            _stateMachine.AddTransition(_dead, shouldDie, _fall);
            _stateMachine.AddTransition(_dead, shouldDie, _jump);
            _stateMachine.AddTransition(_dead, shouldDie, _walkRight);
            _stateMachine.AddTransition(_dead, shouldDie, _walkLeft);
            _stateMachine.AddTransition(_dead, shouldDie, _idle);
        }

        protected virtual void Update()
        {
            if (_timeUntillNextAttack > 0)
            {
                _timeUntillNextAttack -= Time.deltaTime;
            }

            _stateMachine.Tick();
        }
        
        protected void AddAbilityState(IAbilityState abilityState, 
                                       Func<bool> shouldTransitionTo, 
                                       Action transitionLogic = null,
                                       Func<bool> shouldTransitionFrom = null)
        {
            var shouldTransitionFromInternal =
                new Func<bool>(() => shouldTransitionFrom?.Invoke() ?? true);
            
            _stateMachine.AddTransition(_idle, () => _noHorizontalInput() && shouldTransitionFromInternal(), abilityState,null, "Shoot Arrow -> Idle");
            _stateMachine.AddTransition(_walkLeft, () => _walkLeftTransitionCondition() && shouldTransitionFromInternal(), abilityState, null,"Shoot Arrow -> Walk Left");
            _stateMachine.AddTransition(_walkRight, () => _walkRightTransitionCondition() && shouldTransitionFromInternal(), abilityState, null,"Shoot Arrow -> Walk Right");

            var shouldTransitionToInternal = new Func<bool>(() => shouldTransitionTo() && abilityState.IsHotKeyDown());
            
            _stateMachine.AddTransition(abilityState, shouldTransitionToInternal, _idle, transitionLogic,
                "Idle -> Transition To Attack");
            _stateMachine.AddTransition(abilityState, shouldTransitionToInternal, _walkLeft, transitionLogic,
                "Walk Left -> Transition To Attack");
            _stateMachine.AddTransition(abilityState, shouldTransitionToInternal, _walkRight, transitionLogic,
                "Walk Right -> Transition To Attack");
            _stateMachine.AddTransition(abilityState, shouldTransitionToInternal, abilityState, transitionLogic,
                "Walk Right -> Transition To Attack");
            
            RegisterBooleanToKey(abilityState.GetHotKey(), abilityState.SetHotKeyDown);
        }

        private void RegisterBooleanToKey(KeyCode keyCode, Action<bool> setValue)
        {
            var keyDownSub = _inputChannel.SubscribeKeyDown(keyCode, () => setValue(true));
            _allKeySubscription.Add(keyDownSub);
            
            var keyUpSub =_inputChannel.SubscribeKeyUp(keyCode, () => setValue(false));
            _allKeySubscription.Add(keyUpSub);
        }

        private void OnDestroy()
        {
            foreach (var keySubscription in _allKeySubscription)
            {
                keySubscription.Unsubscribe();
            }
            
            _allKeySubscription.Clear();
            _stateMachine.Close();
        }
    }
}