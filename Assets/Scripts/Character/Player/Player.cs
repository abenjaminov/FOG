using System.Linq;
using Character.Enemies;
using ScriptableObjects;
using UnityEngine;

namespace Character.Player
{
    public class Player : Character
    {
        private PlayerTraits _playerTraits;
        [SerializeField] private CombatChannel _combatChannel;
        [SerializeField] private LevelConfiguration _levelConfiguration;
        
        void Awake()
        {
            _playerTraits = Traits as PlayerTraits;;
            _combatChannel.EnemyDiedEvent += EnemyDiedEvent;
            _playerTraits.GainedExperienceEvent += GainedExperienceEvent; 
        }

        private void GainedExperienceEvent()
        {
            var nextLevel = _levelConfiguration.Levels.FirstOrDefault(x => x.Order == _playerTraits.Level + 1);
            if (nextLevel != null && _playerTraits.ExperienceGained >= nextLevel.fromExp - 1)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _playerTraits.Level++;
        }
        
        public override void ReceiveDamage(float damage)
        {
            
        }

        protected override void Die()
        {
            
        }

        private void EnemyDiedEvent(Enemy DeadEnemy)
        {
            _playerTraits.ExperienceGained += ((EnemyTraits) DeadEnemy.Traits).Experience;
        }
    }
}