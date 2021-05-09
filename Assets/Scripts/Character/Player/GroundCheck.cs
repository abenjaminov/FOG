using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class GroundCheck : MonoBehaviour
    {
        public bool IsOnGround;
        public UnityAction<bool, float> OnGroundChanged;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                IsOnGround = true;
                OnGroundChanged?.Invoke(IsOnGround, other.transform.position.y);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                IsOnGround = false;
                OnGroundChanged?.Invoke(IsOnGround, other.transform.position.y);
            }
        }
    }
}
