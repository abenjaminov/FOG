using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Basic Traits", menuName = "Game Stats/Basic Traits", order = 0)]
    public class Traits : ScriptableObject
    {
        public int Health;
        public int Defense;
        
        [Range(1,10)]
        public int Level;

        public int Strength;
        public int Dexterity;
    }
}