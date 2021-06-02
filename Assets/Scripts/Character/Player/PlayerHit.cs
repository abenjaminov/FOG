using System;
using ScriptableObjects.Channels;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Character.Player
{
    public class PlayerHit : MonoBehaviour
    {
        [SerializeField] private CombatChannel _combatChannel;
        private Character _player;
        
        private void Awake()
        {
            _player = GetComponentInParent<Character>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_player.IsDead) return;
            
            if (other.TryGetComponent(typeof(Character), out var character))
            {
                _combatChannel.OnCharacterHit((Character)character, _player);
            }
        }
    }
}
