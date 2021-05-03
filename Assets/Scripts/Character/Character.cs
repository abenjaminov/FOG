using Platformer;
using ScriptableObjects;
using UnityEngine;

namespace Character
{
    public abstract class Character : MonoBehaviour
    {
        public Traits Traits;
        public bool IsDead;
        
        private IHealthUI _healthUIl;
        protected float _health;
        protected int _defense;
        
        protected IHealthUI _healthUI;
        
        private void Awake()
        {
            _health = Traits.MaxHealth;
            _defense = _defense;

            _healthUI = GetComponent<IHealthUI>();
        }

        public void ComeAlive()
        {
            _health = Traits.MaxHealth;
            IsDead = false;
            _healthUI?.SetHealth(1);
        }
        
        public abstract void ReceiveDamage(float damage);

        protected abstract void Die();
    }
}