﻿using HeroEditor.Common;
using HeroEditor.Common.Enums;
using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
    [CreateAssetMenu(fileName = "Inventory Equipment Meta", menuName = "Inventory/Equipment", order = 2)]
    public class EquipmentItemMeta : InventoryItemMeta
    {
        public SpriteGroupEntry Item;
        public EquipmentPart Part;
        public int RequiredLevel;
        
        public override bool Use(Entity.Player.Player player)
        {
            if (player.Traits.Level < RequiredLevel) return false;
            
            player.Apearence.EquipItem(this);

            return true;
        }
    }
}