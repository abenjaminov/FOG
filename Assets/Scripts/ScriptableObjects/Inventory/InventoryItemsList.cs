using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using HeroEditor.Common.Enums;
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
#if UNITY_EDITOR
            _currency = AssetsHelper.GetAllAssets<CurrencyItemMeta>().First();

            var equipment = AssetsHelper.GetAllAssets<EquipmentItemMeta>();
            _armours = equipment.Where(x => x.Part == EquipmentPart.Armor).ToList();
            _helmets = equipment.Where(x => x.Part == EquipmentPart.Helmet).ToList();
            
            var weapons = AssetsHelper.GetAllAssets<WeaponItemMeta>();
            _bows = weapons.Where(x => x.Part == EquipmentPart.Bow).ToList();
            _melee1Hand = weapons.Where(x => x.Part == EquipmentPart.MeleeWeapon1H && !x.IsStaff).ToList();
            _staffs = weapons.Where(x => x.Part == EquipmentPart.MeleeWeapon1H && x.IsStaff).ToList();
            
            _potions = AssetsHelper.GetAllAssets<PotionItemMeta>();
#endif
            
            _allItems = _armours.AsEnumerable<InventoryItemMeta>().
                Concat(_bows).
                Concat(_melee1Hand).
                Concat(_staffs).
                Concat(_potions).
                Concat(_helmets).
                ToDictionary(x => x.Id, 
                    x => x);
            _allItems.Add(_currency.Id, _currency);
            
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
            
            foreach (var item in _allItems.Values)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = Guid.NewGuid().ToString();
                    UnityEditor.EditorUtility.SetDirty(item);
                }
            }
            
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

        public InventoryItemMeta GetItemMetaById(string id)
        {
            if (!_allItems.ContainsKey(id)) return null;

            return _allItems[id];
        }
    }
}