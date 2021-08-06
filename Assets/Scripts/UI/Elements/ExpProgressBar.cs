using System;
using ScriptableObjects;
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

            CurrentValue = _playerTraits.ExperienceGained;
            
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
                MaxValue = _levelConfiguration.GetLevelByOrder(userLevel + 1).ExpForNextLevel;
            }
        }

        private void GainedExperienceEvent()
        {
            CurrentValue = _playerTraits.ExperienceGained;
            UpdateUI();
        }

        protected override void UpdateUI()
        {
            var currentLevel = _playerTraits.Level;
            var previousLevel = _playerTraits.Level - 1;

            var expForCurrentLevel = _levelConfiguration.GetLevelByOrder(currentLevel).ExpForNextLevel;

            var expInCurrentLevel = (float)_playerTraits.ExperienceGained;
            
            var actualPercentage = expInCurrentLevel / expForCurrentLevel;

            var foregroundWidth = _background.rect.width * actualPercentage;
            _foreground.sizeDelta = new Vector2(foregroundWidth, _foreground.rect.height);
            
            _progressText.SetText(CurrentValue + " / " + MaxValue);
        }
    }
}