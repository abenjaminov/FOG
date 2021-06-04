using Animations;
using Character;
using Entity;
using Entity.Player.ArcherCharacter;
using UnityEngine;

namespace State.States.ArcherStates
{
    public class ArcherShootArrowState : PlayerAttackState
    {
        private Archer _archer;
        
        public ArcherShootArrowState(CharacterWrapper character,
            Animator animator, Rigidbody2D rigidBody) : base(animator, rigidBody, character)
        {
            _archer = character as Archer;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _character.GetCharacter().Animator.SetInteger(CachedAnimatorPropertyNames.Charge, 1);
            
            _archer.WorldMovementDirection =
                (int)_archer.transform.rotation.y != 0 ? Vector2.left : Vector2.right;
        }
    }
}