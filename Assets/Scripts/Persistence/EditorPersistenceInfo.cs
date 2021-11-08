using System;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Persistence
{
    public class EditorPersistenceInfo : MonoBehaviour
    {
        [SerializeField] private PersistenceChannel _persistenceChannel;

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
            
        }
    }
}