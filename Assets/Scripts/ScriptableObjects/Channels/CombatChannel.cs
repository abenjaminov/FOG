using System;
using Platformer;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Combat Channel", menuName = "Channels/Combat Channel", order = 1)]
    public class CombatChannel : ScriptableObject
    {
        public void OnCharacterHit(Character attacker, Character receiver)
        {
            var damage = TraitsCalculator.CalculateDamage(attacker.Traits, receiver.Traits);
            receiver.ReceiveDamage(damage);
        }
    }
}