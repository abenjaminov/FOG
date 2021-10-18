using System;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using UI.Behaviours;
using UI.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Screens
{
    public class EquipmentItemView : MonoBehaviour, 
        IDoubleClickHandler, 
        IPointerEnterHandler, 
        IPointerExitHandler, 
        IDraggable, 
        ISingleClickHandler
    {
        public Image ItemSprite;
        public InventoryItemMeta ItemMeta;
        public InventoryItem InventoryItem;
        public Action<EquipmentItemView> ItemViewDoubleClicked;
        public Action<EquipmentItemView> ItemViewSingleClicked;
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

        public Sprite GetDragImage()
        {
            return ItemSprite.sprite;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void HandleClick()
        {
            ItemViewSingleClicked?.Invoke(this);
        }
    }
}