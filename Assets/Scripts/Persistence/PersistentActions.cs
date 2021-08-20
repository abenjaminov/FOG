using System;
using System.Collections.Generic;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Persistence
{
    [CreateAssetMenu(fileName = "Persistent Actions", menuName = "Game Configuration/Persistent Actions")]
    public class PersistentActions : ScriptableObject
    {
        [SerializeField] private PersistenceChannel _persistenceChannel;
        private List<Action> _persistantActions;

        private void OnEnable()
        {
            _persistenceChannel.GameModulesLoadedEvent += GameModulesLoadedEvent;
            _persistantActions = new List<Action>();
        }

        private void OnDisable()
        {
            _persistenceChannel.GameModulesLoadedEvent -= GameModulesLoadedEvent;
        }

        private void GameModulesLoadedEvent()
        {
            foreach (var action in _persistantActions)
            {
                action?.Invoke();
            }
        }

        public void RunPersistantAction(Action action)
        {
            if (!_persistenceChannel.IsReady)
                _persistantActions.Add(action);
            else
            {
                action?.Invoke();
            }
        }
    }
}