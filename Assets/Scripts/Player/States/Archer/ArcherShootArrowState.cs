using Player.Archer;
using UnityEngine;

namespace Player.States.Archer
{
    public class ArcherShootArrowState : PlayerAttackState
    {
        private ArcherCombat _archerCombat;
        
        public ArcherShootArrowState(Animator animator, ArcherCombat archerCombat) : base(animator)
        {
            _archerCombat = archerCombat;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _archerCombat.WorldMovementDirection =
                (int)_archerCombat.transform.rotation.y == 180 ? Vector2.left : Vector2.right;
        }
    }
}