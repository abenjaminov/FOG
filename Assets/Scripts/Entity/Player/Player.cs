using System;
using System.Linq;
using Assets.HeroEditor.Common.CommonScripts;
using Entity.Enemies;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.GameConfiguration;
using ScriptableObjects.Inventory;
using ScriptableObjects.Traits;
using UnityEngine;

namespace Entity.Player
{
    public class Player : CharacterWrapper
    {
        private PlayerTraits _playerTraits;

        [Header("Visuals")] [SerializeField] private GameObject _charachterVisuals;
        
        [Header("Player Specific")]
        [SerializeField] private CombatChannel _combatChannel;
        [SerializeField] private PlayerChannel _playerChannel;
        [SerializeField] private LevelConfiguration _levelConfiguration;
        [SerializeField] private Collider2D _hitbox;
        private float _receiveDamageColldown;
        private float _timeUntillVulnerable;
        
        [HideInInspector] public PlayerTraits PlayerTraits => _playerTraits;
        [HideInInspector] public PlayerAppearance Apearence;
        public PlayerEquipment Equipment;
        
        protected override void Awake()
        {
            base.Awake();
            
            _charachterVisuals.transform.localPosition = Vector3.zero;
            
            _playerTraits = Traits as PlayerTraits;;
            _combatChannel.EnemyDiedEvent += EnemyDiedEvent;
            
            // ReSharper disable once PossibleNullReferenceException
            _playerChannel.GainedResistancePointsEvent += GainedExperienceEvent;
            _playerChannel.ReviveEvent += ReviveEvent;
            _playerTraits.DiedEvent += PlayerDiedEvent;

            _receiveDamageColldown = _playerTraits.ReceiveDamageCooldown;
        }

        private void PlayerDiedEvent()
        {
            IsDead = true;
        }

        private void ReviveEvent()
        {
            IsDead = false;
        }

        private void OnDestroy()
        {
            _playerChannel.GainedResistancePointsEvent -= GainedExperienceEvent;
            _combatChannel.EnemyDiedEvent -= EnemyDiedEvent;
            _playerChannel.ReviveEvent -= ReviveEvent;
            _playerTraits.DiedEvent -= PlayerDiedEvent;
        }

        protected override void Start()
        {
            base.Start();
            
            Apearence = GetComponent<PlayerAppearance>();
        }

        private void Update()
        {
            if (_timeUntillVulnerable <= 0) return;
            
            _timeUntillVulnerable -= Time.deltaTime;
            
            if(_timeUntillVulnerable <= 0)
                _hitbox.SetActive(true);
        }

        private void GainedExperienceEvent(float gained)
        {
            var currentLevel = _levelConfiguration.Levels.FirstOrDefault(x => x.Order == _playerTraits.Level);
            if (currentLevel != null && _playerTraits.ResistancePointsGained >= currentLevel.ExpForNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _playerTraits.LevelUp();
        }
        
        public override void ReceiveDamage(float damage)
        {
            if (IsDead || _timeUntillVulnerable > 0) return;
            
            DisplayDamage(damage);

            ChangeHealth(-damage);

            _timeUntillVulnerable = _receiveDamageColldown;
            _hitbox.SetActive(false);
        }

        public override void ChangeHealth(float delta)
        {
            if (_playerTraits.GetCurrentHealth() + delta <= 0)
            {
                IsDead = true;
            }
            
            _playerTraits.ChangeCurrentHealth(delta);
        }

        private void EnemyDiedEvent(Enemy deadEnemy)
        {
            _playerTraits.ResistancePointsGained += ((EnemyTraits) deadEnemy.Traits).ResistancePoints;
        }
    }
}