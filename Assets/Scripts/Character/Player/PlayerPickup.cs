﻿using System;
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
        private List<Drop> _dropsInRange;

        private void Awake()
        {
            _dropsInRange = new List<Drop>();
            
            _inputChannel.RegisterKeyDown(KeyCode.Z, Pickup);
        }

        private void Pickup()
        {
            if (_dropsInRange.Count == 0) return;

            var itemToPickup = _dropsInRange[0];
            _dropsInRange.RemoveAt(0);

            itemToPickup.OnPickedUp(transform);

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