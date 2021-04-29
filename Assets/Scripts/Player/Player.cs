﻿using System;
using Player.States;
using State;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        [SerializeField] private float _walkingSpeed;
        [SerializeField] private float _jumpingHeight;

        private IState _defaultState;
        protected int _horizontalAxisRaw;
        private bool _isLeftAltDown;

        protected Animator _animator;

        protected PlayerIdleState _idle;
        protected PlayerWalkLeftState _walkLeft;
        protected PlayerWalkRightState _walkRight;
        protected PlayerJumpingState _jump;

        protected virtual void Awake()
        {
            _stateMachine = new StateMachine();


            var playerMovement = GetComponent<PlayerMovement>();
            var rigidBody = GetComponent<Rigidbody2D>();
            var playerGroundCheck = GetComponent<GroundCheck>();
            var collider2D = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();
            
            _idle = new PlayerIdleState(playerMovement,_animator);
            _walkLeft = new PlayerWalkLeftState(playerMovement, _animator, _walkingSpeed);
            _walkRight = new PlayerWalkRightState(playerMovement, _animator, _walkingSpeed);
            _jump = new PlayerJumpingState(collider2D, _animator, playerMovement, _jumpingHeight,rigidBody);

            var noHorizontalInput = new Func<bool>(() => _horizontalAxisRaw == 0);
            var walkLeft = new Func<bool>(() => _horizontalAxisRaw < 0 && rigidBody.velocity.y == 0);
            var walkRight = new Func<bool>(() => _horizontalAxisRaw > 0 && rigidBody.velocity.y == 0);
            var shouldJump = new Func<bool>(() =>  _isLeftAltDown && playerGroundCheck.IsOnGround && rigidBody.velocity.y == 0);
            var walkLeftAfterLand = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw < 0 && rigidBody.velocity.y < 0);
            var walkRightAfterLand = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw > 0 && rigidBody.velocity.y < 0);
            var idleAfterJump = new Func<bool>(() => playerGroundCheck.IsOnGround && _horizontalAxisRaw == 0 && rigidBody.velocity.y < 0);

            _stateMachine.AddTransition(_idle, noHorizontalInput, _walkLeft);
            _stateMachine.AddTransition(_idle, noHorizontalInput, _walkRight);
            _stateMachine.AddTransition(_idle, idleAfterJump, _jump);

            _stateMachine.AddTransition(_walkLeft, walkLeft, _idle);
            _stateMachine.AddTransition(_walkLeft, walkLeft, _walkRight);
            _stateMachine.AddTransition(_walkLeft, walkLeftAfterLand, _jump);
            
            _stateMachine.AddTransition(_walkRight, walkRight, _idle);
            _stateMachine.AddTransition(_walkRight, walkRight, _walkLeft);
            _stateMachine.AddTransition(_walkRight, walkRightAfterLand, _jump);
            
            _stateMachine.AddTransition(_jump,shouldJump, _walkLeft);
            _stateMachine.AddTransition(_jump,shouldJump, _walkRight);
            _stateMachine.AddTransition(_jump,shouldJump, _idle);

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