using System;
using Animations;
using Character;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, ICharacterMovement
    {
        private bool _isOnGround;
        
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private GameObject _visualsToRotate;

        private bool _isMovementActive = true;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void SetHorizontalVelocity(float horizontalVelocity)
        {
            _rigidbody2D.velocity = new Vector2(horizontalVelocity, _rigidbody2D.velocity.y);
        }
        
        public void SetVerticalVelocity(float verticalVelocity)
        {
            _rigidbody2D.velocity = new Vector2( _rigidbody2D.velocity.x,verticalVelocity);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        public void SetYRotation(float yRotation)
        {
            _visualsToRotate.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
