using System;
using Player.States;
using State;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private StateMachine _stateMachine;
        [SerializeField] private float _walkingSpeed;
        [SerializeField] private float _jumpingHeight;

        private IState _defaultState;
        private int _horizontalAxisRaw;
        private bool _isLeftAltDown;

        void Awake()
        {
            _stateMachine = new StateMachine();
            
            var playerMovement = GetComponent<PlayerMovement>();
            var animator = GetComponent<Animator>();
            var playerGroundCheck = GetComponentInChildren<GroundCheck>();
            var collider = GetComponent<Collider2D>();
            var rigidBody = GetComponent<Rigidbody2D>();
            
            var idle = new PlayerIdleState(playerMovement,animator);
            var walkLeft = new PlayerWalkLeftState(playerMovement, animator, _walkingSpeed);
            var walkRight = new PlayerWalkRightState(playerMovement, animator, _walkingSpeed);
            var jump = new PlayerJumpingState(collider, animator, playerMovement, _jumpingHeight,rigidBody);
            
            var NoHorizontalInput = new Func<bool>(() => _horizontalAxisRaw == 0);
            var WalkLeft = new Func<bool>(() => _horizontalAxisRaw < 0 && rigidBody.velocity.y == 0);
            var WalkRight = new Func<bool>(() => _horizontalAxisRaw > 0 && rigidBody.velocity.y == 0);
            var Jump = new Func<bool>(() =>  _isLeftAltDown && playerGroundCheck.IsOnGround && rigidBody.velocity.y == 0);
            var WalkLeftAfterLand = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw < 0 && rigidBody.velocity.y < 0);
            var WalkRightAfterLand = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw > 0 && rigidBody.velocity.y < 0);
            var IdleAfterJump = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw == 0 && rigidBody.velocity.y < 0);
            
            _stateMachine.AddTransition(idle, NoHorizontalInput, walkLeft);
            _stateMachine.AddTransition(idle, NoHorizontalInput, walkRight);
            _stateMachine.AddTransition(idle, IdleAfterJump, jump);
            
            _stateMachine.AddTransition(walkLeft, WalkLeft, idle);
            _stateMachine.AddTransition(walkLeft, WalkLeft, walkRight);
            _stateMachine.AddTransition(walkLeft, WalkLeftAfterLand, jump);
            
            _stateMachine.AddTransition(walkRight, WalkRight, idle);
            _stateMachine.AddTransition(walkRight, WalkRight, walkLeft);
            _stateMachine.AddTransition(walkRight, WalkRightAfterLand, jump);
            
            _stateMachine.AddTransition(jump,Jump, walkLeft);
            _stateMachine.AddTransition(jump,Jump, walkRight);
            _stateMachine.AddTransition(jump,Jump, idle);

            _defaultState = idle;
        }

        private void Start()
        {
            _stateMachine.SetState(_defaultState);
        }

        private void Update()
        {
            _horizontalAxisRaw = (int) Input.GetAxisRaw("Horizontal");
            _isLeftAltDown = Input.GetKeyDown(KeyCode.LeftAlt);
            
            _stateMachine.Tick();
        }
    }
}