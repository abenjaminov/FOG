using Entity;
using Entity.Enemies;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Character.Player
{
    public class PlayerHit : MonoBehaviour
    {
        [SerializeField] private CombatChannel _combatChannel;
        private CharacterWrapper _player;
        
        private void Awake()
        {
            _player = GetComponentInParent<CharacterWrapper>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_player.IsDead) return;
            
            if (other.TryGetComponent(typeof(Enemy), out var character))
            {
                _combatChannel.OnPlayerHit((Entity.Player.Player)_player, (Enemy)character);
            }
        }
    }
}
