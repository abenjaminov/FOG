using System;
using System.Collections.Generic;
using Game;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Character.Player
{
    public class PlayerPickup : MonoBehaviour
    {
        [SerializeField] private InputChannel _inputChannel;
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

            // TODO : Add To Inventory
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