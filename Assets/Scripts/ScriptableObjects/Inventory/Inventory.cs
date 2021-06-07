using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 1)]
    public class Inventory : ScriptableObject
    {
        [SerializeField] private InventoryChannel _inventoryChannel;
        public List<InventoryItem> OwnedItems;
        public InventoryItem CurrencyItem;

        public void AddItem(InventoryItemMeta itemMetaData, int amount)
        {
            var newItem = new InventoryItem()
            {
                ItemMeta = itemMetaData,
                Amount = amount
            };

            if (itemMetaData is CurrencyItemMeta)
            {
                AddCurrency(newItem, amount);
                return;
            }
            
            var item = OwnedItems.FirstOrDefault(x => x.ItemMeta.Name == itemMetaData.Name);
            
            if (item != null)
            {
                item.Amount += newItem.Amount;
            }
            else
            {
                OwnedItems.Add(newItem);
                item = newItem;
            }

            _inventoryChannel.OnItemAdded(newItem, item);
        }

        private void AddCurrency(InventoryItem currencyAddition, int amount)
        {
            CurrencyItem.Amount += amount;
            
            _inventoryChannel.OnItemAdded(currencyAddition, CurrencyItem);
        }

        private void RemoveItem(InventoryItem item)
        {
            var itemToRemove = OwnedItems.FirstOrDefault(x => x == item);
            
            if (itemToRemove != null)
            {
                OwnedItems.Remove(itemToRemove);
            }
        }

        public void UseItem(Entity.Player.Player player, InventoryItem item)
        {
            item.Use(player, 1);
            
            if (item.Amount == 0)
            {
                RemoveItem(item);
            }
        }
    }
}