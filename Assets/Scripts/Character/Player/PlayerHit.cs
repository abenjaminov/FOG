using System;
using ScriptableObjects.Channels;
using UnityEditor.Build.Reporting;
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
            
            if (other.TryGetComponent(typeof(CharacterWrapper), out var character))
            {
                _combatChannel.OnCharacterHit((CharacterWrapper)character, _player);
            }
        }
    }
}
