using HeroEditor.Common;
using HeroEditor.Common.Enums;
using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Equipment Meta", menuName = "Inventory/Equipment", order = 1)]
    public class EquipmentItemMeta : InventoryItemMeta
    {
        public SpriteGroupEntry Item;
        public EquipmentPart Part;
        public override void Use(Entity.Player.Player player)
        {
            player.EquipItem(this);
        }
    }
}