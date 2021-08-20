using System;
using System.Linq;
using Assets.HeroEditor.Common.CommonScripts;
using Persistence;
using Persistence.Accessors;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LocationsManager : PersistentMonoBehaviour
    {
        [SerializeField] private PersistenceChannel _persistenceChannel;
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private ScenesList _scenesList;
        [SerializeField] private Entity.Player.Player _player;
        
        [SerializeField] private SceneMeta _defaultFirstScene;
        private SceneMeta _currentScene;
        
        protected override void Awake()
        {
            base.Awake();
            
            _locationsChannel.ChangeLocationEvent += ChangeLocationEvent;
            _persistenceChannel.GameModulesLoadedEvent += GameModulesLoadedEvent;
            
            // TODO open loading scene
        }

        private void GameModulesLoadedEvent()
        {
            LoadFirstScene();
            _player.SetActive(true);
        }

        public override void OnModuleLoaded(IPersistenceModuleAccessor accessor)
        {
            var sceneMetaId = accessor.GetValue<string>("FirstScene");
            var meta = _scenesList.GetSceneMetaById(sceneMetaId);

            _currentScene = meta != null ? meta : _defaultFirstScene;
        }

        public override void OnModuleClosing(IPersistenceModuleAccessor accessor)
        {
            accessor.PersistData("FirstScene",_currentScene.Id);
        }

        private void LoadFirstScene()
        {
            var operation = SceneManager.LoadSceneAsync(_currentScene.SceneAsset.name, LoadSceneMode.Additive);
            
            operation.completed += asyncOperation =>
            {
                _locationsChannel.OnChangeLocationComplete(_currentScene, null);
                
                var startingPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

                if (startingPoint == null)
                {
                    Debug.LogError("There is no Object with 'SpawnPoint' Tag");
                }
                
                _player.transform.position = startingPoint.transform.position;
            };
        }
        
        private void ChangeLocationEvent(SceneMeta destination, SceneMeta source)
        {
            SceneManager.UnloadSceneAsync(source.name);
                
            var operation = SceneManager.LoadSceneAsync(destination.name, LoadSceneMode.Additive);
            
            operation.completed += asyncOperation =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(destination.name));
                
                _locationsChannel.OnChangeLocationComplete(destination, source);

                var teleport = FindObjectsOfType<Teleport>().SingleOrDefault(x => x.Destination == source);

                if (teleport != null)
                {
                    _player.transform.position = teleport.CenterTransform.position;
                }
            };
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationEvent -= ChangeLocationEvent;
        }
    }
}