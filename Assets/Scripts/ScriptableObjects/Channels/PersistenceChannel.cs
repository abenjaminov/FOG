using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Persistence Channel", menuName = "Channels/Persistence Channel")]
    public class PersistenceChannel : ScriptableObject
    {
        public UnityAction GameModulesLoadedEvent;
        public bool IsReady = false;

        public void OnGameModulesLoaded()
        {
            IsReady = true;
            GameModulesLoadedEvent?.Invoke();
        }
    }
}