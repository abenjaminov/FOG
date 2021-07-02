using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Input Channel", menuName = "Channels/Input Channel", order = 2)]
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

        public void ok(KeyCode k, UnityAction a)
        {
            MappedKeyDownActions[k] -= a;
        }
        
        public KeySubscription SubscribeKeyDown(KeyCode keyCode, UnityAction action)
        {
            if (MappedKeyDownActions.TryGetValue(keyCode, out var keyEvent))
            {
                MappedKeyDownActions[keyCode] += action;
            }
            else
            {
                keyEvent += action;
                MappedKeyDownActions.Add(keyCode, keyEvent);
            }

            return new KeyDownSubscription(this, keyCode, action);
        }
        
        public KeySubscription SubscribeKeyUp(KeyCode keyCode, UnityAction action)
        {
            if (MappedKeyUpActions.TryGetValue(keyCode, out var keyEvent))
            {
                MappedKeyUpActions[keyCode] += action;
            }
            else
            {
                keyEvent += action;
                MappedKeyUpActions.Add(keyCode, keyEvent);
            }

            return new KeyUpSubscription(this, keyCode, action);
        }
    }

    public abstract class KeySubscription
    {
        protected KeyCode Key;
        protected UnityAction _action;
        protected InputChannel _channel;

        internal KeySubscription(InputChannel channel, KeyCode code, UnityAction action)
        {
            Key = code;
            _action = action;
            _channel = channel;
        }

        public abstract void Unsubscribe();
    }

    public class KeyDownSubscription : KeySubscription
    {
        public KeyDownSubscription(InputChannel channel, KeyCode code, UnityAction action) : base(channel, code, action)
        {
        }

        public override void Unsubscribe()
        {
            _channel.MappedKeyDownActions[Key] -= _action;
        }
    }
    
    public class KeyUpSubscription : KeySubscription
    {
        public KeyUpSubscription(InputChannel channel, KeyCode code, UnityAction action) : base(channel, code, action)
        {
        }

        public override void Unsubscribe()
        {
            _channel.MappedKeyUpActions[Key] -= _action;
        }
    }
}