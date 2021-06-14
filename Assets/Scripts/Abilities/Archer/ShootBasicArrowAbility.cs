using System.Linq;
using Entity;
using Entity.Player.ArcherClass;
using Platformer;
using ScriptableObjects.Traits;
using State;
using State.States.ArcherStates;
using UnityEngine;

namespace Abilities.Archer
{
    public class ShootBasicArrowAbility : Attack
    {
        private CharacterWrapper _hostWrapper;
        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] private Transform _fireTransform;
        
        public Vector2 WorldMovementDirection;
        
        public ShootBasicArrowAbility(WorldEntity host,KeyCode hotKey,  int numberEnemies, GameObject arrowPrefab, Transform fireTransform) : base(host, hotKey,numberEnemies)
        {
            _arrowPrefab = arrowPrefab;
            _fireTransform = fireTransform;
            _hostWrapper = host as CharacterWrapper;

            DamagePercentage = 1.0f;
        }

        public override void Use()
        {
            var arrow = Object.Instantiate(_arrowPrefab, _fireTransform).GetComponent<Arrow>();
            
            arrow.WorldMovementDirection = WorldMovementDirection;
            arrow.ParentCharacter = _hostWrapper;
            arrow.Range = TraitsCalculator.CalculateAttackRange(_host.Traits);
            arrow.SetParentAbility(this);
            
            var sr = arrow.GetComponent<SpriteRenderer>();

            var arrowTransform = arrow.transform;
            
            arrowTransform.localPosition = Vector3.zero;
            arrowTransform.localRotation = Quaternion.identity;
            arrow.transform.SetParent(null);
            sr.sprite = GetArrowSprite();
        }

        private Sprite GetArrowSprite()
        {
            return _hostWrapper.GetCharacter().Bow.Single(j => j.name == "Arrow");
        }
    }
}