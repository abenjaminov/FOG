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
    }
}