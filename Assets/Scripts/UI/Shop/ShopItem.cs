using System;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using TMPro;
using UI.Mouse;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopItem : MonoBehaviour, IDoubleClickHandler, IPointerEnterHandler, 
        IPointerExitHandler
    {
        [SerializeField] private Image Icon;
        [SerializeField] private TextMeshProUGUI NameText;
        [SerializeField] private TextMeshProUGUI AdditionalText;
        [SerializeField] private TextMeshProUGUI PriceText;
        
        [HideInInspector] public InventoryItemMeta ItemMeta;
        [HideInInspector] public InventoryItem Item;
        
        [HideInInspector] public UnityAction<ShopItem> ShopItemDoubleClicked;
        [HideInInspector] public UnityAction<ShopItem> ShopItemMouseEnter;
        [HideInInspector] public UnityAction<ShopItem> ShopItemMouseExit;

        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public void SetItem(InventoryItem item)
        {
            Item = item;
        }
        
        public void SetItemMeta(InventoryItemMeta item)
        {
            ItemMeta = item;
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
            AdditionalText.SetText(additionalLine);
        }

        public void HandleDoubleClick()
        {
            ShopItemDoubleClicked?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShopItemMouseEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ShopItemMouseExit?.Invoke(this);
        }
        
        public Vector2 GetBottomLeftCorner()
        {
            return new Vector2(_collider.bounds.max.x, _collider.bounds.max.y);
        }
    }
}