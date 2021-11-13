using Game;
using ScriptableObjects;
using UnityEngine;

namespace Entity.Enemies
{
    public class Enemy : WorldEntity
    {
        private RandomItemsDropper _randomItemsDropper;
        private bool _isAgressive;

        private EnemyHealthUI _healthUI;
        [SerializeField] public string EnemyID;
        [SerializeField] private float _timeAgressive;
        [SerializeField] private bool isInvulnerable;
        private float _activeAgressiveTime;
        public EnemyTraits EnemyTraits;

        protected override void Awake()
        {
            base.Awake();
            
            _randomItemsDropper = GetComponentInParent<RandomItemsDropper>();
            _healthUI = GetComponent<EnemyHealthUI>();
            
            EnemyTraits = Traits as EnemyTraits;
        }

        private void Update()
        {
            if (_isAgressive)
            {
                _activeAgressiveTime += Time.deltaTime;

                if (_activeAgressiveTime >= _timeAgressive)
                {
                    SetAgressive(false);
                }
            }
        }

        public override void ReceiveDamage(float damage)
        {
            SetAgressive(true);
            
            DisplayDamage(damage);
            
            if (isInvulnerable) return;
            
            ChangeHealth(-damage);
            
            if (_health <= 0)
            {
                Die();
            }
        }

        private void SetAgressive(bool isAgresive)
        {
            _activeAgressiveTime = isAgresive ? 0 : Mathf.Infinity;
            _isAgressive = isAgresive;
            _healthUI.ToggleUI(isAgresive);
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
            _healthUI.ToggleUI(false);
        }

        protected override void Die()
        {
            base.Die();

            _randomItemsDropper.Drop();
        }
    }
}