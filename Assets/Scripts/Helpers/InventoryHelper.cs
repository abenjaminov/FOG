using System;
using ScriptableObjects;
using ScriptableObjects.Inventory;
using Random = UnityEngine.Random;

namespace Helpers
{
    public static class InventoryHelper
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