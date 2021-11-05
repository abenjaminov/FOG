using System;
using System.Collections.Generic;
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
        [SerializeField] private EquipmentItemMeta _helmetMeta;

        [Header("Scenes")] 
        [SerializeField] private ScenesList _sceneList;

        [Header("Phase 1 - Karf")] 
        [SerializeField] private WeaponItemMeta _primaryWeapon;
        [SerializeField] private PotionItemMeta _hpPotion;

        [Header("Phase 2")] 
        [SerializeField] private List<Quest> _phase2QuestsToComplete;
        
        [Header("Phase 3")] 
        [SerializeField] private List<Quest> _phase3QuestsToComplete;

        private void ShowPhaseResetMessage(string text)
        {
            EditorUtility.DisplayDialog("Phase reset", text, "Nice");
        }
        
        [ContextMenu("Phases/Phase 0 - Tutorial")]
        private void Phase0()
        {
            Phase0Content();
            
            ShowPhaseResetMessage("Phase 0 - Tutorial beggining");
        }

        private void Phase0Content()
        {
            ResetPlayerTraits();
            ResetQuests();
            ResetInventory();
            ResetEquipment();

            _playerInventory.AddItemSilent(_hpPotion, 50);
            _sceneList.DefaultFirstScene = _sceneList.Scenes.FirstOrDefault(x => x.ReplacementPhrase == "{TUTORIAL}");
        }

        private void Phase1Content()
        {
            Phase0Content();
            _playerTraits.Level = 3;
            _playerTraits.Strength = 15;

            _playerEquipment.PrimaryWeapon = _primaryWeapon;
            _sceneList.DefaultFirstScene = _sceneList.Scenes.FirstOrDefault(x => x.ReplacementPhrase == "{KARF}");
        }
        
        [ContextMenu("Phases/Phase 1 - Karf")]
        private void Phase1()
        {
            Phase1Content();
            
            ShowPhaseResetMessage("Phase 1 - Right after tutorial ended and moved to Karf");
        }

        private void Phase2Content()
        {
            Phase1Content();
            _playerTraits.Level = 4;
            _playerTraits.Strength = 20;

            foreach (var quest in _phase2QuestsToComplete)
            {
                quest.State = QuestState.Completed;
            }
            
            _playerEquipment.PrimaryWeapon = _primaryWeapon;
            _playerEquipment.Helmet = _helmetMeta;
            _sceneList.DefaultFirstScene = _sceneList.Scenes.FirstOrDefault(x => x.ReplacementPhrase == "{KARF}");
        }
        
        [ContextMenu("Phases/Phase 2 - Karf Level 4")]
        private void Phase2()
        {
            Phase2Content();
            
            ShowPhaseResetMessage("Phase 2 - Level 4");
        }

        private void Phase3Content()
        {
            Phase2Content();
            
            foreach (var quest in _phase3QuestsToComplete)
            {
                quest.State = QuestState.Completed;
            }
            
            _sceneList.DefaultFirstScene = _sceneList.Scenes.FirstOrDefault(x => x.ReplacementPhrase == "{MAP5}");
        }
        
        [ContextMenu("Phases/Phase 3 -Map 5 Level 4")]
        private void Phase3()
        {
            Phase3Content();
            
            ShowPhaseResetMessage("Phase 3 - Level 4");
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