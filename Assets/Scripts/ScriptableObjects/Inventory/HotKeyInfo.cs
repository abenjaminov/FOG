using System;
using System.Collections.Generic;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "Hotkey Info", menuName = "Inventory/Hotkey Info")]
    public class HotKeyInfo : ScriptableObject
    {
        [HideInInspector] public Dictionary<KeyCode, InventoryItemMeta> HotkeyCodeToItemIdMap 
            = new Dictionary<KeyCode, InventoryItemMeta>();

        [SerializeField] private InventoryChannel _inventoryChannel;

        private void OnEnable()
        {
            _inventoryChannel.HotkeyAssignedEvent += HotkeyAssignedEvent;
            _inventoryChannel.HotkeyUnAssignedEvent += HotkeyUnAssignedEvent;
        }
        
        private void OnDisable()
        {
            _inventoryChannel.HotkeyAssignedEvent -= HotkeyAssignedEvent;
            _inventoryChannel.HotkeyUnAssignedEvent -= HotkeyUnAssignedEvent;
        }

        private void HotkeyUnAssignedEvent(KeyCode code)
        {
            if (HotkeyCodeToItemIdMap.ContainsKey(code))
            {
                HotkeyCodeToItemIdMap.Remove(code);
            }
        }
        
        private void HotkeyAssignedEvent(KeyCode code, InventoryItem item)
        {
            HotkeyCodeToItemIdMap[code] = item?.ItemMeta;
        }
    }
}