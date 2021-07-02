using ScriptableObjects.Channels;
using UnityEngine;

namespace Game
{
    public class Teleport : MonoBehaviour
    {
        private Entity.Player.Player playerReference;
        [SerializeField] private InputChannel _inputChannel;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<Entity.Player.Player>();

            if (player != null)
            {
                playerReference = player;
                _inputChannel.RegisterKeyDown(KeyCode.UpArrow, OnTeleport);
            }
        }

        private void OnTeleport()
        {
            Debug.Log("Teleport!!");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var player = other.GetComponent<Entity.Player.Player>();

            if (player != null)
            {
                playerReference = null;
                _inputChannel.UnregisterKeyDown(KeyCode.UpArrow, OnTeleport);
            }
        }
    }
}