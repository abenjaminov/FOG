using System;
using System.Collections.Generic;
using Helpers;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class InventoryScreen : GUIScreen
    {
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventoryChannel _invChannel;
        [SerializeField] private Transform _firstItemTransform;
        
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private List<InventoryItemView> _itemViews = new List<InventoryItemView>();

        // Start is called before the first frame update
        void Start()
        {
            _invChannel.ItemAddedEvent += ItemAddedEvent;
        }

        private void ToggleView()
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }

        protected override KeyCode GetActivationKey()
        {
            return KeyCode.I;
        }

        protected override void UpdateUI()
        {
            _coinText.SetText(MoneyToString(_inventory.CurrencyItem.Amount));
            
            for (int i = 0; i < _itemViews.Count; i++)
            {
                var color = _itemViews[i].ItemSprite.color;
                    
                if (_inventory.OwnedItems.Count > i)
                {
                    _itemViews[i].ItemSprite.sprite = _inventory.OwnedItems[i].ItemMeta.InventoryItemSprite;
                    _itemViews[i].AmountText.SetText(_inventory.OwnedItems[i].Amount.ToString());
                    _itemViews[i].ItemSprite.color = new Color(color.r, color.g, color.b, 255);
                }
                else
                {
                    _itemViews[i].ItemSprite.sprite = null;
                    _itemViews[i].ItemSprite.color = new Color(color.r, color.g, color.b, 0);
                    _itemViews[i].AmountText.SetText("");
                }
            }
        }

        private void ItemAddedEvent(InventoryItem itemAddition, InventoryItem item)
        {
            UpdateUI();
        }

        private string MoneyToString(int money)
        {
            var result = "";
            var currentPart = "";
            
            currentPart = (money % 1000).ToString();
            
            while (money > 0)
            {
                var zerosToAdd = 3 - currentPart.Length;
                var zeros = StringHelper.MuiltiplyString("0", zerosToAdd);
                currentPart = zeros + currentPart;
                result = currentPart + result;
                result = "," + result;
                
                money /= 1000;
                currentPart = (money % 1000).ToString();
            }

            result = result.TrimStart(',', '0');
            
            return result;
        }
    }

    [Serializable]
    class InventoryItemView
    {
        public Image ItemSprite;
        public TextMeshProUGUI AmountText;
    }
}
