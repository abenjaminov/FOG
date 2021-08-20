using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Item List", menuName = "Game Configuration/Inventory Items")]
    public class InventoryItemsList : ScriptableObject
    {
        [Header("Currency")]
        [SerializeField] private CurrencyItemMeta _currency;
        
        [Header("Armour")]
        [SerializeField] private List<EquipmentItemMeta> _armours;
        [SerializeField] private List<EquipmentItemMeta> _helmets;
        
        [Header("Weapons")]
        [SerializeField] private List<WeaponItemMeta> _bows;
        [SerializeField] private List<WeaponItemMeta> _melee1Hand;
        [SerializeField] private List<WeaponItemMeta> _staffs;
        [SerializeField] private List<PotionItemMeta> _potions;

        private Dictionary<string, InventoryItemMeta> _allItems;

        private void OnEnable()
        {
            _allItems = _armours.AsEnumerable<InventoryItemMeta>().
                Concat(_bows).
                Concat(_melee1Hand).
                Concat(_staffs).
                Concat(_potions).
                Concat(_helmets).
                ToDictionary(x => x.Id, 
                          x => x);
            _allItems.Add(_currency.Id, _currency);
        }

        public InventoryItemMeta GetItemMetaById(string id)
        {
            if (!_allItems.ContainsKey(id)) return null;

            return _allItems[id];
        }
    }
}