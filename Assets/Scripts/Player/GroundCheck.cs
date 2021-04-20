using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private PlayerChannel _playerChannel;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _playerChannel.OnGroundCheckEvent(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _playerChannel.OnGroundCheckEvent(false);
            }
        }
    }
}
