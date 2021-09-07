using ScriptableObjects.Traits;
using UnityEngine;

namespace UI.Elements
{
    public class MonsterStateResistanceProgressBar : ProgressBar
    {
        [SerializeField] private PlayerTraits _playerTraits;
        
        private void Awake()
        {
            MaxValue = PlayerTraits.MaxMonsterStateResistance;
            
            SetInitialCurrentValue(_playerTraits.MonsterStateResistance);
            //CurrentValue = _playerTraits.MonsterStateResistance;
            
            _playerTraits.MonsterResistanceChangedEvent += MonsterResistanceChangedEvent;
            
            UpdateUI();
        }

        private void MonsterResistanceChangedEvent()
        {
            CurrentValue = _playerTraits.MonsterStateResistance;
        }
    }
}