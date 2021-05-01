using Player.States;
using UnityEngine;

namespace Character.Player.States.ArcherStates
{
    public class ArcherShootArrowState : PlayerAttackState
    {
        private ArcherCharacter.Archer _archerCombat;
        
        public ArcherShootArrowState(Animator animator, Rigidbody2D rigidBody, ArcherCharacter.Archer archerCombat) : base(animator, rigidBody)
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