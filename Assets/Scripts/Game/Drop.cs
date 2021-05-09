﻿using System;
using Player;
using State;
using State.States.DropStates;
using UnityEngine;

namespace Game
{
    public class Drop : MonoBehaviour
    {
        [SerializeField] private float _lifeSpan;
        [SerializeField] private float _dropHeight;

        private GroundCheck _groundCheck;
        private FloatUpDown _floatComponent;
        private FadeoutToPoint _fadeoutComponent;
        private SpriteRenderer _renderer;
        private float _timeAlive;
        private Collider2D _collider;

        private bool _isPickedUp;

        private StateMachine _stateMachine;
        private PickedUpState _pickedUpState;
        private DroppedState _droppedState;
        private FloatState _floatState;

        private void Awake()
        {
            
            _renderer = GetComponent<SpriteRenderer>();
            _groundCheck = GetComponentInChildren<GroundCheck>();
            _collider = GetComponent<Collider2D>();
            _fadeoutComponent = GetComponent<FadeoutToPoint>();
            _fadeoutComponent.enabled = false;
            _floatComponent = GetComponent<FloatUpDown>();
            _floatComponent.enabled = false;
            
            _stateMachine = new StateMachine();

            _droppedState = new DroppedState(_floatComponent, transform, _dropHeight, _fadeoutComponent);
            _floatState = new FloatState(_floatComponent, _groundCheck, _collider, transform);
            _pickedUpState = new PickedUpState(_floatComponent, _fadeoutComponent, transform);

            var shouldFloat = new Func<bool>(() => _groundCheck.IsOnGround);
            var shouldBePickedUp = new Func<bool>(() => _isPickedUp);
            
            _stateMachine.AddTransition(_floatState,shouldFloat, _droppedState);
            _stateMachine.AddTransition(_pickedUpState,shouldBePickedUp, _floatState);
            
            _stateMachine.SetState(_droppedState);
        }

        public void SetSprite(Sprite sprite)
        {
            _renderer.sprite = sprite;
        }

        private void Update()
        {
            _timeAlive += Time.deltaTime;

            if (_timeAlive >= _lifeSpan)
            {
                Destroy(gameObject);
            }

            _stateMachine.Tick();
        }

        public void OnPickedUp(Transform playerTransform)
        {
            _pickedUpState.SetPlayerTransform(playerTransform);
            _isPickedUp = true;
        }
    }
}