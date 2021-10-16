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
            switch (PotionType)
            {
                case PotionType.Hp:
                    player.PlayerTraits.ChangeCurrentHealth(GainAmount);
                    return true;
                case PotionType.MonsterResistance:
                    player.PlayerTraits.MonsterStateResistance += GainAmount;
                    return true;
                default:
                    return false;
            }
        }
    }
}