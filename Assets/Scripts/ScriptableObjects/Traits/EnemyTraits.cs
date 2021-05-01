using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Traits", menuName = "Game Stats/Enemy Traits", order = 0)]
    public class EnemyTraits: Traits
    {
        public int Experience;
    }
}