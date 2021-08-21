using System.Collections.Generic;
using Persistence.Accessors;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Persistence
{
    public class PersistenceManager : MonoBehaviour
    {
        [SerializeField] private PersistenceChannel _persistenceChannel;
        
        private Dictionary<PersistantModuleTypes, List<IPersistentObject>> _moduleLoadedSubscriptions = 
            new Dictionary<PersistantModuleTypes, List<IPersistentObject>>();
        
        private Dictionary<PersistantModuleTypes, List<IPersistentObject>> _moduleClosingSubscriptions = 
            new Dictionary<PersistantModuleTypes, List<IPersistentObject>>();
        
        private List<IPersistenceModuleAccessor> _moduleAccessors = 
            new List<IPersistenceModuleAccessor>();
        
        public void RegisterModuleLoaded(IPersistentObject persistentObject)
        {
            var moduleType = persistentObject.GetModuleType();
            
            if(!_moduleLoadedSubscriptions.ContainsKey(moduleType))
                _moduleLoadedSubscriptions[moduleType] = new List<IPersistentObject>();
                
            _moduleLoadedSubscriptions[moduleType].Add(persistentObject);
        }
        
        public void RegisterModuleClosing(IPersistentObject persistentObject)
        {
            var moduleType = persistentObject.GetModuleType();
            
            if(!_moduleClosingSubscriptions.ContainsKey(moduleType))
                _moduleClosingSubscriptions[moduleType] = new List<IPersistentObject>();
                
            _moduleClosingSubscriptions[moduleType].Add(persistentObject);
        }

        public void Awake()
        {
            _moduleAccessors.Add(new FileModuleAccessor(PersistantModuleTypes.GameState));
            _moduleAccessors.Add(new FileModuleAccessor(PersistantModuleTypes.Inventory));
            _moduleAccessors.Add(new FileModuleAccessor(PersistantModuleTypes.PlayerTraits));
        }

        public void Start()
        {
            foreach (var moduleAccessor in _moduleAccessors)
            {
                var moduleType = moduleAccessor.GetModuleType();
                
                if(!_moduleLoadedSubscriptions.ContainsKey(moduleType)) continue;
                
                moduleAccessor.LoadModule();
                
                foreach (var subscription in _moduleLoadedSubscriptions[moduleType])
                {
                    subscription.OnModuleLoaded(moduleAccessor);
                }
            }
            
            _persistenceChannel.OnGameModulesLoaded();
        }

        public void OnApplicationQuit()
        {
            foreach (var moduleAccessor in _moduleAccessors)
            {
                var moduleType = moduleAccessor.GetModuleType();
                
                if(!_moduleClosingSubscriptions.ContainsKey(moduleType)) continue;
                
                foreach (var subscription in _moduleClosingSubscriptions[moduleType])
                {
                    subscription.OnModuleClosing(moduleAccessor);
                }
                
                moduleAccessor.CloseModule();
            }
        }
    }
}