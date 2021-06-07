using HeroEditor.Common;
using HeroEditor.Common.Enums;
using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
    [CreateAssetMenu(fileName = "Inventory Equipment Meta", menuName = "Inventory/Equipment", order = 2)]
    public class EquipmentItemMeta : InventoryItemMeta
    {
        public SpriteGroupEntry Item;
        public EquipmentPart Part;
        public override void Use(Entity.Player.Player player)
        {
            player.Apearence.EquipItem(this);
        }
    }
}