using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using TMPro;
using UI.Behaviours;
using UI.Mouse;
using UI.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class HotKeySpot : MonoBehaviour, IDragTarget, IRightClickHandler
    {
        [SerializeField] private Entity.Player.Player _player;
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Image _itemImage;
        [SerializeField] private KeyCode _keyCode;

        private KeySubscription _hotKeySubscription;
        private InventoryItem _currentInventoryItem;

        private void Awake()
        {
            _inventoryChannel.ItemAmountChangedEvent += ItemAmountChangedEvent;
        }

        private void ItemAmountChangedEvent(InventoryItem inventoryItem, int amount)
        {
            if (_currentInventoryItem == null || _currentInventoryItem.ItemMeta.Id != inventoryItem.ItemMeta.Id) return;
                
            if (inventoryItem.Amount == 0)
                _currentInventoryItem = null;
            
            UpdateUI();
        }

        public void DragDropped(IDraggable draggable)
        {
            if (!draggable.GetGameObject().TryGetComponent(typeof(InventoryItemView), out var component)) return;

            _currentInventoryItem = ((InventoryItemView)component).InventoryItem;

            if (!_currentInventoryItem.ItemMeta.IsConsumable())
            {
                _currentInventoryItem = null;
                return;
            }
            
            UpdateUI();

            _hotKeySubscription = _inputChannel.SubscribeKeyDown(_keyCode, KeyDown);
        }

        private void UpdateUI()
        {
            var color = _itemImage.color;
            
            if (_currentInventoryItem == null)
            {
                _itemImage.sprite = null;
                _itemImage.color = new Color(color.r, color.g, color.b, 0);
                _amountText.SetText("");
            }
            else
            {
                _itemImage.sprite = _currentInventoryItem.ItemMeta.InventoryItemSprite;
                _itemImage.color = new Color(color.r, color.g, color.b, 255);
                _amountText.SetText(_currentInventoryItem.Amount.ToString());    
            }
        }

        private void KeyDown()
        {
            _inventoryChannel.OnUseItemRequest(_currentInventoryItem, _player);
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

        public void HandleRightClick()
        {
            if (_currentInventoryItem == null) return;
            
            _currentInventoryItem = null;
            _hotKeySubscription.Unsubscribe();
            UpdateUI();
        }
    }
}