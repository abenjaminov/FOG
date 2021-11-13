using System;
using System.Collections.Generic;
using System.Linq;
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

namespace UI.Screens
{
    public class TraitsScreen : GUIScreen
    {
        [SerializeField] private PlayerChannel _playerChannel;
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private PlayerEquipment _playerEquipment;
        [SerializeField] private LevelConfiguration _levelConfiguration;

        [SerializeField] private List<GameObject> _addButtons;
    
        [SerializeField] private TextMeshProUGUI _dexText;
        [SerializeField] private TextMeshProUGUI _strText;
        [SerializeField] private TextMeshProUGUI _constText;
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
            _constText.SetText(_playerTraits.Constitution.ToString());

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
        
            var nextLevel = _levelConfiguration.Levels.FirstOrDefault(x => x.Order == _playerTraits.Level + 1);
            var expText = nextLevel != null
                ? _playerTraits.ResistancePointsGained.ToString() + " / " + (nextLevel.ExpForNextLevel)
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

        public void AddDexterity()
        {
            _playerTraits.PointsLeft--;
            _playerTraits.Dexterity++;
            UpdateUI();
        }
        
        public void AddIntelligence()
        {
            _playerTraits.PointsLeft--;
            _playerTraits.Intelligence++;
            UpdateUI();
        }
    
        public void AddConstitution()
        {
            _playerTraits.PointsLeft--;
            _playerTraits.Constitution++;
            UpdateUI();
        }

        private void OnDestroy()
        {
            _playerChannel.WeaponChangedEvent -= WeaponChangedEvent;
        }
    }
}
