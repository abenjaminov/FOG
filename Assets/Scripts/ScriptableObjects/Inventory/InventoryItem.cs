using System;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public InventoryItemMeta ItemMeta;
        public int Amount;

        public bool Use(int amount, Entity.Player.Player player = null)
        {
            var result = ItemMeta.Use(player); 
            if (result)
            {
                Amount = Mathf.Max(0, Amount - amount);
            }

            return result;
        }
    }
}