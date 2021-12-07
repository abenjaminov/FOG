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

        private void OnEnable()
        {
            _inventoryChannel.UseItemRequestEvent += UseItem;

            foreach (var item in OwnedItems)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = Guid.NewGuid().ToString();
                }
            }
        }

        private void OnDisable()
        {
            _inventoryChannel.UseItemRequestEvent -= UseItem;
        }

        public InventoryItem AddItemSilent(InventoryItemMeta itemMetaData, int amountToAdd)
        {
            InventoryItem inventoryItem;
            
            switch (itemMetaData)
            {
                case CurrencyItemMeta currentItemMeta:
                    CurrencyItem.Amount += amountToAdd;
                    inventoryItem = CurrencyItem;
                    break;
                case EquipmentItemMeta equipmentItemMeta:
                    inventoryItem = GetNewInventoryItem(itemMetaData, 1);
                    OwnedItems.Add(inventoryItem);
                    break;
                default:
                {
                    inventoryItem = OwnedItems.FirstOrDefault(x => x.ItemMeta.Id == itemMetaData.Id);
            
                    if (inventoryItem != null)
                    {
                        inventoryItem.Amount += amountToAdd;
                    }
                    else
                    {
                        inventoryItem = GetNewInventoryItem(itemMetaData, amountToAdd);
                        OwnedItems.Add(inventoryItem);
                    }

                    break;
                }
            }

            _inventoryChannel.OnItemAmountChangedSilent(inventoryItem, amountToAdd);
            
            return inventoryItem;
        }

        private InventoryItem GetNewInventoryItem(InventoryItemMeta meta, int amount)
        {
            return new InventoryItem()
            {
                Id = Guid.NewGuid().ToString(),
                ItemMeta = meta,
                Amount = amount
            };
        }

        public void AddItem(InventoryItemMeta itemMetaData, int amountToAdd)
        {
            var inventoryItem = AddItemSilent(itemMetaData, amountToAdd);

            if (inventoryItem.Amount == 0)
            {
                RemoveItemInternal(inventoryItem);
            }

            _inventoryChannel.OnItemAmountChanged(inventoryItem, amountToAdd);
        }

        public void RemoveItem(InventoryItem item)
        {
            RemoveItemInternal(item);
            
            _inventoryChannel.OnItemAmountChangedSilent(item, -1);
            _inventoryChannel.OnItemAmountChanged(item, -1);
        }

        public void DecreaseItem(InventoryItem item, int? amount)
        {
            if (item.ItemMeta is EquipmentItemMeta)
            {
                RemoveItem(item);
                return;
            }
            
            AddItem(item.ItemMeta, -1 * (amount ?? 1));
        }

        private void RemoveItemInternal(InventoryItem item)
        {
            var itemToRemove = OwnedItems.FirstOrDefault(x => x.Id == item.Id);

            if (itemToRemove != null)
            {
                OwnedItems.Remove(itemToRemove);
            }
        }

        private void UseItem(InventoryItem item, Entity.Player.Player player = null)
        {
            if (!item.Use(1, player))
            {
                _inventoryChannel.OnFailedToUseItem(item);
                return;
            }
            
            if (item.Amount == 0)
            {
                RemoveItemInternal(item);
            }

            _inventoryChannel.OnItemAmountChangedSilent(item, -1);
            _inventoryChannel.OnItemAmountChanged(item, -1);
        }

        public InventoryItemMeta GetItemMetaById(string itemMetaId)
        {
            return _inventoryItemMetas.GetItemMetaById(itemMetaId);
        }
    }
}