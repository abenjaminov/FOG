using System;
using System.Timers;
using Animations;
using Character;
using Game;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, ICharacterMovement
    {
        private bool _isOnGround;
        
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private GameChannel _gameChannel;
        [SerializeField] private GameObject _visualsToRotate;

        private bool _isMovementActive = true;
        private Vector2 _velocity;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _gameChannel.HitLevelBoundsEvent += HitLevelBoundsEvent;
        }

        private void OnDestroy()
        {
            _gameChannel.HitLevelBoundsEvent -= HitLevelBoundsEvent;
        }

        private void HitLevelBoundsEvent(LevelBounds arg0)
        {
            SetHorizontalVelocity(0);
            Debug.Log("HitLevelBounds");
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

        public Vector3 GetWorldMovementDirection()
        {
            return _visualsToRotate.transform.rotation.y != 0 ? Vector2.left : Vector2.right;
        }
    }
}
