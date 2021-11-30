using System.Linq;
using Helpers;
using ScriptableObjects.Channels;
using ScriptableObjects.GameConfiguration;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ScriptableObjects.Traits
{
    [CreateAssetMenu(fileName = "Player Traits", menuName = "Game Stats/Player Traits", order = 0)]
    public class PlayerTraits : Traits
    {
        public const float MaxMonsterStateResistance = 100;
        public const float MinMonsterStateResistance = 0;

        public float ClimbSpeed;

        [HideInInspector] public bool IsNameSet;
        [HideInInspector] public float CurrentHealth;
        [SerializeField] public int PointsLeft;

        [SerializeField] private PlayerChannel _playerChannel;
        [SerializeField] internal LevelConfiguration _levelConfiguration;
        [SerializeField] private int _resistancePointsGained;
        [SerializeField] private float _monsterStateResistance;

        public float ReceiveDamageCooldown;
        
        [SerializeField] private int _strength;
        [SerializeField] private int _dexterity;
        [SerializeField] private int _intelligence;
        [SerializeField] private int _defense;

        public int Strength
        {
            get => _strength;
            set
            {
                _strength = value;
                _playerChannel.OnTraitsChangedEvent();
            }
        }
        public int Dexterity
        {
            get => _dexterity;
            set
            {
                _dexterity = value;
                _playerChannel.OnTraitsChangedEvent();
            }
        }
        public int Intelligence
        {
            get => _intelligence;
            set
            {
                _intelligence = value;
                _playerChannel.OnTraitsChangedEvent();
            }
        }

        public int Defense
        {
            get => _defense;
            set
            {
                _defense = value;
                UpdateMaxHealth();
                _playerChannel.OnTraitsChangedEvent();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            MaxHealth = TraitsHelper.GetPlayerMaxHealth(this);
        }

        public float MonsterStateResistance
        {
            get => _monsterStateResistance;
            set
            {
                SetMonsterResistanceSilent(value);
                _playerChannel.OnMonsterResistanceChanged();
            }
        }

        public int ResistancePointsGained
        {
            get => _resistancePointsGained;
            set
            {
                var oldPoints = _resistancePointsGained;
                _resistancePointsGained = value;
                _playerChannel.OnGainedResistancePointsEvent(_resistancePointsGained - oldPoints);
            }
        }

        public void SetName(string newName)
        {
            Name = newName;
            IsNameSet = true;
            _playerChannel.OnNameSet();
        }
        
        public void SetResistancePointsSilent(int resistancePointsGained)
        {
            _resistancePointsGained = resistancePointsGained;
        }
        
        
        public void SetMonsterResistanceSilent(float monsterResistance)
        {
            _monsterStateResistance = Mathf.Min(MaxMonsterStateResistance,monsterResistance);
        }
        
        public void ChangeCurrentHealth(float healthDelta)
        {
            CurrentHealth = Mathf.Max(0, Mathf.Min(MaxHealth, CurrentHealth + healthDelta));
            HealthChangedEvent?.Invoke();

            if (CurrentHealth <= 0)
            {
                DiedEvent?.Invoke();
            }
        }

        public void Revive()
        {
            CurrentHealth = 50;
            HealthChangedEvent?.Invoke();

            var resistancePointsToLose = (int)(_levelConfiguration.GetLevelByOrder(this.Level).ExpForNextLevel * 0.15);
            ResistancePointsGained = Mathf.Max(0, _resistancePointsGained - resistancePointsToLose);
            
            _playerChannel.OnRevive();
        }

        public float GetCurrentHealth()
        {
            return CurrentHealth;
        }

        private void UpdateMaxHealth()
        {
            var prevMaxHealth = MaxHealth;
            MaxHealth = TraitsHelper.GetPlayerMaxHealth(this);
            CurrentHealth += prevMaxHealth - CurrentHealth; 
        }
        
        public void LevelUp()
        {
            var prevLevel = _levelConfiguration.Levels.FirstOrDefault(x => x.Order == Level);
            Level++;
            var level = _levelConfiguration.Levels.FirstOrDefault(x => x.Order == Level);
            if (level == null || prevLevel == null) return;
            
            PointsLeft += level.Points;
            _resistancePointsGained = _resistancePointsGained - prevLevel.ExpForNextLevel;
            UpdateMaxHealth();
            
            _playerChannel.OnLevelUp();
        }

        public new void Reset()
        {
            WalkSpeed = 3;
            JumpHeight = 1.1f;
            BaseDelayBetweenAttacks = 0.8f;
            Level = 1;
            ClimbSpeed = 3;
            _resistancePointsGained = 0;
            _monsterStateResistance = 100;
            ReceiveDamageCooldown = 1;
            _strength = 5;
            _dexterity = 5;
            _intelligence = 5;
            _intelligence = 5;
            _defense = 5;
            PointsLeft = 0;
            Name = "????";
            IsNameSet = false;
            MaxHealth = TraitsHelper.GetPlayerMaxHealth(this);
            CurrentHealth = MaxHealth;
        }
    }
}