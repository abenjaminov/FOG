using UnityEngine;

namespace ScriptableObjects.Traits
{
    [CreateAssetMenu(fileName = "Fixed Damage Enemy Traits", menuName = "Game Stats/Fixed Damage Enemy Traits", order = 0)]
    public class FixedDamageEnemyTraits : EnemyTraits
    {
        public int FixedDamage;
        
        public override int GetDamage()
        {
            return FixedDamage;
        }
    }
}