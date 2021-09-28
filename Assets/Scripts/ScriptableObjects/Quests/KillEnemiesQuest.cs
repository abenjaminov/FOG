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
        public EnemyMeta EnemyMeta;
        
        public CombatChannel _combatChannel;
        public int NumberOfEnemiesToKill;
        
        private Enemy _questEnemy;

        [HideInInspector] public int ActualEnemiesKilled;

        public override string GetName()
        {
            return "Defeat " + NumberOfEnemiesToKill + " " + EnemyMeta.ReplacementPhrase;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _questEnemy = EnemyMeta.EnemyPrefab.GetComponentInChildren<Enemy>();
            MaxValue = NumberOfEnemiesToKill;
            
            ProgressMade(ActualEnemiesKilled);
        }

        public override void Activate()
        {
            base.Activate();
            
            _combatChannel.EnemyDiedEvent += EnemyDiedEvent;
        }

        public override void Complete()
        {
            _combatChannel.EnemyDiedEvent -= EnemyDiedEvent;
            
            base.Complete();
        }
        
        private void EnemyDiedEvent(Enemy killedEnemy)
        {
            if (State == QuestState.PendingComplete) return;
            
            if (killedEnemy.EnemyID == _questEnemy.EnemyID)
            {
                ActualEnemiesKilled++;
                ProgressMade(ActualEnemiesKilled);
            }

            if (ActualEnemiesKilled == NumberOfEnemiesToKill)
            {
                this.Complete();
            }
        }

        public override void ResetQuest()
        {
            base.ResetQuest();
            ActualEnemiesKilled = 0;
        }
    }
}