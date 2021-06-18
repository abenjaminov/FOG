using System;
using ScriptableObjects.Inventory.ItemMetas;

namespace Game
{
    [Serializable]
    public class DropItem
    {
        public InventoryItemMeta ItemMetaData;
        public float Percentage;
    }
}