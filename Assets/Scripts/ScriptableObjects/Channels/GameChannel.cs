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
        public UnityAction<string, List<MessageOptions>, UnityAction<MessageOptions>> ShowMessageRequest;

        public void OnPlayGame()
        {
            PlayGameEvent?.Invoke();
        }

        public void OnHitLevelBounds(LevelBounds levelBounds)
        {
            HitLevelBoundsEvent?.Invoke(levelBounds);
        }

        public void ShowGameMessage(string message, List<MessageOptions> options, UnityAction<MessageOptions> callback)
        {
            ShowMessageRequest?.Invoke(message, options, callback);
        }
    }
}