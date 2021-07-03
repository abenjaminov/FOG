using System;
using ScriptableObjects;
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
            
            _playerTraits.HealthChangedEvent += HealthChangedEvent;
            
            UpdateUI();
        }

        private void HealthChangedEvent()
        {
            CurrentValue = _playerTraits.GetCurrentHealth();
            UpdateUI();
        }

        private void OnDestroy()
        {
            _playerTraits.HealthChangedEvent -= HealthChangedEvent;
        }
    }
}