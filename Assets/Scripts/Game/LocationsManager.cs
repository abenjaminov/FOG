using System;
using System.Linq;
using Assets.HeroEditor.Common.CommonScripts;
using Persistence;
using Persistence.Accessors;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.GameConfiguration;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LocationsManager : MonoBehaviour
    {
        [SerializeField] private PersistenceChannel _persistenceChannel;
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private Entity.Player.Player _player;
        [SerializeField] private ScenesList _scenesList;
        
        private SceneMeta _currentScene;

        public SceneMeta CurrentScene
        {
            get => _currentScene ? _currentScene : _scenesList.DefaultFirstScene;
            set => _currentScene = value;
        }

        protected void Awake()
        {
            _locationsChannel.ChangeLocationEvent += ChangeLocationEvent;
            _persistenceChannel.GameModulesLoadedEvent += GameModulesLoadedEvent;
            _locationsChannel.RespawnEvent += OnRespawn;
            
        }

        private void OnRespawn(SceneMeta destination, SceneMeta source)
        {
            LoadScene(destination, source, true);
        }

        private void GameModulesLoadedEvent()
        {
            LoadFirstScene();
            _player.SetActive(true);
        }

        private void LoadFirstScene()
        {
            var operation = SceneManager.LoadSceneAsync(CurrentScene.SceneAsset.name, LoadSceneMode.Additive);

            operation.completed += asyncOperation =>
            {
                _locationsChannel.OnChangeLocationComplete(CurrentScene, null);

                PositionPlayerOnSpawnPoint();
            };
        }

        private void PositionPlayerOnSpawnPoint()
        {
            var startingPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

            if (startingPoint == null)
            {
                Debug.LogError("There is no Object with 'SpawnPoint' Tag");
            }

            _player.transform.position = startingPoint.transform.position;
        }

        private void ChangeLocationEvent(SceneMeta destination, SceneMeta source)
        {
            LoadScene(destination, source, true);
        }

        private void LoadScene(SceneMeta destination, SceneMeta source, bool ignoreTeleports = false)
        {
            SceneManager.UnloadSceneAsync(source.name);

            var operation = SceneManager.LoadSceneAsync(destination.name, LoadSceneMode.Additive);

            operation.completed += asyncOperation =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(destination.name));

                _locationsChannel.OnChangeLocationComplete(destination, source);
                _currentScene = destination;

                var teleport = FindObjectsOfType<Teleport>().SingleOrDefault(x => x.Destination == source);

                if (teleport != null && !ignoreTeleports)
                {
                    _player.transform.position = teleport.CenterTransform.position;
                }
                else
                {
                    PositionPlayerOnSpawnPoint();
                }
            };
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationEvent -= ChangeLocationEvent;
            _locationsChannel.RespawnEvent -= OnRespawn;
            _persistenceChannel.GameModulesLoadedEvent -= GameModulesLoadedEvent;
        }
    }
}