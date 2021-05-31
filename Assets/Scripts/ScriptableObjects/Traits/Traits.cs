using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Basic Traits", menuName = "Game Stats/Basic Traits", order = 0)]
    public class Traits : ScriptableObject
    {
        public UnityAction HealthChangedEvent;
        
        public float MaxHealth;
        private float CurrentHealth;
        public int Defense;
        
        [Range(1,10)]
        public int Level;

        public int Strength;
        public int Dexterity;

        public void SetCurrentHealth(float health)
        {
            CurrentHealth = health;
            HealthChangedEvent?.Invoke();
        }
        
        public void ChangeCurrentHealth(float healthDelta)
        {
            CurrentHealth += healthDelta;
            HealthChangedEvent?.Invoke();
        }

        public float GetCurrentHealth()
        {
            return CurrentHealth;
        }
    }
}