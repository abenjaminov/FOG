using System.Linq;
using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Entity.Enemies;
using UnityEngine;

namespace Abilities.Magic
{
    public class MagicAttackAbility : Attack
    {
        [SerializeField] private float _forwardRange;
        [SerializeField] private float _downRange;
        [SerializeField] private float _upRange;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private MagicEffect _magicEffect;
        
        private AnimationEvents _animationEvents;
        protected override void Awake()
        {
            base.Awake();
            _animationEvents = GetComponentInChildren<AnimationEvents>();
        }
        
        public override void Use()
        {
            _hostWrapper.GetCharacter().Slash();
        }
        
        public override void Activate()
        {
            _animationEvents.MeleeStrikeEvent += OnMagicHit;
        }

        public override void Deactivate()
        {
            _animationEvents.MeleeStrikeEvent -= OnMagicHit;
        }
        
        private void OnMagicHit()
        {
            var direction = _playerMovement.GetWorldMovementDirection();
            var downAddition = Vector3.down * (_downRange / 3);
            var startPoint = transform.position + downAddition;
            var endPoint = startPoint - downAddition + new Vector3(direction.x * _forwardRange, _upRange);
            
            
            var enemies = Physics2D.OverlapAreaAll(startPoint, endPoint, _layer)
                .OrderBy(x => Vector2.Distance(startPoint, x.transform.position))
                .Take(NumberOfEnemies).Select(x => x.GetComponent<Enemy>()).ToList();
            
            enemies.ForEach(x =>
            {
                Instantiate(_magicEffect, x.transform.position, Quaternion.identity);
                _combatChannel.OnEnemyHit((Entity.Player.Player)_host, x, this);    
            });
        }
        
        private void Update()
        {
            var direction = _playerMovement.GetWorldMovementDirection();
            var downAddition = Vector3.down * (_downRange / 3);
            var startPoint = transform.position + downAddition;
            var endPoint = startPoint - downAddition + new Vector3(direction.x * _forwardRange, _upRange);
            
            Debug.DrawLine(startPoint, endPoint, Color.blue);
        }
    }
}