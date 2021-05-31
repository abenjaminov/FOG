using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Traits", menuName = "Game Stats/Player Traits", order = 0)]
    public class PlayerTraits : Traits
    {
        public UnityAction GainedExperienceEvent;
        
        public float CurrentHealth;
        public int PointsLeft;

        [SerializeField] private int _experienceGained;
        [SerializeField] private LevelConfiguration _levelConfiguration;

        public int ExperienceGained
        {
            get => _experienceGained;
            set
            {
                _experienceGained = value;
                GainedExperienceEvent?.Invoke();
            }
        }

        public void SetCurrentHealth(float health)
        {
            CurrentHealth = Mathf.Max(0, health);;
            HealthChangedEvent?.Invoke();
        }
        
        public void ChangeCurrentHealth(float healthDelta)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth + healthDelta);
            HealthChangedEvent?.Invoke();
        }

        public float GetCurrentHealth()
        {
            return CurrentHealth;
        }
        
        public void LevelUp()
        {
            Level++;
            var level = _levelConfiguration.Levels.FirstOrDefault(x => x.Order == Level);
            if (level == null) return;
            
            PointsLeft += level.Points;
            
            LevelUpEvent?.Invoke();
        }
    }
}