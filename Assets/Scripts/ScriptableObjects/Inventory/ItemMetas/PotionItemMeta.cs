using Platformer.ScriptableObjects.Enums;
using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
    [CreateAssetMenu(fileName = "Potion Item Meta", menuName = "Inventory/Potion", order = 3)]
    public class PotionItemMeta : InventoryItemMeta
    {
        public PotionType PotionType;
        public int GainAmount;

        public override bool Use(Entity.Player.Player player)
        {
            if (PotionType == PotionType.Hp)
            {
                player.PlayerTraits.ChangeCurrentHealth(GainAmount);
                return true;
            }

            return false;
        }
    }
}