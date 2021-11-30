using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.CommonScripts;
using Helpers;
using HeroEditor.Common.Enums;
using Platformer;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.GameConfiguration;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using ScriptableObjects.Traits;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Screens
{
    public class TraitsScreen : GUIScreen
    {
        [SerializeField] private int _removeTraitBasicCost;
        [SerializeField] private PlayerChannel _playerChannel;
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private GameChannel _gameChannel;
        [SerializeField] private PlayerEquipment _playerEquipment;
        [SerializeField] private LevelConfiguration _levelConfiguration;

        [SerializeField] private List<GameObject> _addButtons;

        [SerializeField] private Button _removeStr;
        [SerializeField] private Button _removeDex;
        [SerializeField] private Button _removeInt;

        [SerializeField] private TextMeshProUGUI _dexText;
        [SerializeField] private TextMeshProUGUI _strText;
        [SerializeField] private TextMeshProUGUI _defText;
        [SerializeField] private TextMeshProUGUI _intText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _pointsText;
        [SerializeField] private TextMeshProUGUI _expText;
        [SerializeField] private TextMeshProUGUI _damageText;

        private int previousExp = -1;

        protected override void Awake()
        {
            base.Awake();
            
            _playerChannel.WeaponChangedEvent += WeaponChangedEvent;
            _playerChannel.ItemEquippedEvent += ItemEquippedEvent;
            _playerChannel.ItemUnEquippedEvent += ItemUnEquippedEvent;
        }

        private void ItemUnEquippedEvent(EquipmentItemMeta arg0, EquipmentPart arg1)
        {
            UpdateUI();
        }

        private void ItemEquippedEvent(EquipmentItemMeta arg0, EquipmentPart arg1)
        {
            UpdateUI();
        }

        private void WeaponChangedEvent(WeaponItemMeta arg0, EquipmentPart arg1)
        {
            UpdateUI();
        }

        public override KeyCode GetActivationKey()
        {
            return _keyboardConfiguration.OpenTraitsScreen;
        }

        protected override void UpdateUI()
        {
            _dexText.SetText(_playerTraits.Dexterity.ToString());
            _strText.SetText(_playerTraits.Strength.ToString());
            _intText.SetText(_playerTraits.Intelligence.ToString());
            _defText.SetText(_playerEquipment.GetCombinedDefense().ToString());

            if (_playerEquipment.PrimaryWeapon == null)
            {
                _damageText.SetText("Equip a weapon");
            }
            else
            {
                _damageText.SetText(TraitsHelper.GetMinPlayerDamage(_playerTraits, _playerEquipment) + 
                                    " - " + TraitsHelper.GetMaxPlayerDamage(_playerTraits, _playerEquipment));    
            }
        
            SetExp();

            previousExp = _playerTraits.ResistancePointsGained;
        
            if (_playerTraits.PointsLeft == 0)
            {
                foreach (var button in _addButtons)
                {
                    button.SetActive(false);
                }
            }
            else
            {
                foreach (var button in _addButtons)
                {
                    button.SetActive(true);
                }
            }

            _removeStr.SetActive(_playerTraits.Strength > 5);
            _removeDex.SetActive(_playerTraits.Dexterity > 5);
            _removeInt.SetActive(_playerTraits.Intelligence > 5);
        }

        public override void ToggleView()
        {
            base.ToggleView();
            
            UpdateUI();
        }

        private void SetExp()
        {
            _levelText.SetText(_playerTraits.Level.ToString());
            _pointsText.SetText(_playerTraits.PointsLeft.ToString());
        
            if (previousExp == _playerTraits.ResistancePointsGained) return;
        
            var currentLevel = _levelConfiguration.Levels.FirstOrDefault(x => x.Order == _playerTraits.Level);
            var expText = currentLevel != null
                ? _playerTraits.ResistancePointsGained.ToString() + " / " + (currentLevel.ExpForNextLevel)
                : "Max Level";

            _expText.SetText(expText);
        }

        private void Update()
        {
            SetExp();
        }

        public void AddStrength()
        {
            _playerTraits.PointsLeft--;
            _playerTraits.Strength++;
            UpdateUI();
        }
        
        public void RemoveStrength()
        {
            RemoveTrait(() =>
            {
                _playerTraits.PointsLeft++;
                _playerTraits.Strength--;
            });
        }

        public void AddDexterity()
        {
            _playerTraits.PointsLeft--;
            _playerTraits.Dexterity++;
            UpdateUI();
        }
        
        public void RemoveDexterity()
        {
            RemoveTrait(() =>
            {
                _playerTraits.PointsLeft++;
                _playerTraits.Dexterity--;
            });
        }
        
        public void AddIntelligence()
        {
            _playerTraits.PointsLeft--;
            _playerTraits.Intelligence++;
            UpdateUI();
        }
        
        public void RemoveIntelligence()
        {
            RemoveTrait(() =>
            {
                _playerTraits.PointsLeft++;
                _playerTraits.Intelligence--;
            });
        }

        private void OnDestroy()
        {
            _playerChannel.WeaponChangedEvent -= WeaponChangedEvent;
            _playerChannel.ItemEquippedEvent -= ItemEquippedEvent;
            _playerChannel.ItemUnEquippedEvent -= ItemUnEquippedEvent;
        }

        private void RemoveTrait(Action removeAction)
        {
            var cost = _removeTraitBasicCost * _playerTraits.Level;
            var usedCoins = _inventoryChannel.UseCoinsRequest(cost);

            if (!usedCoins)
            {
                _gameChannel.OnGameErrorEvent($"Insufficient funds ({cost})");
                return;
            }
            
            removeAction();
            UpdateUI();
        }
    }
}
