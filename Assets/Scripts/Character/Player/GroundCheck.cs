using System;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private PlayerChannel _playerChannel;
        public bool IsOnGround;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                IsOnGround = true;
                Debug.Log("On Ground");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                IsOnGround = false;
                Debug.Log("Off ground");
            }
        }
    }
}
