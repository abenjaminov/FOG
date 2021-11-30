using System;
using System.IO;
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

        public abstract void PrintPersistantData();

        protected void PrintPersistenceAsTextInternal(string persistence, string moduleName)
        {
            var directory = Application.persistentDataPath + "\\Persistence\\";

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            var filePath = directory + $"{moduleName}.txt";
            File.WriteAllText(filePath, persistence);
        }
    }
}