using System;
using System.Linq;
using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LocationsManager : MonoBehaviour
    {
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private Entity.Player.Player _player;
        
        // TODO : Get This from Saved Data
        [SerializeField] private SceneAsset _firstScene;
        
        private void Awake()
        {
            _locationsChannel.ChangeLocationEvent += ChangeLocationEvent;
            
            LoadFirstScene();
        }

        private void LoadFirstScene()
        {
            var operation = SceneManager.LoadSceneAsync(_firstScene.name, LoadSceneMode.Additive);
            
            operation.completed += asyncOperation =>
            {
                _locationsChannel.OnChangeLocationComplete(_firstScene, null);
                
                var startingPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

                if (startingPoint == null)
                {
                    Debug.LogError("There is no Object with 'SpawnPoint' Tag");
                }
                
                _player.transform.position = startingPoint.transform.position;
            };
        }
        
        private void ChangeLocationEvent(SceneAsset destination, SceneAsset source)
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