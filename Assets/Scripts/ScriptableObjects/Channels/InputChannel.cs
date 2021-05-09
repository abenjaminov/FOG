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
        public Dictionary<KeyCode, UnityAction> MappedKeyDownActions = new Dictionary<KeyCode, UnityAction>();
        public Dictionary<KeyCode, UnityAction> MappedKeyUpActions = new Dictionary<KeyCode, UnityAction>();

        public void OnKeyDown(KeyCode keyCode)
        {
            var action = MappedKeyDownActions[keyCode];
            action?.Invoke();
        }
        
        public void OnKeyUp(KeyCode keyCode)
        {
            var action = MappedKeyUpActions[keyCode];
            action?.Invoke();
        }

        public void RegisterKeyDown(KeyCode keyCode, UnityAction action)
        {
            if (!MappedKeyDownActions.ContainsKey(keyCode))
                MappedKeyDownActions[keyCode] = action;
            else
                MappedKeyDownActions[keyCode] += action;
        }
        
        public void RegisterKeyUp(KeyCode keyCode, UnityAction action)
        {
            if (!MappedKeyUpActions.ContainsKey(keyCode))
                MappedKeyUpActions[keyCode] = action;
            else
                MappedKeyUpActions[keyCode] += action;
        }
    }
}