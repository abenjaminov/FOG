using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
    public abstract class InventoryItemMeta : ScriptableObject
    {
        public string Id;
        public string Name;

        public bool IsQuestItem;
        
        [Header("Shop")] 
        public int PriceInShop;
        public int PriceForSale;
        
        [HideInInspector]
        public Sprite ItemSprite;
        [HideInInspector]
        public Sprite InventoryItemSprite;

        public abstract bool Use(Entity.Player.Player player);
        public abstract bool IsConsumable();

        public virtual string GetAmountChangedText(int amountAdded)
        {
            if (amountAdded <= 0) return "";

            var message = $"Gained {amountAdded} {Name}{(amountAdded > 1 ? 's' : ' ')}";

            return message;
        }
    }
}