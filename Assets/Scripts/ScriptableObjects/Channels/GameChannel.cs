using Game;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Game Channel", menuName = "Channels/Game Channel")]
    public class GameChannel : ScriptableObject
    {
        public UnityAction PlayGameEvent;

        public UnityAction<LevelBounds> HitLevelBoundsEvent;

        public void OnPlayGame()
        {
            PlayGameEvent?.Invoke();
        }

        public void OnHitLevelBounds(LevelBounds levelBounds)
        {
            HitLevelBoundsEvent?.Invoke(levelBounds);
        }
    }
}