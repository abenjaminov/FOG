using System;
using Entity.Enemies;
using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.Quests
{
    [CreateAssetMenu(fileName = "Kill Enemies Quest", menuName = "Quest/Kill Enemies Quest")]
    public class KillEnemiesQuest : Quest
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
            _actualEnemiesKilled = 0;
            _questEnemy = EnemyPrefab.GetComponentInChildren<Enemy>();
        }

        protected override void QuestActive()
        {
            _combatChannel.EnemyDiedEvent += EnemyDiedEvent;
        }

        protected override void QuestCompleted()
        {
            _combatChannel.EnemyDiedEvent -= EnemyDiedEvent;
        }
        
        private void EnemyDiedEvent(Enemy killedEnemy)
        {
            if (killedEnemy.EnemyID == _questEnemy.EnemyID)
            {
                _actualEnemiesKilled++;
                Debug.Log(_actualEnemiesKilled + " out of " + NumberOfEnemiesToKill);
            }

            if (_actualEnemiesKilled == NumberOfEnemiesToKill)
            {
                this.Complete();
            }
        }
    }
}