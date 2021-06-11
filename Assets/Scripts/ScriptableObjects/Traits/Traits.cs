using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Traits
{
    [CreateAssetMenu(fileName = "Basic Traits", menuName = "Game Stats/Basic Traits", order = 0)]
    public class Traits : ScriptableObject
    {
        public UnityAction HealthChangedEvent;
        public UnityAction LevelUpEvent;

        public float WalkSpeed;
        public float JumpHeight;

        public float DelayBetweenAttacks;
        
        public float MaxHealth;
        public int Defense;
        
        [Header("Game Progress")]
        [Range(1,10)] public int Level;
        public int Strength;
        public int Dexterity;
    }
}