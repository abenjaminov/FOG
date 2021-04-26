using System;
using ScriptableObjects;
using UnityEngine;

namespace Platformer
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] private Traits _traits;

        protected float _health;
        protected int _defense;

        private void Awake()
        {
            _health = _traits.Health;
            _defense = _defense;
        }

        public abstract void ReceiveDamage(float damage);

        protected void Die()
        {
            
        }
    }
}