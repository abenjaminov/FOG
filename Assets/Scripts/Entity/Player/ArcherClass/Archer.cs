using System.Linq;
using Platformer;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

namespace Entity.Player.ArcherClass
{
    public class Archer : Player
    {
        [Header("Archer Specific")]
        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] protected Transform _fireTransform;

        [HideInInspector] public Vector2 WorldMovementDirection;

        protected override void Start()
        {
            base.Start();

            _animationEvents.BowChargeEndEvent += OnShootArrow;
        }

        public void OnShootArrow()
        {
            var arrow = Instantiate(_arrowPrefab, _fireTransform).GetComponent<Arrow>();
            
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

        private Sprite GetArrowSprite()
        {
            return _character.Bow.Single(j => j.name == "Arrow");
        }
    }
}
