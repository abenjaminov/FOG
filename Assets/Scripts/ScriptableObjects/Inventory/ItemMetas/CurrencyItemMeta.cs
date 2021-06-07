using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
    [CreateAssetMenu(fileName = "Currency Meta", menuName = "Inventory/Currency", order = 1)]
    public class CurrencyItemMeta : InventoryItemMeta
    {
        public override void Use(Entity.Player.Player player)
        { }
    }
}