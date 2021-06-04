using System.Linq;
using Assets.HeroEditor.FantasyInventory.Scripts.Data;
using Character.Player.Archer;
using Platformer;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Character.Player.ArcherCharacter
{
    public class Archer : Player
    {
        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] private Transform _arrowSpawnPosition;
        public Vector2 WorldMovementDirection;
        public Transform FireTransform;

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
            var rb = arrow.GetComponent<Rigidbody>();
            const float speed = 18.75f; // TODO: Change this!
			
            arrow.transform.localPosition = Vector3.zero;
            arrow.transform.localRotation = Quaternion.identity;
            arrow.transform.SetParent(null);
            sr.sprite = _character.Bow.Single(j => j.name == "Arrow");

            var characterCollider = _character.GetComponent<Collider>();

            if (characterCollider != null)
            {
                Physics.IgnoreCollision(arrow.GetComponent<Collider>(), characterCollider);
            }
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
