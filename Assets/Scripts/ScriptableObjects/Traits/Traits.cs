using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Basic Traits", menuName = "Traits/Basic Traits", order = 0)]
    public class Traits : ScriptableObject
    {
        public int Health;
        public int Defense;
        public int Level;

        public int Strength;
        public int Dexterity;
    }
}