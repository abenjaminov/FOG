using ScriptableObjects;
using UnityEngine;

namespace Character.Enemies
{
    public abstract class Enemy : Character
    {
        [SerializeField] private GameObject _damagePrefab;
        [SerializeField] private Transform _damageSpawnPosition;
        [SerializeField] private CombatChannel _combatChannel;
        
        public override void ReceiveDamage(float damage)
        {
            var position = _damageSpawnPosition.position;
            var damageText = Instantiate(_damagePrefab, position, Quaternion.identity).GetComponent<DamageText>();
            damageText.SetText(damage.ToString());
            damageText.SetPosition(position);

            _health = Mathf.Max(0,_health - damage);
            _healthUI?.ReduceHealth(_health / Traits.Health);
            
            if (_health <= 0)
            {
                this.Die();
            }
        }

        protected override void Die()
        {
            _combatChannel.OnEnemyDied(this);
        }
    }
}