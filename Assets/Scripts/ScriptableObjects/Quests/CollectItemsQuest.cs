using System.Linq;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace ScriptableObjects.Quests
{
    public class CollectItemsQuest : Quest
    {
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private InventoryItemMeta _inventoryItemMeta;
        [SerializeField] private int _amountToCollect;
        [SerializeField] private Inventory.Inventory _playerInventory;
        protected override void QuestCompleted()
        {
            _playerInventory.AddItem(_inventoryItemMeta, -_amountToCollect);   
        }

        protected override void QuestActive()
        {
            _inventoryChannel.ItemAddedEvent += ItemAddedEvent;
            
            var itemInInventory =
                _playerInventory.OwnedItems.FirstOrDefault(x => x.ItemMeta.Id == _inventoryItemMeta.Id);

            TryComplete(itemInInventory);
        }

        private void ItemAddedEvent(InventoryItem itemAddition, InventoryItem item)
        {
            if (item.ItemMeta.Id != _inventoryItemMeta.Id) return;

            TryComplete(item);
        }

        private void TryComplete(InventoryItem itemInInventory)
        {
            if (itemInInventory != null && itemInInventory.Amount >= _amountToCollect)
            {
                Complete();
            }
        }
    }
}