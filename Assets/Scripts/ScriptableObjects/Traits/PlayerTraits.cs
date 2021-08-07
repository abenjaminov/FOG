using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("resistancePointsGained")] [FormerlySerializedAs("_experienceGained")] [SerializeField] private int _resistancePointsGained;

        public float ReceiveDamageCooldown;
        
        public int Strength;
        public int Dexterity;
        public int Inteligence;
        public int Constitution;
        
        
        public int ResistancePointsGained
        {
            get => _resistancePointsGained;
            set
            {
                _resistancePointsGained = value;
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
            _resistancePointsGained = 0;
            
            LevelUpEvent?.Invoke();
        }

        protected override void Reset()
        {
            base.Reset();

            _resistancePointsGained = 0;
            Strength = 5;
            Dexterity = 5;
            Constitution = 5;
            Inteligence = 5;
            PointsLeft = 0;
            CurrentHealth = MaxHealth;
            MonsterStateResistance = 1;
        }
    }
}