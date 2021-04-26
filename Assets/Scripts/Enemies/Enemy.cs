using Platformer;
using TMPro;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : Character
    {
        [SerializeField] private GameObject _damagePrefab;
        [SerializeField] private Transform _damageSpawnPosition;
        public override void ReceiveDamage(float damage)
        {
            var position = _damageSpawnPosition.position;
            var damageText = Instantiate(_damagePrefab, position, Quaternion.identity).GetComponent<DamageText>();
            damageText.SetText(damage.ToString());
            damageText.SetPosition(position);

            _health -= damage;

            if (_health <= 0)
            {
                this.Die();
            }
        }
    }
}