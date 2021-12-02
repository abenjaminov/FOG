using System;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace UI.Shop
{
    public abstract class Shop : MonoBehaviour
    {
        [SerializeField] protected GameChannel _gameChannel;
        [SerializeField] protected InventoryChannel _inventoryChannel;
        [SerializeField] protected ShopItem _shopItemPrefab;
        [SerializeField] protected RectTransform _shopItemsContent;
        [SerializeField] protected GameObject _itemsArea;
        [SerializeField] protected Inventory _inventory;
        [SerializeField] protected float _itemOffset;
        
        protected InventoryItemMeta _currentItem;
        protected string _currentInputMessageId;
        protected List<InventoryItemMeta> _itemsInShop;
        private  List<ShopItem> _shopItems;
        private float shopItemHeight;

        private void Awake()
        {
            _itemsInShop = new List<InventoryItemMeta>();
            _shopItems = new List<ShopItem>();
            shopItemHeight = ((RectTransform)_shopItemPrefab.transform).sizeDelta.y + _itemOffset;
            _gameChannel.InputRequestClosedEvent += InputRequestClosedEvent;
        }
        
        private void OnDestroy()
        {
            _gameChannel.InputRequestClosedEvent -= InputRequestClosedEvent;
        }

        public void UpdateShop()
        {
            UpdateItemsDisplayed();
            
            _shopItemsContent.sizeDelta = new Vector2(_shopItemsContent.sizeDelta.x, shopItemHeight * _itemsInShop.Count);
            var index = 0;
            var limit = Mathf.Max(_itemsInShop.Count, _shopItems.Count);
            
            for (index = 0; index < limit; index++)
            {
                if (index > _itemsInShop.Count - 1)
                {
                    _shopItems[index].ShopItemDoubleClicked -= ShopItemDoubleClicked;
                    _shopItems[index].SetActive(false);
                    continue;
                }
                
                if(_shopItems.Count <= index) AddShopItem();

                var currentShopItem = _shopItems[index];
                currentShopItem.SetActive(true);
                ((RectTransform)currentShopItem.transform).localPosition = new Vector3(0, shopItemHeight * index, 0);
                
                SetItemInfo(_itemsInShop[index], currentShopItem);
                currentShopItem.ShopItemDoubleClicked += ShopItemDoubleClicked;
            }            
        }
        
        private void AddShopItem()
        {
            var newShopItem = Instantiate(_shopItemPrefab, _itemsArea.transform);
            _shopItems.Add(newShopItem);
        }

        protected abstract void UpdateItemsDisplayed();
        protected abstract void InputRequestClosedEvent(string messageId, string result, MessageOptions reason);
        protected abstract void SetItemInfo(InventoryItemMeta item, ShopItem shopItem);
        protected abstract void ShopItemDoubleClicked(ShopItem item);
    }
}