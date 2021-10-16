using System;
using ScriptableObjects;
using ScriptableObjects.Traits;
using UnityEngine;

namespace UI
{
    public class HpProgressBar : ProgressBar
    {
        [SerializeField] private PlayerTraits _playerTraits;
        
        private void Awake()
        {
            MaxValue = _playerTraits.MaxHealth;
            CurrentValue = _playerTraits.GetCurrentHealth();
            
            _playerTraits.HealthChangedEvent += UpdateUI;
            _playerTraits.LevelUpEvent += UpdateUI;
            _playerTraits.TraitsChangedEvent += UpdateUI;
            
            UpdateUI();
        }

        protected override void UpdateUI()
        {
            MaxValue = _playerTraits.MaxHealth;
            CurrentValue = _playerTraits.GetCurrentHealth();
            base.UpdateUI();
        }

        private void OnDestroy()
        {
            _playerTraits.HealthChangedEvent -= UpdateUI;
            _playerTraits.LevelUpEvent -= UpdateUI;
            _playerTraits.TraitsChangedEvent -= UpdateUI;
        }
    }
}