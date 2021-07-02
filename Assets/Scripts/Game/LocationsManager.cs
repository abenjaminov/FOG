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

                var teleport = FindObjectsOfType<Teleport>().SingleOrDefault(x => x.Destination == destination);

                if (teleport != null)
                {
                    var player = FindObjectOfType<Entity.Player.Player>();
                    player.transform.position = teleport.CenterTransform.position;
                }
            };
        }
    }
}