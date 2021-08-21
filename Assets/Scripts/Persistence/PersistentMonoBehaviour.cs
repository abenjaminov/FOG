using System;
using Persistence.Accessors;
using UnityEngine;

namespace Persistence
{
    public abstract class PersistentMonoBehaviour : MonoBehaviour, IPersistentObject
    {
        [SerializeField] private PersistantModuleTypes _moduleType;
        [SerializeField] private PersistenceManager _persistenceManager;

        protected virtual void Awake()
        {
            _persistenceManager.RegisterModuleLoaded(this);
            _persistenceManager.RegisterModuleClosing(this);
        }

        public PersistantModuleTypes GetModuleType()
        {
            return _moduleType;
        }

        public abstract void OnModuleLoaded(IPersistenceModuleAccessor accessor);

        public abstract void OnModuleClosing(IPersistenceModuleAccessor accessor);
    }
}