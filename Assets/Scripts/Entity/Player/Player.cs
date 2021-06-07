using System.Linq;
using Assets.HeroEditor.FantasyInventory.Scripts.Data;
using Entity.Enemies;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Entity.Player
{
    public abstract class Player : CharacterWrapper
    {
        private PlayerTraits _playerTraits;
        
        [Header("Player Specific")]
        [SerializeField] private CombatChannel _combatChannel;
        [SerializeField] private LevelConfiguration _levelConfiguration;
        public Transform FireTransform;

        [HideInInspector] public PlayerTraits PlayerTraits;
        [HideInInspector] public PlayerAppearance Apearence;
        
        protected override void Awake()
        {
            base.Awake();
            
            _playerTraits = Traits as PlayerTraits;;
            _combatChannel.EnemyDiedEvent += EnemyDiedEvent;
            
            // ReSharper disable once PossibleNullReferenceException
            _playerTraits.GainedExperienceEvent += GainedExperienceEvent; 
            
            PlayerTraits = Traits as PlayerTraits;
            Apearence = GetComponent<PlayerAppearance>();
        }

        private void GainedExperienceEvent()
        {
            var nextLevel = _levelConfiguration.Levels.FirstOrDefault(x => x.Order == _playerTraits.Level + 1);
            if (nextLevel != null && _playerTraits.ExperienceGained >= nextLevel.FromExp - 1)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _playerTraits.LevelUp();
            var level = _levelConfiguration.Levels.FirstOrDefault(x => x.Order == _playerTraits.Level);
            if (level == null) return;
            
            _playerTraits.PointsLeft += level.Points;
        }
        
        public override void ReceiveDamage(float damage)
        {
            if (IsDead) return;
            
            DisplayDamage(damage);

            ChangeHealth(-damage);

            if (_playerTraits.GetCurrentHealth() <= 0)
            {
                IsDead = true;
            }
        }

        public override void ChangeHealth(float delta)
        {
            _health = Mathf.Max(0, Mathf.Min(Traits.MaxHealth, _health + delta));
            _playerTraits.ChangeCurrentHealth(delta);
        }

        private void EnemyDiedEvent(Enemy deadEnemy)
        {
            _playerTraits.ExperienceGained += ((EnemyTraits) deadEnemy.Traits).Experience;
        }
    }
}