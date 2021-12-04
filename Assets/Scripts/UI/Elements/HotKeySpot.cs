using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
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
        public KeyCode KeyCode;

        private KeySubscription _hotKeySubscription;
        [HideInInspector] public InventoryItem InventoryItem;

        private void Awake()
        {
            _inventoryChannel.ItemAmountChangedEvent += ItemAmountChangedEvent;
            _inventoryChannel.DropItemRequestEvent += DropItemRequestEvent;
        }

        private void ItemAmountChangedEvent(InventoryItem inventoryItem, int amount)
        {
            if (InventoryItem == null || InventoryItem.Id != inventoryItem.Id) return;

            if (inventoryItem.Amount == 0)
            {
                AssignItem(null);
            }
            else
            {
                UpdateUI();
            }
        }

        private void DropItemRequestEvent(InventoryItem inventoryItem)
        {
            if (InventoryItem == null || InventoryItem.Id != inventoryItem.Id) return;
            
            AssignItem(null);
        }
        
        public void DragDropped(IDraggable draggable)
        {
            if (!draggable.GetGameObject().TryGetComponent(typeof(InventoryItemView), out var component)) return;

            var item = ((InventoryItemView)component).InventoryItem;

            if (!item.ItemMeta.IsConsumable()) return;

            _inventoryChannel.OnHotkeyAssigned(KeyCode, item);
        }

        public void UnAssignItem()
        {
            AssignItemInternal(null);
        }
        
        public void AssignItem(InventoryItem item)
        {
            AssignItemInternal(item);
        }

        private void AssignItemInternal(InventoryItem item)
        {
            InventoryItem = item;

            if (InventoryItem == null)
            {
                _hotKeySubscription?.Unsubscribe();
            }
            else
            {
                _hotKeySubscription = _inputChannel.SubscribeKeyDown(KeyCode, KeyDown);
            }
            
            UpdateUI();
        }

        private void UpdateUI()
        {
            var color = _itemImage.color;
            
            if (InventoryItem == null)
            {
                _itemImage.sprite = null;
                _itemImage.color = new Color(color.r, color.g, color.b, 0);
                _amountText.SetText("");
            }
            else
            {
                _itemImage.sprite = InventoryItem.ItemMeta.InventoryItemSprite;
                _itemImage.color = new Color(color.r, color.g, color.b, 255);
                _amountText.SetText(InventoryItem.Amount.ToString());    
            }
        }

        private void KeyDown()
        {
            _inventoryChannel.OnUseItemRequest(InventoryItem, _player);
        }

        public bool IsEmpty()
        {
            return _itemImage.sprite == null;
        }

        private void OnDestroy()
        {
            _hotKeySubscription?.Unsubscribe();
            _inventoryChannel.ItemAmountChangedEvent -= ItemAmountChangedEvent;
            _inventoryChannel.DropItemRequestEvent -= DropItemRequestEvent;
        }

        public void HandleRightClick()
        {
            if (InventoryItem == null) return;
            
            AssignItemInternal(null);
        }
    }
}