using JetBrains.Annotations;
using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
    [CreateAssetMenu(fileName = "Currency Meta", menuName = "Inventory/Currency", order = 1)]
    public class CurrencyItemMeta : InventoryItemMeta
    {
        public override bool Use(Entity.Player.Player player)
        {
            return true;
        }

        public override bool IsConsumable()
        {
            return false;
        }

        public override string GetAmountChangedText(int amountAdded)
        {
            if (amountAdded < 0)
            {
                return $"Used {Mathf.Abs(amountAdded)} coins";
            }

            return base.GetAmountChangedText(amountAdded);
        }
    }
}