using System;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ItemReward
    {
        [SerializeField] internal InventoryItemMeta ItemMeta;
        [SerializeField] internal int Amount;
    }
}