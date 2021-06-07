using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
    [CreateAssetMenu(fileName = "Inventory Equipment Meta", menuName = "Inventory/Equipment", order = 4)]
    public class WeaponItemMeta : InventoryItemMeta
    {
        public override void Use(Entity.Player.Player player)
        {
            // TODO : Use
        }
    }
}