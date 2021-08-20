using System.Collections.Generic;
using Persistence.Accessors;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Persistence
{
    public class PersistenceManager : MonoBehaviour
    {
        [SerializeField] private PersistenceChannel _persistenceChannel;
        
        private Dictionary<PersistantModuleTypes, List<IPersistantObject>> _moduleLoadedSubscriptions = 
            new Dictionary<PersistantModuleTypes, List<IPersistantObject>>();
        
        private Dictionary<PersistantModuleTypes, List<IPersistantObject>> _moduleClosingSubscriptions = 
            new Dictionary<PersistantModuleTypes, List<IPersistantObject>>();
        
        private List<IPersistenceModuleAccessor> _moduleAccessors = 
            new List<IPersistenceModuleAccessor>();
        
        public void RegisterModuleLoaded(IPersistantObject persistantObject)
        {
            var moduleType = persistantObject.GetModuleType();
            
            if(!_moduleLoadedSubscriptions.ContainsKey(moduleType))
                _moduleLoadedSubscriptions[moduleType] = new List<IPersistantObject>();
                
            _moduleLoadedSubscriptions[moduleType].Add(persistantObject);
        }
        
        public void RegisterModuleClosing(IPersistantObject persistantObject)
        {
            var moduleType = persistantObject.GetModuleType();
            
            if(!_moduleClosingSubscriptions.ContainsKey(moduleType))
                _moduleClosingSubscriptions[moduleType] = new List<IPersistantObject>();
                
            _moduleClosingSubscriptions[moduleType].Add(persistantObject);
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