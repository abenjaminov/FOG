using System.Linq;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace ScriptableObjects.Quests
{
    [CreateAssetMenu(fileName = "Collect Items Quest", menuName = "Quest/Collect Items Quest")]
    public class CollectItemsQuest : ProgressQuest
    {
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private InventoryItemMeta _inventoryItemMeta;
        [SerializeField] private int _amountToCollect;
        [SerializeField] private Inventory.Inventory _playerInventory;

        protected override void OnEnable()
        {
            base.OnEnable();

            MaxValue = _amountToCollect;
        }

        protected override void QuestCompleted()
        {
            _inventoryChannel.ItemAddedEvent -= ItemAddedEvent;
            _playerInventory.AddItem(_inventoryItemMeta, -_amountToCollect);
            Complete();
        }

        protected override void QuestActive()
        {
            _inventoryChannel.ItemAddedEvent += ItemAddedEvent;
            
            InventoryItem itemInInventory;
            
            if (_inventoryItemMeta is CurrencyItemMeta)
            {
                itemInInventory = _playerInventory.CurrencyItem;
            }
            else
            {
                itemInInventory = 
                    _playerInventory.OwnedItems.FirstOrDefault(x => x.ItemMeta.Id == _inventoryItemMeta.Id);    
            }

            TryComplete(itemInInventory);
        }

        private void ItemAddedEvent(InventoryItem itemAddition, InventoryItem item)
        {
            if (State == QuestState.PendingComplete) return;
            if (item.ItemMeta.Id != _inventoryItemMeta.Id) return;

            TryComplete(item);
        }

        private void TryComplete(InventoryItem itemInInventory)
        {
            ProgressMade(itemInInventory?.Amount ?? 0);
            
            if (itemInInventory != null && itemInInventory.Amount >= _amountToCollect)
            {
                Complete();
            }
        }
    }
}