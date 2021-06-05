using Game;
using UnityEngine;

namespace Entity.Enemies
{
    public abstract class Enemy : WorldEntity
    {
        private Dropper _dropper;

        protected override void Awake()
        {
            base.Awake();
            
            _dropper = GetComponentInParent<Dropper>();
        }

        public override void ReceiveDamage(float damage)
        {
            DisplayDamage(damage);
            ChangeHealth(-damage);
            
            if (_health <= 0)
            {
                Die();
            }
        }

        public override void ChangeHealth(float delta)
        {
            _health = Mathf.Max(0,_health + delta);
            _healthUI?.SetHealth(_health / Traits.MaxHealth);
        }

        public override void ComeAlive()
        {
            base.ComeAlive();
            _health = Traits.MaxHealth;
            _healthUI.SetHealth(1);
        }

        protected override void Die()
        {
            base.Die();

            _dropper.Drop();
        }
    }
}