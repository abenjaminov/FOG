using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;

namespace UI.Shop
{
    public class ShopItemInfo
    {
        public InventoryItem InventoryItem;
        public InventoryItemMeta ItemMeta;

        public string Name => ItemMeta.Name;
        public int Price;
    }
}