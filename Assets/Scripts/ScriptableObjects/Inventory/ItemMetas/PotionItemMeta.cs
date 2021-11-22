using Platformer.ScriptableObjects.Enums;
using ScriptableObjects.Channels;
using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
    [CreateAssetMenu(fileName = "Potion Item Meta", menuName = "Inventory/Potion", order = 3)]
    public class PotionItemMeta : InventoryItemMeta
    {
        public PotionType PotionType;
        public int GainAmount;
        [SerializeField] private InventoryChannel _inventoryChannel;

        public override bool Use(Entity.Player.Player player)
        {
            var result = false;
            switch (PotionType)
            {
                case PotionType.Hp:
                    player.PlayerTraits.ChangeCurrentHealth(GainAmount);
                    result = true;
                    break;
                case PotionType.MonsterResistance:
                    player.PlayerTraits.MonsterStateResistance += GainAmount;
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }

            if(result)
                _inventoryChannel.OnUsePotion();
            
            return result;
        }

        public override bool IsConsumable()
        {
            return true;
        }
    }
}