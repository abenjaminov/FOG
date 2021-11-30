using System;
using System.Collections.Generic;
using Game;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Game Channel", menuName = "Channels/Game Channel")]
    public class GameChannel : ScriptableObject
    {
        public UnityAction PlayGameEvent;

        public UnityAction<LevelBounds> HitLevelBoundsEvent;
        public UnityAction<string, string, List<MessageOptions>> ShowMessageRequestEvent;
        public UnityAction<string, MessageOptions> MessageClosedEvent;

        public UnityAction<string, string, List<MessageOptions>> ShowInputRequestEvent;
        public UnityAction<string, string, MessageOptions> InputRequestClosedEvent;
        

        public UnityAction<string> GameErrorEvent;
        
        public void OnPlayGame()
        {
            PlayGameEvent?.Invoke();
        }

        public void OnGameErrorEvent(string message)
        {
            GameErrorEvent?.Invoke(message);
        }
        
        public void OnHitLevelBounds(LevelBounds levelBounds)
        {
            HitLevelBoundsEvent?.Invoke(levelBounds);
        }

        public string ShowGameMessage(string message, List<MessageOptions> options)
        {
            var messageId = Guid.NewGuid().ToString();

            ShowMessageRequestEvent?.Invoke(messageId, message, options);

            return messageId;
        }

        public void OnMessageClosed(string messageId, MessageOptions result)
        {
            MessageClosedEvent?.Invoke(messageId, result);
        }

        public string ShowInputMessage(string message, List<MessageOptions> options)
        {
            var messageId = Guid.NewGuid().ToString();

            ShowInputRequestEvent?.Invoke(messageId, message, options);

            return messageId;
        }
        
        public void OnInputRequestClosed(string messageId, string result, MessageOptions reason)
        {
            InputRequestClosedEvent?.Invoke(messageId, result, reason);   
        }
    }
}