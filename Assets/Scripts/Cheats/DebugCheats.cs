using System;
using ScriptableObjects;
using UnityEngine;

namespace Cheats
{
    public class DebugCheats : MonoBehaviour
    {
        [SerializeField] private Entity.Player.Player _player;
        [SerializeField] private bool _isPlayerInvincible;

        private void Update()
        {
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
            _player.ChangeHealth(_player.Traits.MaxHealth);
        }
    }
}