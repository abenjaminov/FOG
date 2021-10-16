using System;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.GameConfiguration;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class Teleport : MonoBehaviour
    {
        private Entity.Player.Player playerReference;
        public SceneMeta Source;
        public SceneMeta Destination;
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private KeyboardConfiguration _keyboardConfiguration;
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
                keyUpSub = _inputChannel.SubscribeKeyDown(_keyboardConfiguration.EnterTeleport, OnTeleport);
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