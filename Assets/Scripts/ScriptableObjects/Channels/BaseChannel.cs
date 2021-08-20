using System;
using Persistence;
using UnityEngine;

namespace ScriptableObjects.Channels
{
    public abstract class BaseChannel : ScriptableObject
    {
        [SerializeField] private PersistentActions _persistentActions;

        protected void InvokeEvent(Action eventWrapper)
        {
            _persistentActions.RunPersistantAction(eventWrapper);
        }
    }
}