using System.Collections.Generic;
using ScriptableObjects.Inventory.ItemMetas;
using ScriptableObjects.Shops;

namespace UI.Shop
{
    public class BuyShop : Shop
    {
        public ShopInfo ShopInfo;

        protected override void UpdateItemsDisplayed()
        {
            _itemsInShop.Clear();

            _itemsInShop = ShopInfo.ShopItems;
        }

        protected override void InputRequestClosedEvent(string messageId, string result, MessageOptions reason)
        {
            if (messageId != _currentInputMessageId) return;
            
            if (reason == MessageOptions.Buy)
            {
                TryBuyItem(result);
            }
        }

        private void TryBuyItem(string result)
        {
            if (!int.TryParse(result, out var amount)) return;
            
            if (_inventoryChannel.UseCoinsRequest(amount * _currentItem.PriceInShop))
            {
                _inventory.AddItem(_currentItem, amount);
            }
            else
            {
                _gameChannel.OnGameErrorEvent("Insufficient Funds");
            }
        }

        protected override void SetItemInfo(InventoryItemMeta item, ShopItem shopItem)
        {
            shopItem.SetItem(item);
            shopItem.SetNameText(item.Name);
            shopItem.SetPriceText(item.PriceInShop, _inventory.CurrencyItem.ItemMeta as CurrencyItemMeta);
            shopItem.SetIcon(item.InventoryItemSprite);
        }
        
        protected override void ShopItemDoubleClicked(ShopItem item)
        {
            _currentItem = item.Item;
            _currentInputMessageId =
                _gameChannel.ShowInputMessage($"How many {item.Item.Name}s would you like to buy?", new List<MessageOptions>() {
                    MessageOptions.Buy,
                    MessageOptions.Cancel
                });
        }
    }
}