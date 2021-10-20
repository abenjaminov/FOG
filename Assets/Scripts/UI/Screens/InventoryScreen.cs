using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.CommonScripts;
using Helpers;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using TMPro;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class InventoryScreen : GUIScreen
    {
        private Entity.Player.Player _player;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private DragChannel _dragChannel;
        [SerializeField] private EquipmentDetailsPanel _equipmentDetailsPanel;

        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private PagingComponent _pagingComponent;
        private List<InventoryItemView> _itemViews = new List<InventoryItemView>();

        private int _itemsPerPage;
        private int _numOfPages;

        protected override void Awake()
        {
            _itemViews = GetComponentsInChildren<InventoryItemView>().ToList();

            _pagingComponent.CurrentPage = 1;
            _itemsPerPage = _itemViews.Count;

            _pagingComponent.NextPageClickedEvent += UpdateUI;
            _pagingComponent.PreviousPageClickedEvent += UpdateUI;

            foreach (var itemView in _itemViews)
            {
                itemView.ItemViewDoubleClicked += ItemViewDoubleClicked;
                itemView.ItemViewMouseEnter += ItemViewMouseEnter;
                itemView.ItemViewMouseExit += ItemViewMouseExit;
                itemView.ItemViewSingleClicked += ItemViewSingleClicked;
                itemView.ItemViewRightClicked += ItemViewRightClicked;
            }
            
            base.Awake();
        }

        private void ItemViewRightClicked(EquipmentItemView itemView)
        {
            _inventoryChannel.OnDropItemRequest(itemView.InventoryItem);
        }

        private void ItemViewSingleClicked(EquipmentItemView itemView)
        {
            _dragChannel.OnDragStart(itemView);
        }

        private void ItemViewMouseExit(EquipmentItemView itemView)
        {
            _equipmentDetailsPanel.HideItemDetails();
        }

        private void ItemViewMouseEnter(EquipmentItemView itemView)
        {
            if (itemView.ItemMeta == null) return;
            
            _equipmentDetailsPanel.ShowItemDetails(itemView.ItemMeta, itemView.GetBottomLeftCorner());
        }

        void Start()
        {
            _player = FindObjectOfType<Entity.Player.Player>();
            _inventoryChannel.ItemAmountChangedSilentEvent += ItemAddedSilentEvent;
        }

        private void ItemViewDoubleClicked(EquipmentItemView item)
        {
            OnItemDoubleClicked(_itemViews.IndexOf(item as InventoryItemView));
        }

        private void OnDestroy()
        {
            _inventoryChannel.ItemAmountChangedSilentEvent -= ItemAddedSilentEvent;
            _pagingComponent.NextPageClickedEvent -= UpdateUI;
            _pagingComponent.PreviousPageClickedEvent -= UpdateUI;
            foreach (var itemView in _itemViews)
            {
                itemView.ItemViewDoubleClicked -= ItemViewDoubleClicked;
                itemView.ItemViewMouseEnter -= ItemViewMouseEnter;
                itemView.ItemViewMouseExit -= ItemViewMouseExit;
                itemView.ItemViewSingleClicked -= ItemViewSingleClicked;
                itemView.ItemViewRightClicked -= ItemViewRightClicked;
            }
        }

        public override KeyCode GetActivationKey()
        {
            return _keyboardConfiguration.OpenInventoryScreen;
        }

        protected override void UpdateUI()
        {
            _pagingComponent.NumberOfPages = (_inventory.OwnedItems.Count / _itemsPerPage) + 1;
            
            _pagingComponent.UpdateUI();

            _coinText.SetText(StringHelper.NumberToString(_inventory.CurrencyItem.Amount));

            var startIndex = (_pagingComponent.CurrentPage - 1) * _itemsPerPage;
            var endIndex = startIndex + _itemsPerPage;

            for (int i = startIndex; i < endIndex; i++)
            {
                var viewIndex = i - startIndex;
                var itemView = _itemViews[viewIndex];
                var color = itemView.ItemSprite.color;
                
                if (_inventory.OwnedItems.Count > i)
                {
                    itemView.ItemSprite.sprite = _inventory.OwnedItems[i].ItemMeta.InventoryItemSprite;
                    if (_inventory.OwnedItems[i].ItemMeta is EquipmentItemMeta)
                    {
                        itemView.AmountText.SetText("");
                    }
                    else
                    {
                        itemView.AmountText.SetText(_inventory.OwnedItems[i].Amount.ToString());    
                    }
                    
                    itemView.ItemSprite.color = new Color(color.r, color.g, color.b, 255);
                    itemView.ItemMeta = _inventory.OwnedItems[i].ItemMeta;
                    itemView.InventoryItem = _inventory.OwnedItems[i];
                }
                else
                {
                    itemView.ItemSprite.sprite = null;
                    itemView.ItemSprite.color = new Color(color.r, color.g, color.b, 0);
                    itemView.AmountText.SetText("");
                    itemView.ItemMeta = null;
                    itemView.InventoryItem = null;
                }
            }
        }

        private void ItemAddedSilentEvent(InventoryItem item, int amountAdded)
        {
            UpdateUI();
        }

        private void OnItemDoubleClicked(int itemIndex)
        {
            if (itemIndex >= _inventory.OwnedItems.Count) return;
            
            _inventoryChannel.OnUseItemRequest(_inventory.OwnedItems[itemIndex], _player);
            UpdateUI();
        }
        
        public override void ToggleView()
        {
            base.ToggleView();

            if (!IsOpen)
            {
                _equipmentDetailsPanel.HideItemDetails();
            }
        }
    }
}
