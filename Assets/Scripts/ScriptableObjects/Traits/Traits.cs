using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Basic Traits", menuName = "Traits/Basic Traits", order = 0)]
    public class Traits : ScriptableObject
    {
        public const int MAX_LEVEL = 100;
        public int Health;
        public int Defense;
        
        [Range(1,100)]
        public int Level;

        public int Strength;
        public int Dexterity;

        public int PointsLeft;
    }
}