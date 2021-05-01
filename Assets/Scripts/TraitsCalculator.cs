using System;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platformer
{
    public static class TraitsCalculator
    {
        public static int CalculateDamage(Traits attacker, Traits receiver)
        {
            var randomRangeDiff = Mathf.Exp(Mathf.Ceil((float)attacker.Level / attacker.Dexterity));

            var rangeValue = Mathf.Ceil(Random.Range(-randomRangeDiff, randomRangeDiff));
            
            var damage = Mathf.Ceil(attacker.Strength * ((float)attacker.Level / LevelConfiguration.MAX_LEVEL));
            damage += rangeValue;

            return (int)Mathf.Ceil(Mathf.Max(attacker.Level, attacker.Level + damage));
        }

        public static float CalculateAttackRange(Traits attacker)
        {
            var range = (attacker.Strength + (attacker.Dexterity*1.5f)) / (float)attacker.Level;
            return range;
        }
    }
}