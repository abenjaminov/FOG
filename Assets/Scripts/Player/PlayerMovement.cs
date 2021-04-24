using System;
using Animations;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private PlayerChannel _playerChannel;

        private bool _isOnGround;
        
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _collider2D;

        private bool _isMovementActive = true;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<BoxCollider2D>();
            _playerChannel.GroundCheckEvent += GroundCheckEvent;
            _playerChannel.MovementActiveEvent += MovementActiveEvent;
            LookDirection(true);
        }

        private void MovementActiveEvent(bool isActive)
        {
            _isMovementActive = isActive;
        }

        private void GroundCheckEvent(bool isOnGround)
        {
            _isOnGround = isOnGround;
            if (isOnGround)
            {
                var velocity = new Vector2(_rigidbody2D.velocity.x, 0);
                _rigidbody2D.velocity = velocity;
                _playerChannel.OnVeloctyChanged(velocity);
                _collider2D.enabled = true;
            }
            else
            {
                _collider2D.enabled = false;
            }

            _rigidbody2D.gravityScale = isOnGround ? 0 : 1;
        }

        void Update()
        {
            if (!_isMovementActive)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _playerChannel.OnVeloctyChanged(_rigidbody2D.velocity);
                return;
            }
            
            var horizontalAxis = (int)Input.GetAxisRaw("Horizontal");
            var velocity = _rigidbody2D.velocity;

            if (horizontalAxis == 0)
            {
                velocity.x = 0;
            }
            else if(_isOnGround)
            {
                LookDirection(horizontalAxis == 1);
                velocity.x = horizontalAxis * _speed;
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt) && _isOnGround)
            {
                velocity.y = Mathf.Sqrt(2 * 9.8f * _jumpHeight);
            }

            _rigidbody2D.velocity = velocity;
            _playerChannel.OnVeloctyChanged(velocity);
        }

        private void LookDirection(bool faceRight)
        {
            transform.rotation = Quaternion.Euler(0, faceRight ? 0 : 180, 0);

            _playerChannel.FaceDirection = Vector2.right * (faceRight ? 1 : -1);
        }
    }
}
