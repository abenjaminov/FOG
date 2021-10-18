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

        public void Use(int amount, Entity.Player.Player player = null)
        {
            if (ItemMeta.Use(player))
            {
                Amount = Mathf.Max(0, Amount - amount);
            }
        }
    }
}