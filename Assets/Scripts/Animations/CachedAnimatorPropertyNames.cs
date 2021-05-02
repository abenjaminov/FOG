using UnityEngine;

namespace Animations
{
    public static class CachedAnimatorPropertyNames
    {
        public static readonly int IsWalking = Animator.StringToHash("IsWalking");
        public static readonly int IsJumping  = Animator.StringToHash("IsJumping");
        public static readonly int Attack1 = Animator.StringToHash("Attack1");
        public static readonly int IsFalling = Animator.StringToHash("IsFalling");
        public static readonly int Dead = Animator.StringToHash("Dead");
    }
}