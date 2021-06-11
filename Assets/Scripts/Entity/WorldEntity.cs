using Platformer;
using ScriptableObjects;
using ScriptableObjects.Traits;
using State;
using UI;
using UnityEngine;

namespace Entity
{
    public abstract class WorldEntity : MonoBehaviour
    {
        protected Collider2D _collider;
        protected IHealthUI _healthUI;
        protected float _health;
        
        [Header("Game")]
        public Traits Traits;
        [SerializeField] private Transform _damageSpawnPosition;
        [SerializeField] private GameObject _damagePrefab;
        
        [HideInInspector]
        public bool IsDead;
        
        protected virtual void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _healthUI = GetComponent<IHealthUI>();
        }
        
        public virtual void ComeAlive()
        {
            IsDead = false;
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

        protected virtual void Die()
        {
            IsDead = true;
            _collider.enabled = false;
        }

        public virtual void ChangeHealth(float delta)
        {
        }
    }
}