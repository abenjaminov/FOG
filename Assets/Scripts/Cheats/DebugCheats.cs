using System;
using ScriptableObjects;
using UnityEngine;

namespace Cheats
{
    public class DebugCheats : MonoBehaviour
    {
        [SerializeField] private Entity.Player.Player _player;
        [SerializeField] private bool _isPlayerInvincible;

        private void Awake()
        {
            if (_player == null)
            {
                Debug.Log("Player is not assigned for cheats");
            }
        }

        private void Update()
        {
            if (_player == null) return;
            
            if (_isPlayerInvincible)
            {
                if (((PlayerTraits) _player.Traits).CurrentHealth < _player.Traits.MaxHealth)
                {
                    FillPlayerHealth();
                }
            }
        }

        [ContextMenu("Fill Player Health")]
        public void FillPlayerHealth()
        {
            if (_player == null) return;
            
            _player.ChangeHealth(_player.Traits.MaxHealth);
        }
    }
}