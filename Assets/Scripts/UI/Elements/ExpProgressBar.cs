using System;
using ScriptableObjects;
using ScriptableObjects.Traits;
using UnityEngine;

namespace UI
{
    public class ExpProgressBar : ProgressBar
    {
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private LevelConfiguration _levelConfiguration;
        
        private void Awake()
        {
            UpdateMaxValue();

            CurrentValue = _playerTraits.ResistancePointsGained;
            
            _playerTraits.GainedExperienceEvent += GainedExperienceEvent;
            _playerTraits.LevelUpEvent += LevelUpEvent;
            
            UpdateUI();
        }

        private void OnDestroy()
        {
            _playerTraits.GainedExperienceEvent -= GainedExperienceEvent;
            _playerTraits.LevelUpEvent -= LevelUpEvent;
        }

        private void LevelUpEvent()
        {
            UpdateMaxValue();
            UpdateUI(); 
        }

        private void UpdateMaxValue()
        {
            var userLevel = _playerTraits.Level;
            if (userLevel >= _levelConfiguration.Levels.Count)
            {
                MaxValue = _levelConfiguration.Levels[_levelConfiguration.Levels.Count - 1].ExpForNextLevel;
            }
            else
            {
                MaxValue = _levelConfiguration.GetLevelByOrder(userLevel).ExpForNextLevel;
            }
        }

        private void GainedExperienceEvent()
        {
            CurrentValue = _playerTraits.ResistancePointsGained;
            UpdateUI();
        }

        protected override void UpdateUI()
        {
            var currentLevel = _playerTraits.Level;

            var expForCurrentLevel = _levelConfiguration.GetLevelByOrder(currentLevel).ExpForNextLevel;

            var expInCurrentLevel = (float)_playerTraits.ResistancePointsGained;
            
            var actualPercentage = expInCurrentLevel / expForCurrentLevel;

            var foregroundWidth = _background.rect.width * actualPercentage;
            _foreground.sizeDelta = new Vector2(foregroundWidth, _foreground.rect.height);
            
            _progressText.SetText(CurrentValue + " / " + MaxValue);
        }
    }
}