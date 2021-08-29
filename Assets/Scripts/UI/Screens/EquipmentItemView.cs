using System;
using ScriptableObjects.Inventory.ItemMetas;
using UI.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Screens
{
    public class EquipmentItemView : MonoBehaviour, IDoubleClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Image ItemSprite;
        public InventoryItemMeta ItemMeta;
        public Action<EquipmentItemView> ItemViewDoubleClicked;
        public Action<EquipmentItemView> ItemViewMouseEnter;
        public Action<EquipmentItemView> ItemViewMouseExit;
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public void HandleDoubleClick()
        {
            ItemViewDoubleClicked?.Invoke(this);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            ItemViewMouseEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ItemViewMouseExit?.Invoke(this);
        }

        public Vector2 GetBottomLeftCorner()
        {
            return new Vector2(_collider.bounds.max.x, _collider.bounds.max.y);
        }
    }
}