using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Traits", menuName = "Game Stats/Enemy Traits", order = 0)]
    public class EnemyTraits: Traits.Traits
    {
        public int ResistancePoints;

        public virtual int GetDamage()
        {
            var minFactor = 3;
            var maxFactor = 5;

            var minValue = CalculateEnemyAttackValue(minFactor);
            var maxValue = CalculateEnemyAttackValue(maxFactor);

            var damage = Random.Range(minValue, maxValue + 1);

            return damage;
        }
        
        private int CalculateEnemyAttackValue(int factor)
        {
            var result = (int)Math.Ceiling(factor * Mathf.Pow(Level + 1, 1.5f));
            return result;
        }
    }
}