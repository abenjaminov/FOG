using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ScriptableObjects.Traits
{
    [CreateAssetMenu(fileName = "Basic Traits", menuName = "Game Stats/Basic Traits", order = 0)]
    public class Traits : ScriptableObject
    {
        public UnityAction HealthChangedEvent;
        public UnityAction LevelUpEvent;

        public float WalkSpeed;
        public float JumpHeight;

        public float BaseDelayBetweenAttacks;
        [HideInInspector] public float DelayBetweenAttacks;
        
        public float MaxHealth;
        public int Defense;
        
        [Header("Game Progress")]
        [Range(1,25)] public int Level;
        

        private void OnEnable()
        {
            DelayBetweenAttacks = BaseDelayBetweenAttacks;
        }
    }
}