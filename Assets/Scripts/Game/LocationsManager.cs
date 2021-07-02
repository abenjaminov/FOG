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

        private void Awake()
        {
            _locationsChannel.ChangeLocationEvent += ChangeLocationEvent;
        }

        private void ChangeLocationEvent(SceneAsset destination, SceneAsset source)
        {
            var operation = SceneManager.LoadSceneAsync(destination.name);
            
            operation.completed += asyncOperation =>
            {
                _locationsChannel.OnChangeLocationComplete();

                var teleports = FindObjectsOfType<Teleport>().Where(x => x.Destination == destination);
                
            };
        }
    }
}