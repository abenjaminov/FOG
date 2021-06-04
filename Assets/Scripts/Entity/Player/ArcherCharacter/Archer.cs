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

            arrow.transform.localPosition = Vector3.zero;
            arrow.transform.localRotation = Quaternion.identity;
            arrow.transform.SetParent(null);
            sr.sprite = _character.Bow.Single(j => j.name == "Arrow");
        }

        protected override void Die()
        {
            
        }

        protected override void EquipWeapon(Item weaponItem)
        {
            if (!weaponItem.IsWeapon) return;
            if (!weaponItem.IsBow) return;
            
            //_character.Equip();
        }
    }
}
