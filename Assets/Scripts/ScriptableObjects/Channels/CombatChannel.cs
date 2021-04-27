using System;
using Platformer;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Combat Channel", menuName = "Channels/Combat Channel", order = 1)]
    public class CombatChannel : ScriptableObject
    {
        private DamageCalculator _damageCalculator;

        private void OnEnable()
        {
            _damageCalculator = new DamageCalculator();
        }

        public void OnCharacterHit(Character attacker, Character receiver)
        {
            var damage = _damageCalculator.CalculateDamage(attacker.Traits, receiver.Traits);
            receiver.ReceiveDamage(damage);
        }
    }
}