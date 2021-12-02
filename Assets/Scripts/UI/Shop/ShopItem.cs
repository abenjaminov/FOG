using ScriptableObjects.Inventory.ItemMetas;
using TMPro;
using UI.Mouse;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopItem : MonoBehaviour, IDoubleClickHandler
    {
        [SerializeField] private Image Icon;
        [SerializeField] private TextMeshProUGUI NameText;
        [SerializeField] private TextMeshProUGUI PriceText;
        public UnityAction<ShopItem> ShopItemDoubleClicked;
        public InventoryItemMeta Item;

        public void SetItem(InventoryItemMeta item)
        {
            Item = item;
        }

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }
        
        public void SetNameText(string nameText)
        {
            NameText.SetText(nameText);
        }

        public void SetPriceText(int price, InventoryItemMeta currencyItem)
        {
            PriceText.SetText(price + " " + currencyItem.Name);
        }

        public void SetAdditionalLineText(string additionalLine)
        {
            
        }

        public void HandleDoubleClick()
        {
            ShopItemDoubleClicked?.Invoke(this);
        }
    }
}