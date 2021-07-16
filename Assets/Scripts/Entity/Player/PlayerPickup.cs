using System;
using System.Collections.Generic;
using Game;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using UnityEngine;

namespace Character.Player
{
    public class PlayerPickup : MonoBehaviour
    {
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private Inventory _playerInventory;
        [SerializeField] private Transform _pickupTransform;
        private List<Drop> _dropsInRange;

        private KeySubscription _pickupSubscription;
        
        private void Awake()
        {
            _dropsInRange = new List<Drop>();
            
            _pickupSubscription = _inputChannel.SubscribeKeyDown(KeyCode.Z, Pickup);
        }

        private void OnDestroy()
        {
            _pickupSubscription.Unsubscribe();
        }

        private void Pickup()
        {
            if (_dropsInRange.Count == 0) return;

            var itemToPickup = _dropsInRange[0];
            _dropsInRange.RemoveAt(0);

            itemToPickup.OnPickedUp(_pickupTransform);

            _playerInventory.AddItem(itemToPickup.InventoryItemMeta, itemToPickup.Amount);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(typeof(Drop), out var drop))
            {
                _dropsInRange.Add((Drop)drop);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(typeof(Drop), out var drop))
            {
                _dropsInRange.Remove((Drop)drop);
            }
        }
    }
}