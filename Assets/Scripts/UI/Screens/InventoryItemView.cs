using System;
using ScriptableObjects.Inventory.ItemMetas;
using TMPro;
using UI.Mouse;

namespace UI.Screens
{
    class InventoryItemView : EquipmentItemView
    {
        public TextMeshProUGUI AmountText;
        
        public bool IsUsable()
        {
            return ItemMeta is PotionItemMeta;
        }
    }
}