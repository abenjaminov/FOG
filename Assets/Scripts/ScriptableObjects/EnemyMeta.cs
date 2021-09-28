using Entity.Enemies;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Meta", menuName = "Game Configuration/Enemy Meta")]
    public class EnemyMeta : AssetMeta
    {
        [SerializeField] private EnemyTraits _enemyTraits;
        public GameObject EnemyPrefab;
    }
}