using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableObjects.GameConfiguration
{
    [CreateAssetMenu(fileName = "Enemy List", menuName = "Game Configuration/Enemies")]
    public class EnemyList : ScriptableObject
    {
        public List<EnemyMeta> Enemies;

        public EnemyMeta GetEnemyMetaByPhrase(string phrase)
        {
            return Enemies.FirstOrDefault(x => x.ReplacementPhrase == phrase);
        }
    }
}