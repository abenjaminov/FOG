using Platformer;
using ScriptableObjects;
using UI;
using UnityEngine;

namespace Character
{
    public abstract class Character : MonoBehaviour
    {
        public Traits Traits;
        public bool IsDead;

        protected Collider2D _collider;
        private IHealthUI _healthUIl;
        protected float _health;
        protected int _defense;
        
        [SerializeField] private Transform _damageSpawnPosition;
        [SerializeField] private GameObject _damagePrefab;
        
        protected IHealthUI _healthUI;
        
        protected virtual void Awake()
        {
            _health = Traits.MaxHealth;
            _defense = _defense;
            _collider = GetComponent<Collider2D>();
            _healthUI = GetComponent<IHealthUI>();
        }

        public void ComeAlive()
        {
            _health = Traits.MaxHealth;
            IsDead = false;
            _healthUI?.SetHealth(1);
            _collider.enabled = true;
        }

        protected void DisplayDamage(float damage)
        {
            var position = _damageSpawnPosition.position;
            var damageText = Instantiate(_damagePrefab, position, Quaternion.identity).GetComponent<DamageText>();
            damageText.SetText(damage.ToString());
            damageText.SetPosition(position);
        }
        
        public abstract void ReceiveDamage(float damage);

        protected abstract void Die();
    }
}