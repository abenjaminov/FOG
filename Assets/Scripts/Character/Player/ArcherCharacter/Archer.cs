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

        public void OnShootArrow()
        {
            var arrow = Instantiate(_arrowPrefab, _arrowSpawnPosition.position, transform.rotation).GetComponent<Arrow>();
            arrow.WorldMovementDirection = WorldMovementDirection;
            arrow.ParentCharacter = GetComponent<global::Character.Character>();
            arrow.Range = TraitsCalculator.CalculateAttackRange(Traits);
        }

        public override void ReceiveDamage(float damage)
        {
            
        }

        protected override void Die()
        {
            
        }
    }
}
