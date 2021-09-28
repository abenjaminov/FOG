using Game;
using Persistence.Accessors;
using ScriptableObjects;
using ScriptableObjects.GameConfiguration;
using UnityEngine;

namespace Persistence.PersistenceHandlers
{
    public class LocationsPersistenceHandler : PersistentMonoBehaviour
    {
        [SerializeField] private LocationsManager _locationsManager;
        [SerializeField] private ScenesList _scenesList;
        
        public override void OnModuleLoaded(IPersistenceModuleAccessor accessor)
        {
            var sceneMetaId = accessor.GetValue<string>("FirstScene");
            var meta = _scenesList.GetSceneMetaById(sceneMetaId);

            _locationsManager.CurrentScene = meta != null ? meta : _scenesList.DefaultFirstScene;
        }

        public override void OnModuleClosing(IPersistenceModuleAccessor accessor)
        {
            accessor.PersistData("FirstScene",_locationsManager.CurrentScene.Id);
        }
    }
}