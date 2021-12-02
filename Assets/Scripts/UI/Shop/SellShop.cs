﻿using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;

namespace UI.Shop
{
    public class SellShop : Shop
    {
        protected override void Awake()
        {
            base.Awake();
            
            
            _inventoryChannel.ItemAmountChangedSilentEvent += ItemAmountChangedSilentEvent;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _inventoryChannel.ItemAmountChangedSilentEvent -= ItemAmountChangedSilentEvent;
        }

        private void ItemAmountChangedSilentEvent(InventoryItem item, int amount)
        {
            UpdateShop();
        }

        protected override void MessageClosedEvent(string messageId, MessageOptions reason)
        {
            if (messageId != _currentInputMessageId) return;
            
            if (reason == MessageOptions.Yes)
            {
                SellItem();
            }
        }

        protected override void UpdateItemsDisplayed()
        {
            _itemsInShop ??= new List<ShopItemInfo>();

            _itemsInShop.Clear();

            _itemsInShop = _inventory.OwnedItems.Where(x => !x.ItemMeta.IsQuestItem).Select(x =>new ShopItemInfo()
            {
                Price = x.ItemMeta.PriceForSale,
                InventoryItem = x,
                ItemMeta = x.ItemMeta
            }).ToList();
        }

        protected override void InputRequestClosedEvent(string messageId, string result, MessageOptions reason)
        {
            if (messageId != _currentInputMessageId) return;
            
            if (reason == MessageOptions.Sell)
            {
                TrySellItem(result);
            }
        }

        protected override void SetItemInfo(ShopItemInfo info, ShopItem shopItem)
        {
            shopItem.SetItemMeta(info.ItemMeta);
            shopItem.SetNameText(info.Name);
            shopItem.SetPriceText(info.Price, _inventory.CurrencyItem.ItemMeta as CurrencyItemMeta);
            shopItem.SetIcon(info.ItemMeta.InventoryItemSprite);

            shopItem.SetItem(info.InventoryItem);

            var additionalText = info.ItemMeta.IsConsumable() ? info.InventoryItem.Amount + " Owned" : "";
            shopItem.SetAdditionalLineText(additionalText);
        }

        protected override void ShopItemDoubleClicked(ShopItem item)
        {
            _currentItem = item;

            if (!item.ItemMeta.IsConsumable())
            {
                _currentInputMessageId =
                    _gameChannel.ShowGameMessage($"Are you want to sell {item.ItemMeta.Name}?", new List<MessageOptions>() {
                        MessageOptions.Yes,
                        MessageOptions.No
                    });
            }
            else
            {
                _currentInputMessageId =
                    _gameChannel.ShowInputMessage($"How many {item.ItemMeta.Name}s would you like to sell?", new List<MessageOptions>() {
                        MessageOptions.Sell,
                        MessageOptions.Cancel
                    });    
            }
        }

        private void TrySellItem(string amountToSell)
        {
            if (!int.TryParse(amountToSell, out var amount)) return;
            var inventoryItem = _inventory.OwnedItems.FirstOrDefault(x => x.Id == _currentItem.Item.Id);
            
            if(inventoryItem == null) return;

            if (inventoryItem.Amount < amount)
            {
                _gameChannel.OnGameErrorEvent($"Only {inventoryItem.Amount} {inventoryItem.ItemMeta.Name} left");
            }
            else
            {
                SellItem(amount);
            }
        }

        private void SellItem(int amount = 1)
        {
            _inventory.DecreaseItem(_currentItem.Item, amount);
            _inventory.AddItem(_inventory.CurrencyItem.ItemMeta, _currentItem.ItemMeta.PriceForSale * amount);
            UpdateShop();
        }
    }
}