using System;
using ScriptableObjects;
using ScriptableObjects.Traits;
using UnityEngine;

namespace Cheats
{
    public class DebugCheats : MonoBehaviour
    {
        private Entity.Player.Player _player;
        [SerializeField] private bool _isPlayerInvincible;

        private void Awake()
        {
            _player = FindObjectOfType<Entity.Player.Player>();
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