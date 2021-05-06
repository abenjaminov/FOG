using System;
using State;
using State.States;
using State.States.PlayerStates;
using UnityEngine;

namespace Player
{
    public class PlayerStates : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        [SerializeField] private float _walkingSpeed;
        [SerializeField] private float _jumpingHeight;

        private IState _defaultState;
        protected int _horizontalAxisRaw;
        private bool _isLeftAltDown;

        protected Animator _animator;
        protected Rigidbody2D _rigidBody;

        protected IdleState _idle;
        protected WalkLeftState _walkLeft;
        protected WalkRightState _walkRight;
        protected PlayerJumpingState _jump;
        protected PlayerFallState _fall;
        protected DieState _dead;

        protected virtual void Awake()
        {
            _stateMachine = new StateMachine(false);
            
            var playerMovement = GetComponent<PlayerMovement>();
            _rigidBody = GetComponent<Rigidbody2D>();
            var playerGroundCheck = GetComponentInChildren<GroundCheck>();
            var collider2D = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();
            var _player = GetComponent<Character.Player.Player>();
            
            _idle = new IdleState(playerMovement);
            _walkLeft = new WalkLeftState(playerMovement, _animator, _walkingSpeed);
            _walkRight = new WalkRightState(playerMovement, _animator, _walkingSpeed);
            _jump = new PlayerJumpingState(collider2D, _animator, playerMovement, _jumpingHeight,_rigidBody);
            _fall = new PlayerFallState(collider2D, _animator, _rigidBody);
            _dead = new PlayerDieState(playerMovement, _animator);

            var noHorizontalInput = new Func<bool>(() => _horizontalAxisRaw == 0);
            var walkLeft = new Func<bool>(() => _horizontalAxisRaw < 0 && _rigidBody.velocity.y == 0);
            var walkRight = new Func<bool>(() => _horizontalAxisRaw > 0 && _rigidBody.velocity.y == 0);
            var shouldJump = new Func<bool>(() =>  _isLeftAltDown && playerGroundCheck.IsOnGround && _rigidBody.velocity.y == 0);
            var shouldFall = new Func<bool>(() =>  !playerGroundCheck.IsOnGround && _rigidBody.velocity.y == 0);
            var walkLeftAfterLand = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw < 0 && _rigidBody.velocity.y < 0);
            var walkRightAfterLand = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw > 0 && _rigidBody.velocity.y < 0);
            var idleAfterJump = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw == 0 && _rigidBody.velocity.y < 0);
            var shouldDie = new Func<bool>(() => _player.IsDead);

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

            _defaultState = _idle;
        }

        private void Start()
        {
            _stateMachine.SetState(_defaultState);
        }

        protected virtual void Update()
        {
            _horizontalAxisRaw = (int) Input.GetAxisRaw("Horizontal");
            _isLeftAltDown = Input.GetKeyDown(KeyCode.LeftAlt);
            
            _stateMachine.Tick();
        }
    }
}