using HeroEditor.Common;
using HeroEditor.Common.Enums;
using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
    [CreateAssetMenu(fileName = "Drop Item Meta", menuName = "Inventory/Drop")]
    public class DropItemMeta : InventoryItemMeta
    {
        public override bool Use(Entity.Player.Player player)
        {
            return false;
        }

        public override bool IsConsumable()
        {
            return false;
        }
    }
}