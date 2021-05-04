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
            var rangeDiff = GetRangeDiff(attacker);

            return CalculateDamage(attacker, receiver, rangeDiff);
        }

        private static float GetRangeDiff(Traits attacker)
        {
            var rangeDiff = Mathf.Exp(Mathf.Ceil((float) attacker.Level / attacker.Dexterity));

            return rangeDiff;

        }

        private static int CalculateDamage(Traits attacker, Traits receiver,float rangeDiff)
        {
            var rangeValue = Mathf.Ceil(Random.Range(-rangeDiff, rangeDiff));

            return CalculateAttackerDamage(attacker, rangeValue);
        }

        private static int CalculateAttackerDamage(Traits attacker, float rangeValue)
        {
            var damage = Mathf.Ceil(attacker.Strength * ((float) attacker.Level / LevelConfiguration.MAX_LEVEL));
            damage += rangeValue;

            return (int) Mathf.Ceil(Mathf.Max(attacker.Level, attacker.Level + damage));
        }

        public static int GetMaxDamage(Traits attacker)
        {
            var rangeDiff = GetRangeDiff(attacker);

            return CalculateAttackerDamage(attacker, rangeDiff);
        }
        
        public static int GetMinDamage(Traits attacker)
        {
            var rangeDiff = GetRangeDiff(attacker);

            return CalculateAttackerDamage(attacker, -rangeDiff);
        }
        
        public static float CalculateAttackRange(Traits attacker)
        {
            var range = (attacker.Strength + (attacker.Dexterity*1.5f)) / (float)attacker.Level;
            return range;
        }
    }
}