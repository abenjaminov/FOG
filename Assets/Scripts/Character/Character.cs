using Platformer;
using ScriptableObjects;
using UnityEngine;

namespace Character
{
    public abstract class Character : MonoBehaviour
    {
        public Traits Traits;

        private IHealthUI _healthUIl;
        protected float _health;
        protected int _defense;
        
        protected IHealthUI _healthUI;
        
        private void Awake()
        {
            _health = Traits.Health;
            _defense = _defense;

            _healthUI = GetComponent<IHealthUI>();
        }

        public abstract void ReceiveDamage(float damage);

        protected abstract void Die();
    }
}