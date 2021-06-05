using HeroEditor.Common;
using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Equipment Meta", menuName = "Inventory/Equipment", order = 1)]
    public class EquipmentItemMeta : InventoryItemMeta
    {
        public SpriteGroupEntry Item;
    }
}