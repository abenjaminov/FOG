using System;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platformer
{
    public class DamageCalculator
    {
        public int CalculateDamage(Traits attacker, Traits receiver)
        {
            var randomRangeDiff = Mathf.Exp(Mathf.Ceil((float)attacker.Level / attacker.Dexterity));

            var rangeValue = Mathf.Ceil(Random.Range(-randomRangeDiff, randomRangeDiff));
            
            var damage = Mathf.Ceil(attacker.Strength * ((float)attacker.Level / Traits.MAX_LEVEL));
            damage += rangeValue;

            return (int)Mathf.Ceil(Mathf.Max(attacker.Level, attacker.Level + damage));
        }
    }
}