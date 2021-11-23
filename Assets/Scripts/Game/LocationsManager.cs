using System;
using System.Linq;
using System.Threading.Tasks;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.GameConfiguration;
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
            ChangeLocation(destination, source, true);
        }

        private void GameModulesLoadedEvent()
        {
            LoadFirstScene();
            _player.SetActive(true);
        }

        private void LoadFirstScene()
        {
            var operation = SceneManager.LoadSceneAsync(CurrentScene.SceneName, LoadSceneMode.Additive);

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
            ChangeLocation(destination, source);
        }

        private async void ChangeLocation(SceneMeta destination, SceneMeta source, bool ignoreTeleports = false)
        {
            SceneManager.UnloadSceneAsync(source.SceneName);
            
            var operation = SceneManager.LoadSceneAsync(destination.name, LoadSceneMode.Additive);

            await Task.Delay(TimeSpan.FromSeconds(3));
            
            operation.completed += asyncOperation =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(destination.name));
                
                _currentScene = destination;
            
                _locationsChannel.OnChangeLocationComplete(destination, source);

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