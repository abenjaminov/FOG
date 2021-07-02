using System;
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

        private KeySubscription keyUpSub;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (playerReference != null) return;
            
            var player = other.GetComponent<Entity.Player.Player>();

            if (player != null)
            {
                playerReference = player;
                
                keyUpSub?.Unsubscribe();
                keyUpSub = _inputChannel.SubscribeKeyDown(KeyCode.UpArrow, OnTeleport);
            }
        }

        private void OnTeleport()
        {
            _locationsChannel.OnChangeLocation(Destination, Source);
        }

        private void OnDestroy()
        {
            keyUpSub?.Unsubscribe();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (playerReference == null) return;
            
            var player = other.GetComponent<Entity.Player.Player>();

            if (player != null)
            {
                playerReference = null;
                keyUpSub?.Unsubscribe();
                keyUpSub = null;
            }
        }
    }
}