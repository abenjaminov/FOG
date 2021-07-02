using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class Teleport : MonoBehaviour
    {
        private Entity.Player.Player playerReference;
        public SceneAsset Source;
        public SceneAsset Destination;
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private InputChannel _inputChannel;
        public Transform CenterTransform;
        
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
            _locationsChannel.OnChangeLocation(Destination, Source);
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