using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ScriptableObjects.Traits
{
    [CreateAssetMenu(fileName = "Player Traits", menuName = "Game Stats/Player Traits", order = 0)]
    public class PlayerTraits : Traits
    {
        public UnityAction GainedExperienceEvent;
        public UnityAction MonsterResistanceChangedEvent;
        
        public const float MaxMonsterStateResistance = 100;
        public const float MinMonsterStateResistance = 0;

        public float ClimbSpeed;
        
        [HideInInspector] public float CurrentHealth;
        [HideInInspector] public int PointsLeft;

        [SerializeField] internal LevelConfiguration _levelConfiguration;
        [SerializeField] private int _resistancePointsGained;
        [SerializeField] private float _monsterStateResistance;

        public float ReceiveDamageCooldown;
        
        public int Strength;
        public int Dexterity;
        public int Intelligence;
        public int Constitution;
        
        public float MonsterStateResistance
        {
            get => _monsterStateResistance;
            set
            {
                _monsterStateResistance = value;
                MonsterResistanceChangedEvent?.Invoke();
            }
        }

        public int ResistancePointsGained
        {
            get => _resistancePointsGained;
            set
            {
                _resistancePointsGained = value;
                GainedExperienceEvent?.Invoke();
            }
        }

        public void SetResistancePointsSilent(int resistancePointsGained)
        {
            _resistancePointsGained = resistancePointsGained;
        }

        public void SetMonsterResistanceSilent(float monsterResistance)
        {
            _monsterStateResistance = monsterResistance;
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
            var prevLevel = _levelConfiguration.Levels.FirstOrDefault(x => x.Order == Level);
            Level++;
            var level = _levelConfiguration.Levels.FirstOrDefault(x => x.Order == Level);
            if (level == null || prevLevel == null) return;
            
            PointsLeft += level.Points;
            ResistancePointsGained = _resistancePointsGained - prevLevel.ExpForNextLevel;
            
            LevelUpEvent?.Invoke();
        }

        public new void Reset()
        {
            WalkSpeed = 3;
            JumpHeight = 1.1f;
            BaseDelayBetweenAttacks = 0.8f;
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            Defense = 5;
            Level = 1;
            ClimbSpeed = 3;
            _resistancePointsGained = 0;
            _monsterStateResistance = 100;
            ReceiveDamageCooldown = 1;
            Strength = 5;
            Dexterity = 5;
            Intelligence = 5;
            Intelligence = 5;
            Constitution = 5;
            PointsLeft = 0;
        }
    }
}