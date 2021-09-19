using System;
using System.Collections;
using System.Collections.Generic;
using Entity.Enemies;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace World
{
    public class EnemySpawnArea : MonoBehaviour
    {
        [SerializeField] private List<EnemySpawn> _enemySpawns;
        [SerializeField] private Transform _rightBounds;
        [SerializeField] private Transform _leftBounds;
        [SerializeField] private float _firstSpawnDelay;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private LocationsChannel _locationsChannel;

        private bool _isSpawning = true;

        private List<SpawnedEnemy> _liveEnemies;

        private void Awake()
        {
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
            _liveEnemies = new List<SpawnedEnemy>();
            InitializeSpawn();
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
        }

        private void ChangeLocationCompleteEvent(SceneMeta arg0, SceneMeta arg1)
        {
            InitializeSpawn();
        }
        
        private void InitializeSpawn() 
        {
            var sortingOrder = 0;
            for (int i = 0; i < _enemySpawns.Count; i++)
            {
                var enemySpawnType = _enemySpawns[i];
            
                if (enemySpawnType.SpawnPosition == null)
                {
                    Debug.Log("No Assigned Spawn Position for Enemy Spawn Number " + (i + 1));
                    continue;
                }

                var enemy = Instantiate(enemySpawnType.EnemyPrefab,
                    enemySpawnType.SpawnPosition.position,
                    Quaternion.identity, transform);
                
                _liveEnemies.Add(new SpawnedEnemy()
                {
                    SpawnPosition = enemySpawnType.SpawnPosition,
                    Object = enemy,
                    Enemy = enemy.GetComponentInChildren<Enemy>()
                });
                _liveEnemies[_liveEnemies.Count - 1].Object.SetActive(false);
                var movement = enemy.GetComponent<EnemyMovement>();
                movement.LeftBounds = _leftBounds.position;
                movement.RightBounds = _rightBounds.position;

                var renderer = enemy.GetComponentInChildren<Renderer>();
                renderer.sortingOrder = sortingOrder;
                sortingOrder++;
            }
            
            StartCoroutine(SpawnInterval());
        }
        
        private IEnumerator SpawnInterval()
        {
            yield return new WaitForSeconds(_firstSpawnDelay);
            
            Spawn();
            
            while (_isSpawning)
            {
                yield return new WaitForSeconds(_spawnInterval);
                    
                Spawn();
            }

            yield break;
        }

        private void Spawn()
        {
            foreach (var liveEnemy in _liveEnemies)
            {
                if (!liveEnemy.Object.activeInHierarchy)
                {
                    liveEnemy.Object.transform.position = liveEnemy.SpawnPosition.position;
                    liveEnemy.Object.SetActive(true);
                    liveEnemy.Enemy.ComeAlive();
                }
            }
        }
    }

    [Serializable]
    public class EnemySpawn
    {
        public GameObject EnemyPrefab;
        public Transform SpawnPosition;
    }

    public struct SpawnedEnemy
    {
        public GameObject Object;
        public Enemy Enemy;
        public Transform SpawnPosition;
    }
}