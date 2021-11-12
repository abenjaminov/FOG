using System;
using System.Collections.Generic;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Persistence
{
    public class EditorPersistenceInfo : MonoBehaviour
    {
        [SerializeField] private PersistenceChannel _persistenceChannel;
        [SerializeField] private List<PersistentMonoBehaviour> _persistenceHandlers;
        
        private void Awake()
        {
            if (!Application.isEditor) return;
            
            _persistenceChannel.GameModulesSavedEvent += GameModulesSavedEvent;
        }

        private void OnDestroy()
        {
            _persistenceChannel.GameModulesSavedEvent -= GameModulesSavedEvent;
        }

        private void GameModulesSavedEvent()
        {
            foreach (var handler in _persistenceHandlers)
            {
                handler.PrintPersistantData();
            }
        }
    }
}