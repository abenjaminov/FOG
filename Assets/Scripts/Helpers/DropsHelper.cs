using ScriptableObjects.Inventory.ItemMetas;
using ScriptableObjects.Traits;
using Random = UnityEngine.Random;

namespace Helpers
{
    public static class DropsHelper
    { 
        public static int GetDropAmount(InventoryItemMeta metaData, Traits dropperTraits)
        {
            if (!(metaData is CurrencyItemMeta))
            {
                return 1;
            }
            else
            {
                var randomCurrencyRange = Random.Range(0, 5) * dropperTraits.Level;

                var amount = dropperTraits.Level + randomCurrencyRange;

                return amount;
            }
        }
    }
}