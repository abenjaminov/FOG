using System;
using Animations;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private PlayerChannel _playerChannel;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _playerChannel.VelocityChangedEvent += VelocityChangedEvent;
        }

        private void VelocityChangedEvent(Vector2 newVelocity)
        {
            _animator.SetBool(CachedAnimatorPropertyNames.IsWalking, newVelocity.x != 0);
            _animator.SetBool(CachedAnimatorPropertyNames.IsJumping, newVelocity.y != 0);
        }
    }
}