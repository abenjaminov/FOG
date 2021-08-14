using System;
using Entity.Enemies;
using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.Quests
{
    [CreateAssetMenu(fileName = "Kill Enemies Quest", menuName = "Quest/Kill Enemies Quest")]
    public class KillEnemiesQuest : ProgressQuest
    {
        [Header("Kill enemies quest")]
        public GameObject EnemyPrefab;
        
        public CombatChannel _combatChannel;
        public int NumberOfEnemiesToKill;
        
        private Enemy _questEnemy;
        private int _actualEnemiesKilled = 0;

        protected override void OnEnable()
        {
            base.OnEnable();
            _questEnemy = EnemyPrefab.GetComponentInChildren<Enemy>();
            MaxValue = NumberOfEnemiesToKill;
            
            // TODO : Remove for production
            _actualEnemiesKilled = 0;
            ProgressMade(_actualEnemiesKilled);
        }

        protected override void QuestActive()
        {
            _combatChannel.EnemyDiedEvent += EnemyDiedEvent;
        }

        protected override void QuestCompleted()
        {
            _combatChannel.EnemyDiedEvent -= EnemyDiedEvent;
            this.Complete();
        }
        
        private void EnemyDiedEvent(Enemy killedEnemy)
        {
            if (State == QuestState.PendingComplete) return;
            
            if (killedEnemy.EnemyID == _questEnemy.EnemyID)
            {
                _actualEnemiesKilled++;
                ProgressMade(_actualEnemiesKilled);
            }

            if (_actualEnemiesKilled == NumberOfEnemiesToKill)
            {
                this.Complete();
            }
        }
    }
}