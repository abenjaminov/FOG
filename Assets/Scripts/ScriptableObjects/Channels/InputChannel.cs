using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Utilities;
using UI.Behaviours;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Input Channel", menuName = "Channels/Input Channel", order = 2)]
    public class InputChannel : ScriptableObject
    {
        [SerializeField] private LocationsChannel _locationsChannel;
        
        public Dictionary<KeyCode, List<KeySubscription>> MappedKeyDownActions = new Dictionary<KeyCode, List<KeySubscription>>();
        public Dictionary<KeyCode, List<KeySubscription>> MappedKeyUpActions = new Dictionary<KeyCode, List<KeySubscription>>();

        private bool _pauseInput;

        private List<KeySubscription> _currentExceptionsForPause;
        
        private void OnEnable()
        {
            _pauseInput = false;
            _currentExceptionsForPause = new List<KeySubscription>();
        }

        public void PauseInput(params KeySubscription[] exceptions)
        {
            _currentExceptionsForPause = exceptions.ToList();
            _pauseInput = true;
        }
        
        public void ResumeInput()
        {
            _currentExceptionsForPause.Clear();
            _pauseInput = false;
        }
        
        public void OnKeyDown(KeyCode keyCode)
        {
            if (!MappedKeyDownActions.ContainsKey(keyCode)) return;
            
            var isInputPaused = _locationsChannel.IsChangingLocation || _pauseInput;

            MappedKeyDownActions[keyCode] = MappedKeyDownActions[keyCode].Where(x => x.IsActive).ToList();
            
            var allSubs = MappedKeyDownActions[keyCode];

            allSubs?.ForEach(keySub =>
            {
                if (!isInputPaused ||
                    (MappedKeyDownActions.ContainsKey(keyCode) &&
                     _currentExceptionsForPause.Count(e => e.Id == keySub.Id) > 0))
                {
                    keySub.Invoke();
                }
            });
        }
        
        public void OnKeyUp(KeyCode keyCode)
        {
            if (!MappedKeyUpActions.ContainsKey(keyCode)) return;
            
            MappedKeyUpActions[keyCode] = MappedKeyUpActions[keyCode].Where(x => x.IsActive).ToList();
            var allSubs = MappedKeyUpActions[keyCode];

            allSubs?.ForEach(x =>
            {
                x.Invoke();
            });
        }

        public KeySubscription SubscribeKeyDown(KeyCode keyCode, UnityAction action)
        {
            var subscription = new KeyDownSubscription(keyCode, action);

            if (MappedKeyDownActions.ContainsKey(keyCode))
            {
                MappedKeyDownActions[keyCode].Add(subscription);
            }
            else
            {
                MappedKeyDownActions.Add(keyCode, new List<KeySubscription>() { subscription });
            }

            return subscription;
        }
        
        public KeySubscription SubscribeKeyUp(KeyCode keyCode, UnityAction action)
        {
            var subscription = new KeyDownSubscription(keyCode, action);

            if (MappedKeyUpActions.ContainsKey(keyCode))
            {
                MappedKeyUpActions[keyCode].Add(subscription);
            }
            else
            {
                MappedKeyUpActions.Add(keyCode, new List<KeySubscription>() { subscription });
            }

            return subscription;
        }
    }

    public abstract class KeySubscription
    {
        protected KeyCode Key;
        protected UnityAction _action;
        public string Id;
        public bool IsActive;

        internal KeySubscription(KeyCode code, UnityAction action)
        {
            Key = code;
            _action = action;
            Id = Guid.NewGuid().ToString();
            IsActive = true;
        }

        public abstract void Unsubscribe();

        public void Invoke()
        {
            _action?.Invoke();
        }
    }

    public class KeyDownSubscription : KeySubscription
    {
        public KeyDownSubscription(KeyCode code, UnityAction action) 
            : base(code, action)
        {
        }

        public override void Unsubscribe()
        {
            IsActive = false;
        }
    }
    
    public class KeyUpSubscription : KeySubscription
    {
        public KeyUpSubscription(KeyCode code, UnityAction action) 
            : base(code, action)
        {
        }

        public override void Unsubscribe()
        {
            IsActive = false;
        }
    }
}