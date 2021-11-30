using System;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using ScriptableObjects.Shops;
using UnityEngine;

namespace UI.Shop
{
    public class BuyShop : MonoBehaviour
    {
        [SerializeField] private GameChannel _gameChannel;
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private ShopItem _shopItemPrefab;
        [SerializeField] private RectTransform _shopItemsContent;
        [SerializeField] private GameObject _itemsArea;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private float _itemOffset;
        
        private float shopItemHeight;
        private ShopInfo _shopInfo;
        private List<ShopItem> _shopItems;
        private string _currentInputMessageId;
        private InventoryItemMeta _currentItem;

        private void Awake()
        {
            _shopItems = new List<ShopItem>();
            shopItemHeight = ((RectTransform)_shopItemPrefab.transform).sizeDelta.y + _itemOffset;
            _gameChannel.InputRequestClosedEvent += InputRequestClosedEvent;
        }

        private void OnDestroy()
        {
            _gameChannel.InputRequestClosedEvent -= InputRequestClosedEvent;
        }

        private void InputRequestClosedEvent(string messageId, string result, MessageOptions reason)
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

        public void UpdateShop(ShopInfo shopInfo)
        {
            _shopItemsContent.sizeDelta = new Vector2(_shopItemsContent.sizeDelta.x, shopItemHeight * shopInfo.ShopItems.Count);
            var index = 0;
            var limit = Mathf.Max(shopInfo.ShopItems.Count, _shopItems.Count);
            
            for (index = 0; index < limit; index++)
            {
                if (index > shopInfo.ShopItems.Count - 1)
                {
                    _shopItems[index].ShopItemDoubleClicked -= ShopItemDoubleClicked;
                    _shopItems[index].SetActive(false);
                    continue;
                }
                
                if(_shopItems.Count <= index) AddShopItem();

                var currentShopItem = _shopItems[index];
                currentShopItem.SetActive(true);
                ((RectTransform)currentShopItem.transform).localPosition = new Vector3(0, shopItemHeight * index, 0);
                
                currentShopItem.SetItem(shopInfo.ShopItems[index], _inventory.CurrencyItem.ItemMeta as CurrencyItemMeta);
                currentShopItem.ShopItemDoubleClicked += ShopItemDoubleClicked;
            }            
        }

        private void ShopItemDoubleClicked(ShopItem item)
        {
            _currentItem = item.Item;
            _currentInputMessageId =
                _gameChannel.ShowInputMessage($"How many {item.Item.Name}s would you like to buy?", new List<MessageOptions>() {
                    MessageOptions.Buy,
                    MessageOptions.Cancel
                });
        }

        private void AddShopItem()
        {
            var newShopItem = Instantiate(_shopItemPrefab, _itemsArea.transform);
            _shopItems.Add(newShopItem);
        }
    }
}