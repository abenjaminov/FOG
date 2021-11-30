using System.Collections.Generic;
using System.IO;
using System.Linq;
using Helpers;
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
        [SerializeField] private bool _resetPersistence;
        
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

        private static ResetManager _manager;
        
        private void ShowPhaseResetMessage(string text)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog("Phase reset", text, "Nice");
            #endif
        }

        private static ResetManager GetManager()
        {
            if(_manager == null)
                _manager = AssetsHelper.GetAllAssets<ResetManager>().FirstOrDefault();

            return _manager;
        }
        
        [MenuItem("Reset/Phases/Phase 0 - Tutorial")]
        private static void ResetPhase0()
        {
            GetManager().Phase0();
        }
        
        [MenuItem("Reset/Phases/Phase 1 - Karf")]
        private static void ResetPhase1()
        {
            GetManager().Phase1();
        }
        
        [MenuItem("Reset/Phases/Phase 2 - Karf Level 4")]
        private static void ResetPhase2()
        {
            GetManager().Phase2();
        }
        
        [MenuItem("Reset/Phases/Phase 3 - Map 5 Level 5")]
        private static void ResetPhase3()
        {
            GetManager().Phase3();
        }

        [MenuItem("Reset/Reset Persistence")]
        private static void S_ResetPersistence()
        {
            GetManager().ResetPersistence();
        }
        
        [ContextMenu("Phases/Phase 0 - Tutorial")]
        public void Phase0()
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
            ResetPersistence();

            //_playerInventory.AddItemSilent(_hpPotion, 500);
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
        public void Phase1()
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
                if (quest.ItemReward.ApplyReward) quest.ItemReward.Reward();
            }
            
            _playerEquipment.PrimaryWeapon = _primaryWeapon;
            _playerEquipment.Helmet = _helmetMeta;
            _sceneList.DefaultFirstScene = _sceneList.Scenes.FirstOrDefault(x => x.ReplacementPhrase == "{KARF}");
        }
        
        [ContextMenu("Phases/Phase 2 - Karf Level 4")]
        public void Phase2()
        {
            Phase2Content();

            ShowPhaseResetMessage("Phase 2 - Level 4");
        }

        private void Phase3Content()
        {
            Phase2Content();
            
            _playerTraits.Level = 5;
            _playerTraits.Strength = 25;
            
            foreach (var quest in _phase3QuestsToComplete)
            {
                quest.State = QuestState.Completed;
                if (quest.ItemReward.ApplyReward) quest.ItemReward.Reward();
            }
            
            _sceneList.DefaultFirstScene = _sceneList.Scenes.FirstOrDefault(x => x.ReplacementPhrase == "{MAP5}");
        }
        
        [ContextMenu("Phases/Phase 3 - Map 5 Level 5")]
        public void Phase3()
        {
            Phase3Content();
            
            ShowPhaseResetMessage("Phase 3 - Level 4");
        }

        [ContextMenu("Specific/Reset Persistence")]
        public void ResetPersistence()
        {
            if (!_resetPersistence) return;
            
            var path = Application.persistentDataPath + "\\GamePersistence\\";

            if (!Directory.Exists(path)) return;
            
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            Directory.Delete(path, false);
        }
        
        [ContextMenu("Specific/Reset Player Traits")]
        public void ResetPlayerTraits()
        {
            _playerTraits.Reset();
            _playerTraits._levelConfiguration = _levelConfiguration;
        }

        [ContextMenu("Specific/Reset Quests")]
        public void ResetQuests()
        {
            _questsList.ResetQuests();
        }

        [ContextMenu("Specific/Reset Inventory")]
        public void ResetInventory()
        {
            _playerInventory.OwnedItems.Clear();
            _playerInventory.CurrencyItem.Amount = 0;
        }

        [ContextMenu("Specific/Reset Equipment")]
        public void ResetEquipment()
        {
            _playerEquipment.Armour = _defaultArmourMeta;
            _playerEquipment.Cape = null;
            _playerEquipment.Helmet = null;
            _playerEquipment.PrimaryWeapon = null;
        }
    }
}