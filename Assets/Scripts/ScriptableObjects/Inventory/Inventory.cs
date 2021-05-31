using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Channels;
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

            if (itemMetaData.IsCurrency)
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
            }

            _inventoryChannel.OnItemAdded(newItem, item);
        }

        private void AddCurrency(InventoryItem currencyAddition, int amount)
        {
            CurrencyItem.Amount += amount;
            
            _inventoryChannel.OnItemAdded(currencyAddition, CurrencyItem);
        }

        public void RemoveItem(InventoryItemMeta itemMetaData, int amount)
        {
            var item = OwnedItems.FirstOrDefault(x => x.ItemMeta.Name == itemMetaData.Name);
            
            if (item != null)
            {
                item.Amount = Mathf.Max(0,item.Amount - amount);

                if (item.Amount == 0 )
                {
                    // TODO : Delete from inv / Drop
                }
            }
        }
    }

    [Serializable]
    public class InventoryItem
    {
        public InventoryItemMeta ItemMeta;
        public int Amount;
    }
}