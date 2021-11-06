using System;
using ScriptableObjects.Channels;
using ScriptableObjects.Traits;
using UnityEngine;

namespace UI.Elements
{
    public class MonsterStateResistanceProgressBar : ProgressBar
    {
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private PlayerChannel _playerChannel;
        
        private void Awake()
        {
            MaxValue = PlayerTraits.MaxMonsterStateResistance;
            
            SetInitialCurrentValue(_playerTraits.MonsterStateResistance);

            _playerChannel.MonsterResistanceChangedEvent += MonsterResistanceChangedEvent;
            
            UpdateUI();
        }

        private void MonsterResistanceChangedEvent()
        {
            CurrentValue = _playerTraits.MonsterStateResistance;
        }

        private void OnDestroy()
        {
            _playerChannel.MonsterResistanceChangedEvent -= MonsterResistanceChangedEvent;
        }
    }
}