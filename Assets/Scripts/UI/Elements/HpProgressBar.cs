using System;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Traits;
using UnityEngine;

namespace UI
{
    public class HpProgressBar : ProgressBar
    {
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private PlayerChannel _playerChannel;
        
        private void Awake()
        {
            MaxValue = _playerTraits.MaxHealth;
            CurrentValue = _playerTraits.GetCurrentHealth();
            
            _playerTraits.HealthChangedEvent += UpdateUI;
            _playerChannel.LevelUpEvent += UpdateUI;
            _playerChannel.TraitsChangedEvent += UpdateUI;
            
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
            _playerChannel.LevelUpEvent -= UpdateUI;
            _playerChannel.TraitsChangedEvent -= UpdateUI;
        }
    }
}