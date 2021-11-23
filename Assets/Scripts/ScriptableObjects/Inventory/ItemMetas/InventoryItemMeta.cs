using JetBrains.Annotations;
using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
    public abstract class InventoryItemMeta : ScriptableObject
    {
        public string Id;
        public string Name;
        public Sprite ItemSprite;
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