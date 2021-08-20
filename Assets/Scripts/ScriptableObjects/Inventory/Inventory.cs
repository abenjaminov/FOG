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
        [SerializeField] private InventoryItemsList _inventoryItemMetas;
        public List<InventoryItem> OwnedItems;
        public InventoryItem CurrencyItem;

        public InventoryItem AddItemSilent(InventoryItemMeta itemMetaData, int amountToAdd)
        {
            InventoryItem inventoryItem;
            
            if (itemMetaData is CurrencyItemMeta)
            {
                CurrencyItem.Amount += amountToAdd;
                inventoryItem = CurrencyItem;
            }
            else
            {
                inventoryItem = OwnedItems.FirstOrDefault(x => x.ItemMeta.Id == itemMetaData.Id);
            
                if (inventoryItem != null)
                {
                    inventoryItem.Amount += amountToAdd;
                }
                else
                {
                    inventoryItem = new InventoryItem()
                    {
                        ItemMeta = itemMetaData,
                        Amount = amountToAdd
                    };
                    OwnedItems.Add(inventoryItem);
                }
            }

            return inventoryItem;
        }
        
        public void AddItem(InventoryItemMeta itemMetaData, int amountToAdd)
        {
            var inventoryItem = AddItemSilent(itemMetaData, amountToAdd);

            _inventoryChannel.OnItemAdded(inventoryItem, amountToAdd);
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

        public InventoryItemMeta GetItemMetaById(string itemMetaId)
        {
            return _inventoryItemMetas.GetItemMetaById(itemMetaId);
        }
    }
}