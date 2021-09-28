using System;
using System.Linq;
using Entity.Player;
using ScriptableObjects.GameConfiguration;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using ScriptableObjects.Quests;
using ScriptableObjects.Traits;
using UnityEditor;
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

        [Header("Scenes")] 
        [SerializeField] private ScenesList _sceneList;

        [Header("Phase 1 - Karf")] [SerializeField]
        private WeaponItemMeta _primaryWeapon;
        
        [ContextMenu("Phases/Phase 0 - Tutorial")]
        private void Phase0()
        {
            ResetPlayerTraits();
            ResetQuests();
            ResetInventory();
            ResetEquipment();
            
            _sceneList.DefaultFirstScene = _sceneList.Scenes.FirstOrDefault(x => x.ReplacementPhrase == "{TUTORIAL}");
        }
        
        [ContextMenu("Phases/Phase 1 - Karf")]
        private void Phase1()
        {
            Phase0();
            _playerTraits.Level = 3;
            _playerTraits.Strength = 15;

            _playerEquipment.PrimaryWeapon = _primaryWeapon;
            _sceneList.DefaultFirstScene = _sceneList.Scenes.FirstOrDefault(x => x.ReplacementPhrase == "{KARF}");
        }

        [ContextMenu("Specific/Reset Player Traits")]
        private void ResetPlayerTraits()
        {
            _playerTraits.Reset();
            _playerTraits._levelConfiguration = _levelConfiguration;
        }

        [ContextMenu("Specific/Reset Quests")]
        private void ResetQuests()
        {
            _questsList.ResetQuests();
        }

        [ContextMenu("Specific/Reset Inventory")]
        private void ResetInventory()
        {
            _playerInventory.OwnedItems.Clear();
            _playerInventory.CurrencyItem.Amount = 0;
        }

        [ContextMenu("Specific/Reset Equipment")]
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