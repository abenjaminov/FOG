using UnityEngine;

namespace State.States.ArcherStates
{
    public class ArcherShootArrowState : PlayerAttackState
    {
        private Character.Player.ArcherCharacter.Archer _archerCombat;
        
        public ArcherShootArrowState(Animator animator, Rigidbody2D rigidBody, Character.Player.ArcherCharacter.Archer archerCombat) : base(animator, rigidBody)
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