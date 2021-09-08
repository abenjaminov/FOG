using System;
using Entity.Player;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using ScriptableObjects.Quests;
using ScriptableObjects.Traits;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Reset Manager", menuName = "Reset Manager", order = 0)]
    public class ResetManager : ScriptableObject
    {
        [Header("Player Traits")]
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private LevelConfiguration _levelConfiguration;

        [Header("Quests")]
        [SerializeField] private QuestsList _questsList;

        [Header("Inventory")] 
        [SerializeField] private Inventory.Inventory _playerInventory;

        [Header("Player Equipment")] 
        [SerializeField] private PlayerEquipment _playerEquipment;
        [SerializeField] private EquipmentItemMeta _defaultArmourMeta;
        
        [ContextMenu("Reset All Objects")]
        private void ResetObjects()
        {
            ResetPlayerTraits();
            ResetQuests();
            ResetInventory();
            ResetEquipment();
        }

        [ContextMenu("Reset Player Traits")]
        private void ResetPlayerTraits()
        {
            _playerTraits.Reset();
            _playerTraits._levelConfiguration = _levelConfiguration;
        }

        [ContextMenu("Reset Quests")]
        private void ResetQuests()
        {
            _questsList.ResetQuests();
        }

        [ContextMenu("Reset Inventory")]
        private void ResetInventory()
        {
            _playerInventory.OwnedItems.Clear();
            _playerInventory.CurrencyItem.Amount = 0;
        }

        [ContextMenu("Reset Equipment")]
        private void ResetEquipment()
        {
            _playerEquipment.Armour = _defaultArmourMeta;
            _playerEquipment.Cape = null;
            _playerEquipment.Helmet = null;
            _playerEquipment.PrimaryWeapon = null;
            _playerEquipment.SecondaryWeapon = null;
        }
    }
}