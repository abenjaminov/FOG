using System.Collections.Generic;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace ScriptableObjects.Shops
{
    [CreateAssetMenu(fileName = "Shop", menuName = "Shops/Shop")]
    public class ShopInfo : ScriptableObject
    {
        public List<InventoryItemMeta> ShopItems;
    }
}