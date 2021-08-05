using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Traits", menuName = "Game Stats/Player Traits", order = 0)]
    public class PlayerTraits : Traits.Traits
    {
        public UnityAction GainedExperienceEvent;

        public float ClimbSpeed;

        [Range(0,1)]
        public float MonsterStateResistance;
        
        [HideInInspector] public float CurrentHealth;
        [HideInInspector] public int PointsLeft;

        [SerializeField] private LevelConfiguration _levelConfiguration;
        [SerializeField] private int _experienceGained;

        public float ReceiveDamageCooldown;
        
        public int ExperienceGained
        {
            get => _experienceGained;
            set
            {
                _experienceGained = value;
                GainedExperienceEvent?.Invoke();
            }
        }

        public void ChangeCurrentHealth(float healthDelta)
        {
            CurrentHealth = Mathf.Max(0, Mathf.Min(MaxHealth, CurrentHealth + healthDelta));
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