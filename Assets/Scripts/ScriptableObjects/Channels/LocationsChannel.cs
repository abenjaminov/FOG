using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Locations Channel", menuName = "Channels/Locations Channel", order = 5)]
    public class LocationsChannel : ScriptableObject
    {
        public UnityAction<SceneAsset, SceneAsset> ChangeLocationEvent;
        public UnityAction ChangeLocationCompleteEvent;

        public void OnChangeLocation(SceneAsset destination, SceneAsset source)
        {
            ChangeLocationEvent?.Invoke(destination, source);
        }

        public void OnChangeLocationComplete()
        {
            ChangeLocationCompleteEvent?.Invoke();
        }
    }
}