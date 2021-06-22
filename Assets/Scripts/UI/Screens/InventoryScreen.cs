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
        private Entity.Player.Player _player;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventoryChannel _invChannel;

        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private List<InventoryItemView> _itemViews = new List<InventoryItemView>();

        // Start is called before the first frame update
        void Start()
        {
            _player = FindObjectOfType<Entity.Player.Player>();
            _invChannel.ItemAddedEvent += ItemAddedEvent;
        }

        public override KeyCode GetActivationKey()
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

        public void OnItemDoubleClicked(int itemIndex)
        {
            _inventory.UseItem(_player, _inventory.OwnedItems[itemIndex]);
            UpdateUI();
        }
    }

    [Serializable]
    class InventoryItemView
    {
        public Image ItemSprite;
        public TextMeshProUGUI AmountText;
    }
}
