using System;
using System.Collections.Generic;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace ScriptableObjects.Quests.QuestRewards
{
    [Serializable]
    public class InventoryItemQuestReward : QuestReward
    {
        [SerializeField] private Inventory.Inventory _playerInventory;
        [SerializeField] private List<ItemReward> _itemRewards;
        
        public override bool Reward()
        {
            _itemRewards.ForEach(x =>
            {
                _playerInventory.AddItem(x.ItemMeta, x.Amount);    
            });
            
            return true;
        }
    }

    [Serializable]
    public class ItemReward
    {
        [SerializeField] internal InventoryItemMeta ItemMeta;
        [SerializeField] internal int Amount;
    }
}