using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using UI.Elements;
using UnityEngine;

namespace UI.Screens
{
    public class HotKeyPanel : MonoBehaviour
    {
        [SerializeField] private List<HotKeySpot> _hotKeySpots = new List<HotKeySpot>();
        [SerializeField] private InventoryChannel _inventoryChannel;

        private void Awake()
        {
            _inventoryChannel.HotkeyAssignedEvent += HotkeyAssignedEvent;
            _inventoryChannel.HotkeyUnAssignedEvent += HotkeyUnAssignedEvent;
        }

        private void OnDestroy()
        {
            _inventoryChannel.HotkeyAssignedEvent -= HotkeyAssignedEvent;
            _inventoryChannel.HotkeyUnAssignedEvent -= HotkeyUnAssignedEvent;
        }

        private void HotkeyUnAssignedEvent(KeyCode code)
        {
            var sameHotSpot = _hotKeySpots.FirstOrDefault(x => x.KeyCode == code);
            
            if (sameHotSpot == null) return;
            
            sameHotSpot.UnAssignItem();
        }

        private void HotkeyAssignedEvent(KeyCode code, InventoryItem item)
        {
            var sameHotSpot = _hotKeySpots.FirstOrDefault(x => x.KeyCode == code);

            if (sameHotSpot == null) return;
            if (sameHotSpot.InventoryItem != null && sameHotSpot.InventoryItem.Id == item.Id) return;
            
            var similarItemDifferentHotSpot = _hotKeySpots.FirstOrDefault(x => x.InventoryItem != null && 
                                                                               item.Id == x.InventoryItem.Id && 
                                                                               x.KeyCode != code);
            
            sameHotSpot.AssignItem(item);
            
            if(similarItemDifferentHotSpot != null)
                similarItemDifferentHotSpot.AssignItem(null);
        }
    }
}