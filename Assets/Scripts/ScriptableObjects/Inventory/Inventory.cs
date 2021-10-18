using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

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
            else if (itemMetaData is EquipmentItemMeta)
            {
                inventoryItem = new InventoryItem()
                {
                    ItemMeta = itemMetaData,
                    Amount = 1
                };
                OwnedItems.Add(inventoryItem);
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

            _inventoryChannel.OnItemAmountChangedSilent(inventoryItem, amountToAdd);
            
            return inventoryItem;
        }
        
        public void AddItem(InventoryItemMeta itemMetaData, int amountToAdd)
        {
            var inventoryItem = AddItemSilent(itemMetaData, amountToAdd);

            _inventoryChannel.OnItemAmountChanged(inventoryItem, amountToAdd);
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
            
            _inventoryChannel.OnItemAmountChanged(item, -1);
        }

        public InventoryItemMeta GetItemMetaById(string itemMetaId)
        {
            return _inventoryItemMetas.GetItemMetaById(itemMetaId);
        }
    }
}