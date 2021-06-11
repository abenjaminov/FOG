using System;
using Entity;
using ScriptableObjects.Channels;
using State;
using State.States;
using State.States.PlayerStates;
using UnityEngine;

namespace Player
{
    public class PlayerStates : MonoBehaviour
    {
        private bool _isLeftControlDown;
        protected bool _isAttackAnimationActivated = false;
        
        protected StateMachine _stateMachine;
        
        [SerializeField] private InputChannel _inputChannel;

        private IState _defaultState;
        protected int _horizontalAxisRaw;
        private bool _isJumpButtonDown;

        protected Animator _animator;
        protected Rigidbody2D _rigidBody;
        protected Entity.Player.Player _player;
        
        // States
        protected IdleState _idle;
        protected PlayerWalkLeftState _walkLeft;
        protected PlayerWalkRightState _walkRight;
        protected PlayerJumpingState _jump;
        protected PlayerFallState _fall;
        protected DieState _dead;
        protected PlayerAttackState _attack;

        // Transition conditions
        protected Func<bool> _shouldAttack;
        
        // Transition Logic
        protected Action _attackTransitionLogic;
        
        protected float _timeUntillNextAttack = 0;
        
        protected virtual void Start()
        {
            _stateMachine = new StateMachine(false);
            
            var playerMovement = GetComponent<PlayerMovement>();
            _rigidBody = GetComponent<Rigidbody2D>();
            var playerGroundCheck = GetComponentInChildren<GroundCheck>();
            var collider2D = GetComponent<Collider2D>();
            _animator = GetComponentInChildren<Animator>();
            _player = GetComponent<Entity.Player.Player>();
            
            _idle = new PlayerIdleState(_player, playerMovement);
            _walkLeft = new PlayerWalkLeftState(_player, playerMovement, _animator, _player.Traits.WalkSpeed);
            _walkRight = new PlayerWalkRightState(_player, playerMovement, _animator, _player.Traits.WalkSpeed);
            _jump = new PlayerJumpingState(_player, collider2D, playerMovement, _player.Traits.JumpHeight,_rigidBody);
            _fall = new PlayerFallState(_player,collider2D) ;
            _dead = new PlayerDieState(_player, playerMovement, _animator);
            
            var noHorizontalInput = new Func<bool>(() => _horizontalAxisRaw == 0 && !_isAttackAnimationActivated);
            var walkLeft = new Func<bool>(() => _horizontalAxisRaw < 0 && _rigidBody.velocity.y == 0 && !_isAttackAnimationActivated);
            var walkRight = new Func<bool>(() => _horizontalAxisRaw > 0 && _rigidBody.velocity.y == 0 && !_isAttackAnimationActivated);
            var shouldJump = new Func<bool>(() =>  _isJumpButtonDown && playerGroundCheck.IsOnGround && _rigidBody.velocity.y == 0);
            var shouldFall = new Func<bool>(() =>  !playerGroundCheck.IsOnGround && _rigidBody.velocity.y == 0);
            var walkLeftAfterLand = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw < 0 && _rigidBody.velocity.y < 0);
            var walkRightAfterLand = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw > 0 && _rigidBody.velocity.y < 0);
            var idleAfterJump = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw == 0 && _rigidBody.velocity.y < 0);
            var shouldDie = new Func<bool>(() => _player.IsDead);

            _shouldAttack = new Func<bool>(() => _isLeftControlDown && !_isAttackAnimationActivated && _timeUntillNextAttack <= 0);
            _attackTransitionLogic = () =>
            {
                _timeUntillNextAttack = _player.Traits.DelayBetweenAttacks;
                _isAttackAnimationActivated = true;
            };
            
            _stateMachine.AddTransition(_idle, noHorizontalInput, _walkLeft);
            _stateMachine.AddTransition(_idle, noHorizontalInput, _walkRight);
            _stateMachine.AddTransition(_idle, idleAfterJump, _jump);
            _stateMachine.AddTransition(_idle, idleAfterJump, _fall);

            _stateMachine.AddTransition(_walkLeft, walkLeft, _idle);
            _stateMachine.AddTransition(_walkLeft, walkLeft, _walkRight);
            _stateMachine.AddTransition(_walkLeft, walkLeftAfterLand, _jump);
            _stateMachine.AddTransition(_walkLeft, walkLeftAfterLand, _fall);
            
            _stateMachine.AddTransition(_walkRight, walkRight, _idle);
            _stateMachine.AddTransition(_walkRight, walkRight, _walkLeft);
            _stateMachine.AddTransition(_walkRight, walkRightAfterLand, _jump);
            _stateMachine.AddTransition(_walkRight, walkRightAfterLand, _fall);
            
            _stateMachine.AddTransition(_jump,shouldJump, _walkLeft);
            _stateMachine.AddTransition(_jump,shouldJump, _walkRight);
            _stateMachine.AddTransition(_jump,shouldJump, _idle);
            
            _stateMachine.AddTransition(_fall, shouldFall,_walkLeft);
            _stateMachine.AddTransition(_fall, shouldFall,_walkRight);
            
            _stateMachine.AddTransition(_dead, shouldDie,_fall);
            _stateMachine.AddTransition(_dead, shouldDie,_jump);
            _stateMachine.AddTransition(_dead, shouldDie,_walkRight);
            _stateMachine.AddTransition(_dead, shouldDie,_walkLeft);
            _stateMachine.AddTransition(_dead, shouldDie,_idle);
            
            _stateMachine.AddTransition(_idle, noHorizontalInput, _attack,null, "Shoot Arrow -> Idle");
            _stateMachine.AddTransition(_walkLeft, walkLeft, _attack, null,"Shoot Arrow -> Walk Left");
            _stateMachine.AddTransition(_walkRight, walkRight, _attack, null,"Shoot Arrow -> Walk Right");
            
            _stateMachine.AddTransition(_attack, _shouldAttack, _idle, _attackTransitionLogic,"Idle -> Transition To Shoot");
            _stateMachine.AddTransition(_attack, _shouldAttack, _walkLeft, _attackTransitionLogic,"Walk Left -> Transition To Shoot");
            _stateMachine.AddTransition(_attack, _shouldAttack, _walkRight, _attackTransitionLogic,"Walk Right -> Transition To Shoot");
            _stateMachine.AddTransition(_attack, _shouldAttack, _attack, _attackTransitionLogic,"Walk Right -> Transition To Shoot");

            _defaultState = _idle;
            
            RegisterBooleanToKey(KeyCode.LeftAlt,(isKeyDown) => { _isJumpButtonDown = isKeyDown; });
            RegisterBooleanToKey(KeyCode.LeftControl,(isKeyDown) => { _isLeftControlDown = isKeyDown; });

            _inputChannel.RegisterKeyDown(KeyCode.RightArrow, () => _horizontalAxisRaw = 1);
            _inputChannel.RegisterKeyUp(KeyCode.RightArrow, () => _horizontalAxisRaw = 0);
            _inputChannel.RegisterKeyDown(KeyCode.LeftArrow, () => _horizontalAxisRaw = -1);
            _inputChannel.RegisterKeyUp(KeyCode.LeftArrow, () => _horizontalAxisRaw = 0);
            

            _stateMachine.SetState(_defaultState);
        }

        private void RegisterBooleanToKey(KeyCode keyCode, Action<bool> setValue)
        {
            _inputChannel.RegisterKeyDown(keyCode, () => setValue(true));
            _inputChannel.RegisterKeyUp(keyCode, () => setValue(false));
        }

        protected virtual void Update()
        {
            if (_timeUntillNextAttack > 0)
            {
                _timeUntillNextAttack -= Time.deltaTime;
            }

            _stateMachine.Tick();
        }
    }
}