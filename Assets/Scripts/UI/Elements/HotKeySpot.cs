﻿using System;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using TMPro;
using UI.Behaviours;
using UI.Screens;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Elements
{
    public class HotKeySpot : MonoBehaviour, IDragTarget
    {
        [SerializeField] private Entity.Player.Player _player;
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Image _itemImage;
        [SerializeField] private KeyCode _keyCode;

        private KeySubscription _hotKeySubscription;
        private InventoryItemView _currentItemView;

        private void Awake()
        {
            _inventoryChannel.ItemAmountChangedEvent += ItemAmountChangedEvent;
        }

        private void ItemAmountChangedEvent(InventoryItem arg0, int arg1)
        {
            if(_currentItemView != null && _currentItemView.ItemMeta.Id == arg0.ItemMeta.Id)
                UpdateUI();
        }

        public void DragDropped(IDraggable draggable)
        {
            if (!draggable.GetGameObject().TryGetComponent(typeof(InventoryItemView), out var component)) return;

            _currentItemView = (InventoryItemView)component;

            if (!_currentItemView.IsUsable())
            {
                _currentItemView = null;
                return;
            }
            
            UpdateUI();

            _hotKeySubscription = _inputChannel.SubscribeKeyDown(_keyCode, KeyDown);
        }

        private void UpdateUI()
        {
            _itemImage.sprite = _currentItemView.ItemSprite.sprite;
            var color = _itemImage.color;
            _itemImage.color = new Color(color.r, color.g, color.b, 255);
            _amountText.SetText(_currentItemView.InventoryItem.Amount.ToString());
        }

        private void KeyDown()
        {
            _inventoryChannel.OnUseItemRequest(_currentItemView.InventoryItem, _player);
        }

        public bool IsEmpty()
        {
            return _itemImage.sprite == null;
        }

        private void OnDestroy()
        {
            _hotKeySubscription?.Unsubscribe();
            _inventoryChannel.ItemAmountChangedEvent -= ItemAmountChangedEvent;
        }
    }
}