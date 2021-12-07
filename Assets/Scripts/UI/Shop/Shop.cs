using System;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using UI.Screens;
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
        [SerializeField] private EquipmentDetailsPanel _equipmentDetailsPanel;
        
        protected ShopItem _currentItem;
        protected string _currentInputMessageId;
        protected List<ShopItemInfo> _itemsInShop;
        private  List<ShopItem> _shopItems;
        private float shopItemHeight;

        protected virtual void Awake()
        {
            _itemsInShop = new List<ShopItemInfo>();
            _shopItems = new List<ShopItem>();
            shopItemHeight = ((RectTransform)_shopItemPrefab.transform).sizeDelta.y + _itemOffset;
            _gameChannel.InputRequestClosedEvent += InputRequestClosedEvent;
            _gameChannel.MessageClosedEvent += MessageClosedEvent;
        }
        
        protected virtual void OnDestroy()
        {
            _gameChannel.InputRequestClosedEvent -= InputRequestClosedEvent;
            _gameChannel.MessageClosedEvent -= MessageClosedEvent;

            foreach (var shopItem in _shopItems)
            {
                shopItem.ShopItemDoubleClicked -= ShopItemDoubleClicked;
                shopItem.ShopItemMouseEnter -= ShopItemMouseEnter;
                shopItem.ShopItemMouseExit -= ShopItemMouseExit;
            }
        }

        public virtual void UpdateShop()
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
                    _shopItems[index].ShopItemMouseEnter -= ShopItemMouseEnter;
                    _shopItems[index].ShopItemMouseExit -= ShopItemMouseExit;
                    _shopItems[index].SetActive(false);
                    continue;
                }
                
                if(_shopItems.Count <= index) AddShopItem();

                var currentShopItem = _shopItems[index];
                currentShopItem.SetActive(true);
                ((RectTransform)currentShopItem.transform).localPosition = new Vector3(0, shopItemHeight * index, 0);
                
                SetItemInfo(_itemsInShop[index], currentShopItem);
                currentShopItem.ShopItemDoubleClicked += ShopItemDoubleClicked;
                currentShopItem.ShopItemMouseEnter += ShopItemMouseEnter;
                currentShopItem.ShopItemMouseExit += ShopItemMouseExit;
            }            
        }

        private void ShopItemMouseExit(ShopItem item)
        {
            _equipmentDetailsPanel.HideItemDetails();
        }

        private void ShopItemMouseEnter(ShopItem item)
        {
            _equipmentDetailsPanel.ShowItemDetails(item.ItemMeta, item.GetBottomLeftCorner());
        }

        public void CloseShop()
        {
            _equipmentDetailsPanel.HideItemDetails();
        }
        
        private void AddShopItem()
        {
            var newShopItem = Instantiate(_shopItemPrefab, _itemsArea.transform);
            _shopItems.Add(newShopItem);
        }

        protected abstract void UpdateItemsDisplayed();
        protected abstract void InputRequestClosedEvent(string messageId, string result, MessageOptions reason);
        protected abstract void SetItemInfo(ShopItemInfo info, ShopItem shopItem);
        protected abstract void ShopItemDoubleClicked(ShopItem item);
        protected abstract void MessageClosedEvent(string messageId, MessageOptions reason);
    }
}