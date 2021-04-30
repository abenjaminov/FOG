using Player.Archer;
using UnityEngine;

namespace Player.States.Archer
{
    public class ArcherShootArrowState : PlayerAttackState
    {
        private ArcherCombat _archerCombat;
        
        public ArcherShootArrowState(Animator animator, Rigidbody2D rigidBody, ArcherCombat archerCombat) : base(animator, rigidBody)
        {
            _archerCombat = archerCombat;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _archerCombat.WorldMovementDirection =
                (int)_archerCombat.transform.rotation.y != 0 ? Vector2.left : Vector2.right;
        }
    }
}