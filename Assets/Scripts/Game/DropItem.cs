using System;
using ScriptableObjects.Inventory.ItemMetas;
using ScriptableObjects.Quests;

namespace Game
{
    [Serializable]
    public class DropItem
    {
        public InventoryItemMeta ItemMetaData;
        public float Percentage;
        public Quest AssosiatedQuest;
    }
}