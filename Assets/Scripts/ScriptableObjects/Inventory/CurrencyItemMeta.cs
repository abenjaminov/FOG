using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "Currency Meta", menuName = "Inventory/Currency", order = 1)]
    public class CurrencyItemMeta : InventoryItemMeta
    {
        public override void Use(Entity.Player.Player player)
        { }
    }
}