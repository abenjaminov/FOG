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

        public void SetItem(InventoryItemMeta item, CurrencyItemMeta currencyItemMeta)
        {
            Icon.sprite = item.InventoryItemSprite;
            NameText.SetText(item.Name);
            PriceText.SetText(item.PriceInShop + " " + currencyItemMeta.Name);

            Item = item;
        }

        public void HandleDoubleClick()
        {
            ShopItemDoubleClicked?.Invoke(this);
        }
    }
}