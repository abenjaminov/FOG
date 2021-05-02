using System;
using Animations;
using Character;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, ICharacterMovement
    {
        [SerializeField] private float _jumpHeight;

        private bool _isOnGround;
        
        private Rigidbody2D _rigidbody2D;

        private bool _isMovementActive = true;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            
        }

        public void SetHorizontalVelocity(float horizontalVelocity)
        {
            _rigidbody2D.velocity = new Vector2(horizontalVelocity, _rigidbody2D.velocity.y);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        public void SetYRotation(float yRotation)
        {
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
