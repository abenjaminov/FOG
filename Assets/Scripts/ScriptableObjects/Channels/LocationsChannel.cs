using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Locations Channel", menuName = "Channels/Locations Channel", order = 5)]
    public class LocationsChannel : ScriptableObject
    {
        public UnityAction<SceneMeta, SceneMeta> ChangeLocationEvent;
        public UnityAction<SceneMeta, SceneMeta> ChangeLocationCompleteEvent;

        public SceneMeta CurrentScene;
        
        public void OnChangeLocation(SceneMeta destination, SceneMeta source)
        {
            ChangeLocationEvent?.Invoke(destination, source);
        }

        public void OnChangeLocationComplete(SceneMeta destination, SceneMeta source)
        {
            CurrentScene = destination;
            ChangeLocationCompleteEvent?.Invoke(destination, source);
        }
    }
}