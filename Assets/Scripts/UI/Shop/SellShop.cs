using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Inventory.ItemMetas;

namespace UI.Shop
{
    public class SellShop : Shop
    {
        protected override void UpdateItemsDisplayed()
        {
            _itemsInShop ??= new List<InventoryItemMeta>();

            _itemsInShop.Clear();

            _itemsInShop = _inventory.OwnedItems.Select(x => x.ItemMeta).ToList();
        }

        protected override void InputRequestClosedEvent(string messageId, string result, MessageOptions reason)
        {
            if (messageId != _currentInputMessageId) return;
            
            if (reason == MessageOptions.Buy)
            {
                TrySellItem(result);
            }
        }

        protected override void SetItemInfo(InventoryItemMeta item, ShopItem shopItem)
        {
            shopItem.SetItem(item);
            shopItem.SetNameText(item.Name);
            shopItem.SetPriceText(item.PriceForSale, _inventory.CurrencyItem.ItemMeta as CurrencyItemMeta);
            shopItem.SetIcon(item.InventoryItemSprite);
            
            var inventoryItem = _inventory.OwnedItems.FirstOrDefault(x => x.ItemMeta.Id == item.Id);

            if (inventoryItem == null) return;
            
            shopItem.SetAdditionalLineText(inventoryItem.Amount.ToString() + " Owned");
        }

        protected override void ShopItemDoubleClicked(ShopItem item)
        {
            _currentItem = item.Item;
            _currentInputMessageId =
                _gameChannel.ShowInputMessage($"How many {item.Item.Name}s would you like to sell?", new List<MessageOptions>() {
                    MessageOptions.Sell,
                    MessageOptions.Cancel
                });
        }

        private void TrySellItem(string amountToSell)
        {
            if (!int.TryParse(amountToSell, out var amount)) return;
            var inventoryItem = _inventory.OwnedItems.FirstOrDefault(x => x.ItemMeta.Id == _currentItem.Id);
            
            if(inventoryItem == null) return;

            if (inventoryItem.Amount < amount)
            {
                _gameChannel.OnGameErrorEvent($"Only {inventoryItem.Amount} {inventoryItem.ItemMeta.Name} left");
            }
            else
            {
                _inventory.AddItem(_currentItem,-1 * amount);
                _inventory.AddItem(_inventory.CurrencyItem.ItemMeta, _currentItem.PriceForSale * amount);
                UpdateShop();
            }
        }
    }
}