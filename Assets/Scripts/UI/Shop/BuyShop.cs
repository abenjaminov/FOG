using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Inventory.ItemMetas;
using ScriptableObjects.Shops;
using UnityEngine;

namespace UI.Shop
{
    public class BuyShop : Shop
    {
        [HideInInspector] public ShopInfo ShopInfo;

        protected override void UpdateItemsDisplayed()
        {
            _itemsInShop.Clear();

            _itemsInShop = ShopInfo.ShopItems.Select(x => new ShopItemInfo()
            {
                Price = x.PriceInShop,
                ItemMeta = x
            }).ToList();
        }

        protected override void InputRequestClosedEvent(string messageId, string result, MessageOptions reason)
        {
            if (messageId != _currentInputMessageId) return;
            
            if (reason == MessageOptions.Buy)
            {
                TryBuyItem(result);
            }
        }

        protected override void MessageClosedEvent(string messageId, MessageOptions reason)
        {
            if (messageId != _currentInputMessageId) return;
            
            if (reason == MessageOptions.Yes)
            {
                _inventory.AddItem(_currentItem.ItemMeta, 1);
            }
        }
        
        private void TryBuyItem(string result)
        {
            if (!int.TryParse(result, out var amount)) return;
            
            if (_inventoryChannel.UseCoinsRequest(amount * _currentItem.ItemMeta.PriceInShop))
            {
                _inventory.AddItem(_currentItem.ItemMeta, amount);
            }
            else
            {
                _gameChannel.OnGameErrorEvent("Insufficient Funds");
            }
        }

        protected override void SetItemInfo(ShopItemInfo info, ShopItem shopItem)
        {
            shopItem.SetItemMeta(info.ItemMeta);
            shopItem.SetNameText(info.Name);
            shopItem.SetPriceText(info.Price, _inventory.CurrencyItem.ItemMeta as CurrencyItemMeta);
            shopItem.SetIcon(info.ItemMeta.InventoryItemSprite);
            shopItem.SetAdditionalLineText("");
        }
        
        protected override void ShopItemDoubleClicked(ShopItem item)
        {
            _currentItem = item;
            
            if (!item.ItemMeta.IsConsumable())
            {
                _currentInputMessageId =
                    _gameChannel.ShowGameMessage($"Are you sure you want to buy {item.ItemMeta.Name}", new List<MessageOptions>() {
                        MessageOptions.Yes,
                        MessageOptions.No
                    });
            }
            else
            {
                _currentInputMessageId =
                    _gameChannel.ShowInputMessage($"How many {item.ItemMeta.Name}s would you like to buy?", new List<MessageOptions>() {
                        MessageOptions.Buy,
                        MessageOptions.Cancel
                    });
            }
        }
    }
}