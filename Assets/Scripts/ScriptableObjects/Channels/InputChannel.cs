using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Input Channel", menuName = "Channels/Input Channel", order = 1)]
    public class InputChannel : ScriptableObject
    {
        public Dictionary<KeyCode, UnityAction> MappedActions = new Dictionary<KeyCode, UnityAction>();

        public void OnKeyDown(KeyCode keyCode)
        {
            var action = MappedActions[keyCode];
            action?.Invoke();
        }

        public void RegisterKeyDown(KeyCode keyCode, UnityAction action)
        {
            if (!MappedActions.ContainsKey(keyCode))
                MappedActions[keyCode] = action;
            else
                MappedActions[keyCode] += action;
        }
    }
}