using Game;
using UI;
using UnityEngine;

namespace Character.Enemies
{
    public abstract class Enemy : Character
    {
        private Dropper _dropper;
        private float _health;

        protected override void Awake()
        {
            base.Awake();

            _dropper = GetComponentInParent<Dropper>();
        }

        public override void ReceiveDamage(float damage)
        {
            DisplayDamage(damage);

            _health = Mathf.Max(0,_health - damage);
            _healthUI?.SetHealth(_health / Traits.MaxHealth);
            
            if (_health <= 0)
            {
                Die();
            }
        }

        public override void ComeAlive()
        {
            base.ComeAlive();
            _health = Traits.MaxHealth;
        }

        protected override void Die()
        {
            _collider.enabled = false;
            this.IsDead = true;
            _dropper.Drop();
        }
    }
}