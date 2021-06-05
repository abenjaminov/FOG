using System.Linq;
using Assets.HeroEditor.FantasyInventory.Scripts.Data;
using Character.Player.Archer;
using Platformer;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Entity.Player.ArcherCharacter
{
    public class Archer : Entity.Player.Player
    {
        [Header("Archer Specific")]
        [SerializeField] private GameObject _arrowPrefab;

        [HideInInspector] public Vector2 WorldMovementDirection;

        protected override void Awake()
        {
            base.Awake();

            _animationEvents.BowChargeEndEvent += OnShootArrow;
        }

        public void OnShootArrow()
        {
            var arrow = Instantiate(_arrowPrefab, FireTransform).GetComponent<Arrow>();
            
            arrow.WorldMovementDirection = WorldMovementDirection;
            arrow.ParentCharacter = this;
            arrow.Range = TraitsCalculator.CalculateAttackRange(Traits);
            
            var sr = arrow.GetComponent<SpriteRenderer>();

            var arrowTransform = arrow.transform;
            
            arrowTransform.localPosition = Vector3.zero;
            arrowTransform.localRotation = Quaternion.identity;
            arrow.transform.SetParent(null);
            sr.sprite = GetArrowSprite();
        }

        protected override void EquipWeapon(Item weaponItem)
        {
            if (!weaponItem.IsWeapon) return;
            if (!weaponItem.IsBow) return;
            
            //_character.Equip();
        }

        private Sprite GetArrowSprite()
        {
            return _character.Bow.Single(j => j.name == "Arrow");
        }
    }
}
