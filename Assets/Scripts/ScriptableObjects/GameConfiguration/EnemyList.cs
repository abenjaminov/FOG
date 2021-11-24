using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using ScriptableObjects.Quests;
using UnityEngine;

namespace ScriptableObjects.GameConfiguration
{
    [CreateAssetMenu(fileName = "Enemy List", menuName = "Game Configuration/Enemies")]
    public class EnemyList : ScriptableObject
    {
        public List<EnemyMeta> Enemies;

        private void OnEnable()
        {
#if UNITY_EDITOR
            var enemyAssets = AssetsHelper.GetAllAssets<EnemyMeta>();

            Enemies ??= new List<EnemyMeta>();
            Enemies.Clear();

            UnityEditor.AssetDatabase.Refresh();
            
            foreach (var enemy in enemyAssets)
            {
                if (string.IsNullOrEmpty(enemy.Guid))
                {
                    enemy.Guid = Guid.NewGuid().ToString();
                    UnityEditor.EditorUtility.SetDirty(enemy);
                }
                Enemies.Add(enemy);
            }
            
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

        public EnemyMeta GetEnemyMetaByPhrase(string phrase)
        {
            return Enemies.FirstOrDefault(x => x.ReplacementPhrase == phrase);
        }
    }
}