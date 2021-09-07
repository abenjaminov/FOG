using System;
using System.Collections.Generic;
using Abilities;
using Assets.HeroEditor.Common.CharacterScripts;
using Entity;
using Entity.Player;
using Entity.Player.Bow;
using Entity.Player.Magic;
using Entity.Player.Melee;
using Game;
using ScriptableObjects.Channels;
using State;
using State.States;
using State.States.PlayerStates;
using UnityEngine;

namespace Player
{
    public class PlayerStates : MonoBehaviour
    {
        private bool _isBasicAttackKeyDown;

        protected StateMachine _stateMachine;
        
        [SerializeField] private InputChannel _inputChannel;

        [Header("Weapon States")]
        [SerializeField] private BowStates _bowStates;
        [SerializeField] private OneHandedMeleeStates _oneHandedMeleeStates;
        [SerializeField] private MagicStates _magicStates;
        private WeaponStates _activeWeaponStates;
        
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
        protected Func<WeaponStates, Action> _attackTransitionLogicWrapper;
        protected Action<WeaponStates> _attackTransitionLogic;
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

            _attackTransitionLogic = (weaponStates) =>
            {
                _timeUntillNextAttack = _player.Traits.DelayBetweenAttacks;
                weaponStates.IsAbilityAnimationActivated = true;
            };

            _attackTransitionLogicWrapper = new Func<WeaponStates, Action>(weaponStates =>
            {
                return () =>
                {
                    _attackTransitionLogic(weaponStates);
                };
            });
            
            _stateMachine = new StateMachine(false);
            
            _bowStates.Initialize();
            _bowStates.StatesActivatedEvent += StatesActivatedEvent;
            _oneHandedMeleeStates.Initialize();
            _oneHandedMeleeStates.StatesActivatedEvent += StatesActivatedEvent;
            _magicStates.Initialize();
            _magicStates.StatesActivatedEvent += StatesActivatedEvent;
        }

        private void StatesActivatedEvent(WeaponStates activeStates)
        {
            _activeWeaponStates = activeStates;
        }

        protected virtual void Start()
        {
            CreateStates();

            _noHorizontalInput = () => _horizontalAxisRaw == 0 && !_activeWeaponStates.IsAbilityAnimationActivated;
            _walkLeftTransitionCondition = () => _horizontalAxisRaw < 0 && _rigidBody.velocity.y == 0 && !_activeWeaponStates.IsAbilityAnimationActivated;
            _walkRightTransitionCondition = () => _horizontalAxisRaw > 0 && _rigidBody.velocity.y == 0 && !_activeWeaponStates.IsAbilityAnimationActivated;
            var shouldJump = new Func<bool>(() =>  _isJumpButtonDown && _playerGroundCheck.IsOnGround && _rigidBody.velocity.y == 0);
            var shouldFall = new Func<bool>(() =>  !_playerGroundCheck.IsOnGround && _rigidBody.velocity.y == 0);
            var walkLeftAfterLand = new Func<bool>(() => _playerGroundCheck.IsOnGround && _horizontalAxisRaw < 0 && _rigidBody.velocity.y < 0);
            var walkRightAfterLand = new Func<bool>(() => _playerGroundCheck.IsOnGround && _horizontalAxisRaw > 0 && _rigidBody.velocity.y < 0);
            var idleAfterJump = new Func<bool>(() => _playerGroundCheck.IsOnGround && _horizontalAxisRaw == 0 && _rigidBody.velocity.y < 0);
            var shouldClimb = new Func<bool>(() => ((_isClimbDownButtonDown && _playerClimb.CanClimbDown) || (_isClimUpButtonDown && _playerClimb.CanClimbUp)));

            var idleAfterClimb = new Func<bool>(() => (_playerClimb.IsOnEdge) && 
                                                      ((_playerClimb.CurrentEdge.Type == EdgeType.Upper && _isClimUpButtonDown) || 
                                                       (_playerClimb.CurrentEdge.Type == EdgeType.Lower && _isClimbDownButtonDown)));

            _shouldAbility = () => _activeWeaponStates != null && !_activeWeaponStates.IsAbilityAnimationActivated && _timeUntillNextAttack <= 0;
           
            
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

            _bowStates.HookStates();
            _oneHandedMeleeStates.HookStates();
            _magicStates.HookStates();
            
            AddAbilityState(_bowStates.BasicAttackState, _attackTransitionLogicWrapper(_bowStates), null,() => _bowStates.IsEnabled);
            AddAbilityState(_oneHandedMeleeStates.BasicAttackState, _attackTransitionLogicWrapper(_oneHandedMeleeStates), null,() => _oneHandedMeleeStates.IsEnabled);
            AddAbilityState(_magicStates.BasicAttackState, _attackTransitionLogicWrapper(_magicStates), null,() => _magicStates.IsEnabled);

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

        private void CreateStates()
        {
            _idle = new PlayerIdleState(_player, _playerMovement);
            _walkLeft = new PlayerWalkLeftState(_player, _playerMovement, _animator, _player.Traits.WalkSpeed);
            _walkRight = new PlayerWalkRightState(_player, _playerMovement, _animator, _player.Traits.WalkSpeed);
            _jump = new PlayerJumpingState(_player, _collider2D, _playerMovement, _player.Traits.JumpHeight, _rigidBody);
            _fall = new PlayerFallState(_player, _collider2D);
            _dead = new PlayerDieState(_player, _playerMovement, _animator);
            _climb = new PlayerClimbState(_player, _playerClimb, _playerMovement, _rigidBody, _collider2D, _inputChannel,
                _player.PlayerTraits.ClimbSpeed);
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

        internal void AddAttackState(IAbilityState abilityState, WeaponStates weaponStates, Func<bool> shouldTransitionFrom = null)
        {
            AddAbilityState(abilityState, _attackTransitionLogicWrapper(weaponStates), shouldTransitionFrom);
        }
        
        internal void AddBuffState(IAbilityState abilityState, Func<bool> shouldTransitionFrom = null)
        {
            AddAbilityState(abilityState, _buffTransitionLogic, shouldTransitionFrom);
        }
        
        private void AddAbilityState(IAbilityState abilityState,
                                      Action transitionLogic = null,
                                      Func<bool> shouldTransitionFrom = null,
                                      Func<bool> shouldTransitionTo = null)
        {
            var shouldTransitionFromInternal =
                new Func<bool>(() => shouldTransitionFrom?.Invoke() ?? true);
            
            _stateMachine.AddTransition(_idle, () => _noHorizontalInput() && shouldTransitionFromInternal(), abilityState,null, "Ability -> Idle");
            _stateMachine.AddTransition(_walkLeft, () => _walkLeftTransitionCondition() && shouldTransitionFromInternal(), abilityState, null,"Ability -> Walk Left");
            _stateMachine.AddTransition(_walkRight, () => _walkRightTransitionCondition() && shouldTransitionFromInternal(), abilityState, null,"Ability -> Walk Right");

            var shouldTransitionToInternal = new Func<bool>(() => _shouldAbility() && abilityState.IsHotKeyDown() && (shouldTransitionTo?.Invoke() ?? true));
            
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
            _bowStates.StatesActivatedEvent -= StatesActivatedEvent;
            _oneHandedMeleeStates.StatesActivatedEvent -= StatesActivatedEvent;
            _magicStates.StatesActivatedEvent -= StatesActivatedEvent;
        }
    }
}