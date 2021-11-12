using Game;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Entity
{
    public class BoundsCheck : MonoBehaviour
    {
        [SerializeField] private GameChannel _gameChannel;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out LevelBounds levelBounds)) return;
            
            _gameChannel.OnHitLevelBounds(levelBounds);
        }
    }
}