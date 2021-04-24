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
            _playerChannel.Attack1Event += Attack1Event;
        }

        private void Attack1Event()
        {
            _animator.SetTrigger(CachedAnimatorPropertyNames.Attack1);
        }

        private void VelocityChangedEvent(Vector2 newVelocity)
        {
            _playerChannel.IsWalking = newVelocity.x != 0;
            _animator.SetBool(CachedAnimatorPropertyNames.IsWalking, _playerChannel.IsWalking);

            _playerChannel.IsJumping = newVelocity.y != 0;
            _animator.SetBool(CachedAnimatorPropertyNames.IsJumping, _playerChannel.IsJumping);
        } 
    }
}